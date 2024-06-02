using System.Text.Json.Serialization;

namespace Model.Structures;
using System.Collections.Generic;

// Answer is the given options to a question, by the survey creater.
// When an experimentee takes the survey, they will give a Result
public class Answer 
{
    private List<string> _answerOptions = new();
    
    private AnswerType _answerType = AnswerType.Text;
    
    public List<string> AnswerOptions => _answerOptions;

    public AnswerType AnswerType { get => _answerType; set => _answerType = value; }

    public Answer(AnswerType type)
    {
        _answerType = type;
    }

    public Answer(AnswerType type, string option) : this(type)
    {
        AddAnswerOption(option);
    }

    [JsonConstructor]
    public Answer(AnswerType answerType, List<string> answerOptions)
    {
        _answerType = answerType;
        _answerOptions = new List<string>(answerOptions);
    }

    public void AddAnswerOption(string answer) {
        _answerOptions.Add(answer);
    }
    
    public void AddAnswerOption(string answer, int index) {
        _answerOptions.Insert(index, answer);
    }
    
    public bool TryDeleteAnswerOption(int index) {
        if (0 <= index && index < _answerOptions.Count()) {
            _answerOptions.RemoveAt(index);
            return true;
        }
        return false;
    }

    public Answer Copy()
    {
        var optionsCopy = new List<string>(_answerOptions);
        var copy = new Answer(AnswerType, optionsCopy);
        return copy;
    }
}