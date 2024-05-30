namespace Model.FrontEndAPI;

using Model.Database;
using Structures;

internal class FrontEndSuperUserMenu : IFrontEndSuperUser {

    private  IDatabase db = new DatabaseServices();

    internal FrontEndSuperUserMenu(DatabaseServices databaseServices) {
        this.db = databaseServices;
    }

    public SurveyWrapper CreateSurvey() {
        int surveyId = db.GetNextSurveyID();
        return new SurveyWrapper(surveyId);
    }

    public bool ExportSurveyFromDatabase(int surveyId, string folderPath) {
        if (db.ExportSurvey(surveyId, folderPath)) {
            return true;
        } else {
            return false;
        }
    }

    public SurveyWrapper ModifySurvey(int surveyId) {
        return db.GetSurveyWrapper(surveyId);
    }

    public void StorePicture(int surveyId, string filePath) {
        //To be implemented
    }

    public void StorePicture(int surveyId, string filePath, string optionalPrefix) {
        //To be implemented
    }

    public void StoreSurveyInDatabase(Survey survey) {
        
    }

}