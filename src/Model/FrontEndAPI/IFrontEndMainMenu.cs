namespace Model.FrontEndAPI;
using Model.Structures;

public interface IFrontEndMainMenu {
    List<SurveyWrapper>? ValidateSuperUser(string username, string password);
    bool ImportSurvey(string filePath);

    bool ExportResults(int surveyId, string folderPath);
    SurveyWrapper? GetSurvey(int pincode);

}