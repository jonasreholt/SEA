namespace Model.Survey;
using Model.Question;
using Model.Answer;

public interface IModifySurvey {

    IEnumerable<IModifyQuestion>? TryGetModifyQuestion(int index);
    IEnumerable<IModifyQuestion>? TryGetNextModifyQuestion();
    IEnumerable<IModifyQuestion>? TryGetPreviousModifyQuestion();
    void DeleteQuestion(int index);
    IEnumerable<Question> AddNewQuestion(); // Add new question at the end of the Enumerable
    IEnumerable<Question> InsertNewQuestion(int index); // Add new question at position 'index'

}