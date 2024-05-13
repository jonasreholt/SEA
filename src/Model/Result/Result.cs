namespace Result;

public class Result
{
    public ResultType ResultType {get; private set;}

    public string QuestionResult {get; set;}

    public int UserId {get; private set;}

    public int QuestionId {get; private set;}

    public int SurveyId {get; private set;}

    public Result (ResultType type, string questionResult, int userId, int questionId, int surveyId) {
        ResultType = type;
        QuestionResult = questionResult;
        UserId = userId;
        QuestionId = questionId;
        SurveyId = surveyId;
    }
}

//     void StoreResultFromQuestion(int surveyID, int questionsID, int userID, Result result);
