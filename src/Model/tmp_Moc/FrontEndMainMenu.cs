namespace Model.FrontEndAPI;
using Model.Survey;

public static class FrontEndMainMenuMock
{
    private const string DeveloperUserName = "10xdeveloper";
    private const string DeveloperPassword = "ornot";
    private const int DeveloperPin = 123456;

    public static bool ValidateSuperUser(string username, string password) {
        return username == DeveloperUserName && password == DeveloperPassword;
    }
    public static IReadOnlySurvey? GetSurvey(int surveyId)
    {
        return surveyId == DeveloperPin
            ? null
            : null;
    }
}