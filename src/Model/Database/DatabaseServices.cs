using System.Text.Json;

namespace Model.Database;
using System;
using System.IO;
using FrontEndAPI;
using Structures;
using System.Collections.Generic;

internal class DatabaseServices : IDatabase
{
    private readonly Dictionary<UserId, List<SurveyWrapper>> _userToSurveys = new();
    
    private int userId = 0;

    private string databasePath = "./surveyDatabase/";
    private string resultsPath;
    internal DatabaseServices() 
    {
        Directory.CreateDirectory(databasePath); //is only created if not exists
        resultsPath = Path.Combine(databasePath, "./results.csv");
        CreateResultsFileIfNotExisting(resultsPath);
        
        // Add basic admin user with example survey
        var username = "admin";
        var password = "admin";
        var uid = new UserId(username, password);
        var sws = new List<SurveyWrapper> { ExampleSurvey.GetSurvey() };

        _userToSurveys[uid] = sws;
    }

    private static void CreateResultsFileIfNotExisting(string resultsPath) 
    {
        if (!File.Exists(resultsPath)) 
        {
            File.Create(resultsPath).Dispose();
        }

    }

    public int GetUserId()
    {
        return userId++;
    }

    public bool ImportSurvey(string path) 
    {
        return false;
    }

    public List<Result> GetResults(int id) 
    {
        throw new NotImplementedException();
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
            return true;
        }

        return false;
    }


    private struct Serializer(List<string> imagePaths, SurveyWrapper surveyWrapper)
    {
        public List<string> ImagePaths = imagePaths;
        public SurveyWrapper SurveyWrapper = surveyWrapper;
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

    public async Task<SurveyWrapper> Deserialize(string path)
    {
        using var openStream = File.OpenRead(path);
        var retVal = await JsonSerializer.DeserializeAsync<SurveyWrapper>(openStream, new JsonSerializerOptions
        {
            Converters = { new PageConverter() }
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

