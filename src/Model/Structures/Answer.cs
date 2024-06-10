using System.Text.Json.Serialization;

namespace Model.Structures;
using System.Collections.Generic;

// Answer is the given options to a question, by the survey creater.
// When an experimentee takes the survey, they will give a Result
public class Answer
{
    [JsonInclude] public List<string> AnswerOptions = new();

    [JsonInclude] public AnswerType AnswerType;

    public Answer(AnswerType type)
    {
        AnswerType = type;
    }

    public Answer(AnswerType type, string option) : this(type)
    {
        AddAnswerOption(option);
    }

    [JsonConstructor]
    public Answer(AnswerType answerType, List<string> answerOptions)
    {
        AnswerType = answerType;
        AnswerOptions = new List<string>(answerOptions);
    }

    public void AddAnswerOption(string answer) {
        AnswerOptions.Add(answer);
    }
    
    public void AddAnswerOption(string answer, int index) {
        AnswerOptions.Insert(index, answer);
    }
    
    public bool TryDeleteAnswerOption(int index) {
        if (0 <= index && index < AnswerOptions.Count()) {
            AnswerOptions.RemoveAt(index);
            return true;
        }
        return false;
    }

    public Answer Copy()
    {
        var optionsCopy = new List<string>(AnswerOptions);
        var copy = new Answer(AnswerType, optionsCopy);
        return copy;
    }
}