namespace Model.Result;
using Model.Answer;

public class Result
{
    public AnswerType AnswerType {get; private set;} // First: ResultType == AnswerType, so no need for 2 different. 2. If we store it as 'ResultType' it can crash if the enum doesn't match the input type.

    public string QuestionResult {get; set;}

    public int UserId {get; private set;}

    public int QuestionId {get; private set;}

    public int SurveyId {get; private set;}

    public Result (AnswerType type, string questionResult, int userId, int questionId, int surveyId) {
        AnswerType = type;
        QuestionResult = questionResult;
        UserId = userId;
        QuestionId = questionId;
        SurveyId = surveyId;
    }

    public override string ToString() {
        return $"{SurveyId},{QuestionId},{AnswerType},{UserId},{QuestionResult}";
    }
}

//     void StoreResultFromQuestion(int surveyID, int questionsID, int userID, Result result);
