
namespace Model.Database;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using SurveyWrapper = Model.Survey.SurveyWrapper;
using Survey = Model.Survey.Survey;
using Result = Model.Result.Result;
using Model.Result;
using Model.Answer;
using System.Collections.Generic;

internal class DatabaseServices : IDatabase {

    private string databasePath = "./surveyDatabase/";
    private string resultsPath;
    internal DatabaseServices() {
        Directory.CreateDirectory(databasePath); //is only created if not exists
        resultsPath = Path.Combine(databasePath, "./results.csv");
        CreateResultsFileIfNotExisting(resultsPath);
    }

    private static void CreateResultsFileIfNotExisting(string resultsPath) {
        if (!File.Exists(resultsPath)) {
            File.Create(resultsPath).Dispose();
        }
        
    }

    public bool StoreSurvey(Survey survey) {
        string surveyPath = GetSurveyPath(survey.SurveyId);
        Directory.CreateDirectory(surveyPath);
        SaveSurveyToFile(surveyPath, survey);
        return true;
    }

    public void StorePictureOverwrite(string src, int surveyId) {
        string surveyAssetsPath = GetSurveyAssetsPath(surveyId); 
        string dest = Path.Combine(surveyAssetsPath, Path.GetFileName(src));
        Directory.CreateDirectory(surveyAssetsPath);
        File.Copy(src, dest, true); //true -> overwrites automatically if dest already exists
    }

    public bool TryStorePicture(string src, int surveyId) {
        string surveyAssetsPath = GetSurveyAssetsPath(surveyId); 
        string dest = Path.Combine(surveyAssetsPath, Path.GetFileName(src));
        Directory.CreateDirectory(surveyAssetsPath);
        if (!File.Exists(dest)) {
            File.Copy(src, dest);
            return true;
        } else {
            return false;
        }
    }

    private string GetSurveyPath(int surveyId) {
        return Path.Combine(databasePath, surveyId.ToString());
    }

    private string GetSurveyAssetsPath(int surveyId) {
        return Path.Combine( GetSurveyPath(surveyId), "assets");
    }

    private static void SaveSurveyToFile(string surveyPath, Survey survey) {
        string jsonString = JsonSerializer.Serialize(survey);
        File.WriteAllText(surveyPath, jsonString);
    }

    // private List<Survey> LoadAllSurveysFromDatabase() {
    //     string jsonString = File.ReadAllText(SurveyDatabasePath);
    //     List<Survey> surveys = JsonSerializer.Deserialize<List<Survey>>(jsonString)!;
    //     return surveys;
    // }

    // Tmp int used to increment to get unique IDs, must be received from db.
    private int tmpId = 0; 
    public int GetNextSurveyID() {
        return tmpId++;
    }

    public Survey GetSurvey(int surveyId) {
        return (new Survey(surveyId));
    }

    public bool ExportSurvey(int id, string path) {
        return true;
    }

    public bool ImportSurvey(string path) {
        return true;
    }

    public List<Result> GetResults(int id) {
        throw new NotImplementedException();
    }


    public bool StoreResult (IResult result) {
        try {
            using (StreamWriter writer = new StreamWriter(resultsPath, true, Encoding.UTF8)) {
                    writer.WriteLine(result.ToString());
            }
            return true;
        }
        catch (Exception) {
            // Write failed, return false
            return false;
        }
    }

    public bool StoreResults(List<Result> results) {
        try {
            using (StreamWriter writer = new StreamWriter(resultsPath, true, Encoding.UTF8)) {
                foreach (var result in results) {
                    writer.WriteLine(result.ToString());
                }
            }
            return true;
        }
        catch (Exception) {
            // Handle exceptions if needed
            return false;
        }
    }

    public SurveyWrapper GetSurveyWrapper(int surveyId) {
        return new SurveyWrapper(surveyId);
    }
}

