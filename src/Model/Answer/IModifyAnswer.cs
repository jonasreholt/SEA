namespace Model.Answer;
using System.Collections.ObjectModel;

public interface IModifyAnswer {
    AnswerType ModifyAnswerType {get; set;}
    public ReadOnlyCollection<string> ModifyAnswers {get;}
    void AddAnswerOption(string answer);
    void AddAnswerOption(string answer, int index);// Insert answerOption at index 'index'
    bool TryDeleteAnswerOption(int index); // Delete option 'index' from the AnswerList
}