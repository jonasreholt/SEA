namespace Survey;
using Question;
using Answer;

public interface IModifySurvey {
    // IModifyQuestion SetNextQuestion {set;} // Det giver ikke mening at have en hel modify next question? det er forskellige dele der modifies.
    // IModifyQuestion ModifyQuestion {get; set;}
    IModifyQuestion TryGetNextModifyQuestion();
    IModifyQuestion ModifyQuestion(int questionNumber);
    IModifyAnswer ModifyAnswer(int questionNumber);
    void DeleteQuestionAndAnswers(int questionNumber);
    void CreateNewQuestion();
    void InsertNewQuestionAfterThis(int questionNumber);
    
}