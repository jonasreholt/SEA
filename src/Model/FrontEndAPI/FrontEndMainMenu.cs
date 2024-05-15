namespace FrontEndAPI;

public static class FrontEndMainMenu
{
    private const string DeveloperUserName = "10xdeveloper";
    private const string DeveloperPassword = "ornot";
    private const int DeveloperPin = 123456;

    public static bool ValidateSuperUser(string username, string password) {
        return username == DeveloperUserName && password == DeveloperPassword;
    }
    public static IGetSurvey? GetSurvey(int surveyId)
    {
        return surveyId == DeveloperPin
            ? new MockSurvey()
            : null;
    }

    private class MockSurvey : IGetSurvey
    {
        public Question.IGetQuestion GetNextQuestion {get;}

        public Question.IGetQuestion GetQuestionA {get;}

        public Question.IGetQuestion GetQuestionB {get;}

        public Answer.IGetAnswer GetAnswer {get;}

        public Question.IGetQuestion GetPreviousQuestion {get;}
    }
}