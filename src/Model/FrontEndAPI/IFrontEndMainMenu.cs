namespace Model.FrontEndAPI;
using Model.Survey;

public interface IFrontEndMainMenu {
    List<SurveyWrapper>? ValidateSuperUser(string username, string password);
    bool ImportSurvey(string filePath);

    bool ExportResults(int surveyId, string folderPath);
    IReadOnlySurvey? GetSurvey(int pincode);

}