namespace Model.Result;
using Model.Answer;
public interface IResult
{
    AnswerType AnswerType {get;}
    List<string> QuestionResult {get; set;}
    int UserId {get;}
    int QuestionId {get;}
    int SurveyId {get;}
}

//     void StoreResultFromQuestion(int surveyID, int questionsID, int userID, Result result);
