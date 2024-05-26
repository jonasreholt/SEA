namespace Model.FrontEndAPI;
using Model.Survey;

public interface IFrontEndMainMenu {
    bool ValidateSuperUser(string username, string password);
    void ImportSurvey(string filePath);

    bool ExportResults(int surveyId, string path);
    IReadOnlySurvey GetSurvey(int surveyId);

}