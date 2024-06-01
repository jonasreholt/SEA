namespace Model.FrontEndAPI;
using Database;
using Structures;
using System.Text;
using System.IO;

internal class FrontEndMainMenu : IFrontEndMainMenu 
{

    private IDatabase db;

    internal FrontEndMainMenu(DatabaseServices database) {
        db = database;
    }

    public SurveyWrapper GetSurvey(int surveyId) {
        return db.GetSurveyWrapper(surveyId);
    }

    public bool ImportSurvey(string filePath) {
        return db.ImportSurvey(filePath);        
    }

    public List<SurveyWrapper>? ValidateSuperUser(UserId userId) 
    {
        var surveyWrappers = db.GetSurveyWrappersForSuperUser(userId);
        return surveyWrappers;
    }
}  