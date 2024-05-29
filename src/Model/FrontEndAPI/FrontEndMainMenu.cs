namespace Model.FrontEndAPI;
using Model.Database;
using Model.Result;
using Model.Survey;
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
    public IReadOnlySurveyWrapper GetSurvey(int surveyId) {
        return db.GetSurveyWrapper(surveyId);
    }

    public bool ImportSurvey(string filePath) {
        return db.ImportSurvey(filePath);        
    }

    public     List<IModifySurveyWrapper>? ValidateSuperUser(string username, string password) {
        //Validate superuser against Hashfunction first. If true, then return the list of surveys
        List<SurveyWrapper> surveyWrappers = db.GetSurveyWrapperForSuperUser(username);
        List<IModifySurveyWrapper> result = new List<IModifySurveyWrapper>(surveyWrappers.Cast<IModifySurveyWrapper>().ToList());
        return result;

    }
}  