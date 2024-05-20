
using System.Text.Json;
namespace DatabaseServices;
using Survey = Model.Survey.Survey;
using Result = Model.Result.Result;
public class DatabaseServices {

    private string SurveyDatabasePath = "./surveyDatabase.json";
    public DatabaseServices() {
        if (!File.Exists(SurveyDatabasePath)) {
            List<Survey> emptyList = new List<Survey>{};
            string jsonString = JsonSerializer.Serialize(emptyList);
            //ok for the json file with the empty list to be created upon instantiation of this class if it does not already exist?
            File.WriteAllText(SurveyDatabasePath, jsonString);
        }
    }

    public bool StoreSurvey(Survey survey) {
        List<Survey> surveys = LoadAllSurveysFromDatabase();
        //add check that this survey does not already exist? will surveys only ever be stored as complete finished surveys?
        surveys.Add(survey);
        SaveSurveysToFile(surveys);
        return true;
    }

    private List<Survey> LoadAllSurveysFromDatabase() {
        string jsonString = File.ReadAllText(SurveyDatabasePath);
        List<Survey> surveys = JsonSerializer.Deserialize<List<Survey>>(jsonString)!;
        return surveys;
    }
    private void SaveSurveysToFile(List<Survey> surveys) {
        string jsonString = JsonSerializer.Serialize(surveys);
        File.WriteAllText(SurveyDatabasePath, jsonString);
    }
}

