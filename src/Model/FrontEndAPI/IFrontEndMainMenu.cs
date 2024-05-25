namespace Model.FrontEndAPI;
using Model.Survey;

public interface IFrontEndMainMenu {
    bool ValidateSuperUser();
    void ImportSurvey(string filePath);

    string ExportResults(int surveyId);

}