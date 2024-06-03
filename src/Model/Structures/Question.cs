using System.Text.Json.Serialization;

namespace Model.Structures;

public class Question(string caption, string picturePath, List<SubQuestion> subQuestions)
{
    [JsonInclude]
    public string Caption = caption;
    
    [JsonInclude]
    public string PicturePath = picturePath;
    
    [JsonInclude]
    public List<SubQuestion> SubQuestions = subQuestions;

    public Question Copy()
    {
        var copyQs = new List<SubQuestion>(subQuestions.Count);
        foreach (var q in subQuestions)
        {
            copyQs.Add(q.Copy());
        }

        return new Question(caption, picturePath, copyQs);
    }
}

public class SubQuestion(string questionText, Answer answer)
{
    [JsonInclude]
    public string QuestionText = questionText;
    
    [JsonInclude]
    public Answer Answer = answer;
    
    /// <summary>
    /// UserId => Results
    /// </summary>
    [JsonInclude]
    public Dictionary<int, Result> Results = new();

    public SubQuestion Copy()
    {
        return new SubQuestion(QuestionText, answer.Copy());
    }
}