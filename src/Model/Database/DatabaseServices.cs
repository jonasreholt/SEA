using System.Text.Json;

namespace Model.Database;
using System;
using System.IO;
using FrontEndAPI;
using Structures;
using System.Collections.Generic;
using backend.JsonConverters;

internal class DatabaseServices : IDatabase
{
    private Dictionary<UserId, List<SurveyWrapper>> _userToSurveys = new();
    
    private int userId = 0;

    private const string DatabasePath = "./surveyDatabase";
    private readonly string CachePath = Path.Combine(DatabasePath, "cache");
    internal DatabaseServices() 
    {
        LoadCache();
        
        // Add basic admin user with example survey if not present
        var username = "admin";
        var password = "admin";
        var uid = new UserId(username, password);
        if (_userToSurveys.ContainsKey(uid)) return;
        
        var sws = new List<SurveyWrapper> { ExampleSurvey.GetSurvey() };
        _userToSurveys[uid] = sws;
    }

    private async void LoadCache()
    {
        if (!Directory.Exists(DatabasePath) || !File.Exists(CachePath))
        {
            return;
        }
        
        _userToSurveys = await Deserialize<Dictionary<UserId, List<SurveyWrapper>>>(CachePath);
    }

    private async void SaveCache()
    {
        if (!Directory.Exists(DatabasePath))
        {
            Directory.CreateDirectory(DatabasePath);
        }
        
        await using var createStream = File.Create(CachePath);
        await JsonSerializer.SerializeAsync(createStream, _userToSurveys, new JsonSerializerOptions
        {
            Converters = { new DatabaseConverter(), new PageConverter() }
        });
    }

    public int GetUserId()
    {
        return userId++;
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
        throw new NotImplementedException();
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
    
    public SurveyWrapper GetSurveyWrapper(int surveyId)
    {
        var swss = _userToSurveys.Values;
        foreach (var sws in swss)
        {
            var sw = sws.Find(sw => sw.PinCode == surveyId);
            if (sw != null)
            {
                return sw;
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

