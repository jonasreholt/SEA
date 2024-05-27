namespace Model.Result;
using Model.Answer;

public class Result : IResult {
    public AnswerType AnswerType {get; private set;} // First: ResultType == AnswerType, so no need for 2 different. 2. If we store it as 'ResultType' it can crash if the enum doesn't match the input type.

    public string QuestionResult {get; set;}

    public int UserId {get; private set;}

    public int QuestionId {get; private set;}

    public int SurveyId {get; private set;}

    public Result (int surveyId, int questionId, AnswerType type, int userId, string questionResult) {
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
