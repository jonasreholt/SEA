using Model.Result;
using Model.Answer;

namespace scivu.Model;


public struct Answer : IResult
{
    public AnswerType AnswerType {get;}
    public string QuestionResult {get; set;}
    public int UserId {get;}
    public int QuestionId {get;}
    public int SurveyId {get;}

    public Answer(AnswerType type, string result, int userId, int questionId, int surveyId)
    {
        AnswerType = type;
        QuestionResult = result;
        UserId = userId;
        QuestionId = questionId;
        SurveyId = surveyId;
    }
}