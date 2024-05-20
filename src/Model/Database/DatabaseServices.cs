
namespace DatabaseServices;
using System.Text.Json;
using System.IO;
using Survey = Model.Survey.Survey;
using Result = Model.Result.Result;
public class DatabaseServices {

    private string databasePath = "./surveyDatabase/";
    public DatabaseServices() {
        Directory.CreateDirectory(databasePath); //is only created if not exists
    }

    public bool StoreSurvey(Survey survey) {
        string surveyPath = GetSurveyPath(survey.SurveyId);
        Directory.CreateDirectory(surveyPath);
        SaveSurveyToFile(surveyPath, survey);
        return true;
    }

    public void StorePictureOverwrite(string src, int surveyId) {
        string surveyAssetsPath = GetSurveyAssetsPath(surveyId); 
        string dest = surveyAssetsPath + Path.GetFileName(src);
        Directory.CreateDirectory(surveyAssetsPath);
        File.Move(src, dest, true); //true -> overwrites automatically if dest already exists
    }

    public bool TryStorePicture(string src, int surveyId) {
        string surveyAssetsPath = GetSurveyAssetsPath(surveyId); 
        string dest = surveyAssetsPath + Path.GetFileName(src);
        Directory.CreateDirectory(surveyAssetsPath);
        if (!File.Exists(dest)) {
            File.Move(src, dest);
            return true;
        } else {
            return false;
        }
    }

    private string GetSurveyPath(int surveyId) {
        return databasePath + surveyId + "/";
    }

    private string GetSurveyAssetsPath(int surveyId) {
        return GetSurveyPath(surveyId) + "/assets/";
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
    
}

