namespace Model.FrontEndAPI;
using Model.Survey;
using Model.Database;
internal class FrontEndSuperUserMenu : IFrontEndSuperUser {

    private  IDatabase db = new DatabaseServices();

    internal FrontEndSuperUserMenu(DatabaseServices databaseServices) {
        this.db = databaseServices;
    }

    public string ExportSurveyFromDatabase(int surveyId) {
        return string.Empty;
    }

    public IModifySurveyWrapper ModifySurvey(int surveyId) {
        return db.GetSurveyWrapper(surveyId);
    }

    public void StorePicture(int surveyId, string filePath) {
        //To be implemented
    }

    public void StorePicture(int surveyId, string filePath, string optionalPrefix) {
        //To be implemented
    }

    public void StoreSurveyInDatabase(IModifySurvey survey) {
        
    }

}