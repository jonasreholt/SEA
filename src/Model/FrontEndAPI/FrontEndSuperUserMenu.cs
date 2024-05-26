namespace Model.FrontEndAPI;
using Model.Survey;
using Model.Database;
internal class FrontEndSuperUserMenu : IFrontEndSuperUser {

    private  IDatabase db = new DatabaseServices();

    internal FrontEndSuperUserMenu(DatabaseServices databaseServices) {
        this.db = databaseServices;
    }

    public string ExportSurveyFromDatabase(int surveyId) {
        throw new NotImplementedException();
    }

    public IModifySurveyWrapper ModifySurvey(int surveyId) {
        return db.GetSurveyWrapper(surveyId);
    }

    public void StorePicture(int surveyId, string filePath) {
        throw new NotImplementedException();
    }

    public void StorePicture(int surveyId, string filePath, string optionalPrefix) {
        throw new NotImplementedException();
    }

    public void StoreSurveyInDatabase(IModifySurvey survey)
    {
        throw new NotImplementedException();
    }

    // // Flyt 3 nedenstående til separat interface?
    // public  void DeleteSurvey(int surveyId) {

    // }
    // public  IModifySurvey? ModifySurvey(int surveyId) {
    //     return null;
    // }
    // public  IModifySurvey CreateSurvey() {
    //     return null;
    // }

    // public  string ExportSurvey(int surveyId) {
    //     return null;
    // }

}