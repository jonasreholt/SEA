using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Model.Database;
using System;
using System.IO;
using FrontEndAPI;
using Structures;
using System.Collections.Generic;
using backend.JsonConverters;

internal class DatabaseServices : IDatabase
{
    internal Dictionary<UserId, List<SurveyWrapper>> _userToSurveys = new();
    
    private int userId = 0;
    private bool _surveyRunning = false;
    private Survey? _runningSurvey;

    internal const string DatabasePath = "./surveyDatabase";
    private static readonly string CachePath = Path.Combine(DatabasePath, "cache");
    internal DatabaseServices() 
    {
        LoadCache().Wait();
        
        // Add basic admin user with example survey if not present
        var username = "admin";
        var password = "admin";
        var uid = new UserId(username, password);
        if (_userToSurveys.ContainsKey(uid)) return;
        
        var sws = new List<SurveyWrapper> { ExampleSurvey.GetSurvey() };
        _userToSurveys[uid] = sws;
    }

    private struct CacheManifest(Dictionary<UserId, List<SurveyWrapper>> wrappers, int count, bool surveyRunning, Survey runningSurvey)
    {
        [JsonInclude] public Dictionary<UserId, List<SurveyWrapper>> UserIdToWrappers = wrappers;
        [JsonInclude] public int UserIdCount = count;
        [JsonInclude] public bool SurveyRunning = surveyRunning;
        [JsonInclude] public Survey RunningSurvey = runningSurvey;
    }

    private async Task LoadCache()
    {
        if (!Directory.Exists(DatabasePath) || !File.Exists(CachePath))
        {
            return;
        }
        
        var cacheManifest = await Deserialize<CacheManifest>(CachePath);
        _userToSurveys = cacheManifest.UserIdToWrappers;
        userId = cacheManifest.UserIdCount;
        _surveyRunning = cacheManifest.SurveyRunning;
        _runningSurvey = cacheManifest.RunningSurvey;
    }

    internal async Task SaveCache()
    {
        if (!Directory.Exists(DatabasePath))
        {
            Directory.CreateDirectory(DatabasePath);
        }

        var cacheManifest = new CacheManifest(_userToSurveys, userId, _surveyRunning, _runningSurvey!);
        
        await using var createStream = File.Create(CachePath);
        await JsonSerializer.SerializeAsync(createStream, cacheManifest, new JsonSerializerOptions
        {
            Converters = { new DatabaseConverter(), new PageConverter() }
        });
    }

    public int GetUserId()
    {
        return ++userId;
    }
    
    private readonly Random rnd = new Random();
    private Survey ChooseSurvey(SurveyWrapper surveyWrapper)
    {
        var count = surveyWrapper.GetVersionCount();
        var idx = rnd.Next(0, count);

        if (!surveyWrapper.TryGetSurveyVersion(idx, out var survey))
        {
            throw new UnreachableException();
        }

        return survey;
    }

    public (int, Survey) StartSurvey(SurveyWrapper surveyWrapper)
    {
        if (_surveyRunning)
        {
            // we were somehow interrupted last time, so take up last session
            return (userId, _runningSurvey!);
        }
        // This is a new instance of a survey
        _surveyRunning = true;
        _runningSurvey = ChooseSurvey(surveyWrapper);
        return (++userId, _runningSurvey);
    }

    public void StopSurvey()
    {
        _surveyRunning = false;
    }

    public bool Store(SurveyWrapper surveyWrapper, UserId userId, bool overwrite = false)
    {
        if (_userToSurveys.TryGetValue(userId, out var sws))
        {
            if (overwrite)
            {
                var idx = sws.FindIndex(sw => sw.PinCode == surveyWrapper.PinCode);
                if (idx != -1) sws.RemoveAt(idx);
            }
            sws.Add(surveyWrapper);
            
            SaveCache();
            
            return true;
        }

        return false;
    }

    public void ExportResults(SurveyWrapper surveyWrapper, string path)
    {
        var sb = new StringBuilder();
        sb.AppendLine(surveyWrapper.SurveyWrapperName);
        foreach (var survey in surveyWrapper.SurveyVersions)
        {
            var results = survey.GetResults();
            
            sb.AppendLine(survey.SurveyName);
            sb.AppendQuestionHeader(results.Count);
            sb.AppendResults(results);
        }

        using var file = new StreamWriter(path);
        file.Write(sb.ToString());
    }

    public async void Serialize(SurveyWrapper surveyWrapper, string path)
    {
        if (!Directory.Exists(path)) throw new ArgumentException($"'{path}' folder does not exists");

        // Fix up all images to this new folder structure
        foreach (var kvp in FixImagePaths(surveyWrapper, path))
        {
            var currentPath = kvp.Key;
            var newPath = kvp.Value;
            File.Copy(currentPath, newPath);
        }
        
        // Serialize the surveyWrapper itself to the folder
        var surveyPath = Path.Combine(path, surveyWrapper.SurveyWrapperName);
        await using var createStream = File.Create(surveyPath);
        await JsonSerializer.SerializeAsync(createStream, surveyWrapper);
    }

    public async Task<T> Deserialize<T>(string path)
    {
        using var openStream = File.OpenRead(path);
        var retVal = await JsonSerializer.DeserializeAsync<T>(openStream, new JsonSerializerOptions
        {
            Converters = { new PageConverter(), new DatabaseConverter() }
        });
        if (retVal == null) throw new Exception("Could not deserialize the given path");
        return retVal;
    }

    private static List<KeyValuePair<string, string>> FixImagePaths(SurveyWrapper surveyWrapper, string dirPath)
    {
        var imagePaths = new List<KeyValuePair<string, string>>();

        foreach (var survey in surveyWrapper.SurveyVersions)
        {
            imagePaths.AddRange(survey.FixImagePaths(dirPath));
        }
        
        return imagePaths;
    }
    
    public (UserId, SurveyWrapper) GetSurveyWrapper(int surveyId)
    {
        foreach (var kvp in _userToSurveys)
        {
            var sw = kvp.Value.Find(sw => sw.PinCode == surveyId);
            if (sw != null)
            {
                return (kvp.Key, sw);
            }
        }
        
        throw new ArgumentException("Invalid pincode");
    }

    public List<SurveyWrapper> GetSurveyWrappersForSuperUser(UserId userId)
    {
        return _userToSurveys.TryGetValue(userId, out var sws)
            ? sws
            : throw new AggregateException("Invalid user credentail");
    }
}

internal static class StringBuilderExtension
{
    public static StringBuilder AppendQuestionHeader(this StringBuilder sb, int count)
    {
        if (count <= 0) return sb;
        sb.Append(',').Append("UserID");
        for (var i = 0; i < count; i++)
        {
            sb.Append(',').Append(i);
        }

        return sb.Append(Environment.NewLine);
    }

    public static StringBuilder AppendResults(this StringBuilder sb, List<Dictionary<int, Result>> results)
    {
        // [userId -> QuestionResult]
        var userIds = results.Aggregate(new Dictionary<int, List<string>>(), (acc, d) =>
        {
            foreach (var kvp in d)
            {
                if (acc.TryGetValue(kvp.Key, out var res))
                {
                    res.Add(kvp.Value.ToString());
                }
                else
                {
                    acc[kvp.Key] = new List<string>() { kvp.Value.ToString() };
                }
            }
            return acc;
        });

        foreach (var kvp in userIds)
        {
            var userId = kvp.Key;
            var userResults = kvp.Value;

            sb.Append(',').Append(userId);
            foreach (var userResult in userResults)
            {
                sb.Append(',').Append('"').Append(userResult).Append('"');
            }

            sb.Append(Environment.NewLine);
        }

        return sb;
    }
}

