namespace Model.FrontEndAPI;
using Model.Survey;

public interface IFrontEndMainMenu {
    List<SurveyWrapper>? ValidateSuperUser(string username, string password);
    void ImportSurvey(string filePath);

    bool ExportResults(int surveyId, string path);
    IReadOnlySurvey? GetSurvey(int pincode);

}