namespace Model.FrontEndAPI;
using Model.Database;
using Model.Result;
using Model.Survey;
using System.Text;

public class FrontEndMainMenu : IFrontEndMainMenu {

    private DatabaseServices databaseService;

    public FrontEndMainMenu(DatabaseServices database) {
        databaseService = database;
    }

    public bool ExportResults(int surveyId, string path) {
        List<Result> results = databaseService.GetResults(surveyId);
        try {
            using (StreamWriter writer = new StreamWriter(path, true, Encoding.UTF8)) {
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

    public IReadOnlySurvey? GetSurvey(int pincode) {
        return databaseService.GetSurvey(pincode);
    }

    public void ImportSurvey(string filePath) {
        databaseService.ImportSurvey(filePath);        
    }

    public List<SurveyWrapper>? ValidateSuperUser(string username, string password) {
        return new List<SurveyWrapper>();
    }
}  