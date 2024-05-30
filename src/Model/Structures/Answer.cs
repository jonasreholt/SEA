namespace Model.Structures;
using System.Collections.Generic;
using System.Collections.ObjectModel;

// Answer is the given options to a question, by the survey creater.
// When an experimentee takes the survey, they will give a Result
public class Answer {
    private List<string> modifyAnswers = new List<string>();
    private AnswerType answerType = AnswerType.Text;
    
    public ReadOnlyCollection<string> ModifyAnswers => modifyAnswers.AsReadOnly();

    public AnswerType ModifyAnswerType { get => answerType; set => answerType = value; }

    public AnswerType ReadOnlyAnswerType {get => answerType;}

    public ReadOnlyCollection<string> ReadOnlyAnswers => modifyAnswers.AsReadOnly();

    public Answer(AnswerType type)
    {
        answerType = type;
    }

    public Answer(AnswerType type, string option) : this(type)
    {
        AddAnswerOption(option);
    }

    public Answer(AnswerType type, string[] options) : this(type)
    {
        foreach (var option in options)
        {
            AddAnswerOption(option);
        }
    }

    public void AddAnswerOption(string answer) {
        modifyAnswers.Add(answer);
    }
    
    public void AddAnswerOption(string answer, int index) {
        modifyAnswers.Insert(index, answer);
    }
    
    public bool TryDeleteAnswerOption(int index) {
        if (0 <= index && index < modifyAnswers.Count()) {
            modifyAnswers.RemoveAt(index);
            return true;
        }
        return false;
    }
}