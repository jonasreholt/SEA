namespace Survey;
using Question;
using Answer;

public interface IModifySurvey {
    // IModifyQuestion SetNextQuestion {set;} // Det giver ikke mening at have en hel modify next question? det er forskellige dele der modifies.
    // IModifyQuestion ModifyQuestion {get; set;}
    IModifyQuestion? TryGetNextModifyQuestion(QuestionVersion questionVersion);
    IModifyQuestion? TryGetPreviousModifyQuestion(QuestionVersion questionVersion);
    IModifyQuestion? TryGetModifyQuestion(int questionNumber, QuestionVersion questionVersion);
    // IModifyQuestion GetModifyQuestionB(int questionNumber);
    // IModifyAnswer? TryGetModifyAnswer(int questionNumber, QuestionVersion questionVersion);
    // void DeleteQuestionAndAnswers(int questionNumber, QuestionVersion questionVersion);
    // void CreateNewQuestion(QuestionPair questionPair);
    // void InsertNewQuestionAfterThis(int questionNumber);
    
}