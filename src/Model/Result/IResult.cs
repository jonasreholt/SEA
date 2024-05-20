namespace Model.Result;

public interface IResult
{
    ResultType ResultType {get;}
    string QuestionResult {get; set;}
    int userId {get;}
    int questionId {get;}
    int surveyId {get;}
}

//     void StoreResultFromQuestion(int surveyID, int questionsID, int userID, Result result);
