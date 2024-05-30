namespace Model.FrontEndAPI;
using Database;
using Structures;
using System.Text;
using System.IO;

internal class FrontEndMainMenu : IFrontEndMainMenu {

    private IDatabase db;

    internal FrontEndMainMenu(DatabaseServices database) {
        db = database;
    }

    public bool ExportResults(int surveyId, string folderPath) {
        List<Result> results = db.GetResults(surveyId);
        string path = Path.Combine(folderPath, $"{surveyId}.csv");
        try {
            using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8)) {
                foreach (var result in results) {
                    writer.WriteLine(result.ToString());
                }
            }
            return true;
        }
        catch (Exception) {
            return false;
        } 
    }
    public SurveyWrapper GetSurvey(int surveyId) {
        return db.GetSurveyWrapper(surveyId);
    }

    public bool ImportSurvey(string filePath) {
        return db.ImportSurvey(filePath);        
    }

    public List<SurveyWrapper>? ValidateSuperUser(string username, string password) 
    {
        var surveyWrappers = db.GetSurveyWrappersForSuperUser(username, password);
        return surveyWrappers;
    }
}  