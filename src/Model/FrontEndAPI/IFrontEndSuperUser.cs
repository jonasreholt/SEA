using Model.Structures;

namespace Model.FrontEndAPI;

public interface IFrontEndSuperUser {
    
    SurveyWrapper CreateSurvey();
    SurveyWrapper ModifySurvey(int surveyId); // Possibly (SuperUserId, SurveyId)?

    void StoreSurveyInDatabase (Structures.Survey survey);

    bool ExportSurveyFromDatabase(int surveyId, string folderPath);

    void StorePicture(int surveyId, string filePath);
    void StorePicture(int surveyId, string filePath, string optionalPrefix); //filename prefix: eg version_A_fbpic1, version_B_fbpic1
}