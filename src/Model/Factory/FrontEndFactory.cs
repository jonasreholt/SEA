namespace Model.Factory;

using Model.Database;
using Model.FrontEndAPI;
using Result;
using Answer;

public static class FrontEndFactory {
    private static DatabaseServices databaseServices = new DatabaseServices();
    public static IFrontEndMainMenu CreateMainMenu() {
        return new FrontEndMainMenu(databaseServices);
    }

    public static IFrontEndExperimenter CreateExperimenterMenu() {
        return new FrontEndExperimenter(databaseServices);
    }

    public static IFrontEndSuperUser CreateSuperUserMenu() {
        return new FrontEndSuperUserMenu(databaseServices);
    }

    public static IResult CreateResult(
        int surveyId,
        int questionId,
        AnswerType type,
        int userId,
        List<string> questionResult) => new Result(surveyId, questionId, type, userId, questionResult);
}