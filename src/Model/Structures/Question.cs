namespace Model.Structures;

public class Question(string caption, string picturePath, List<SubQuestion> subQuestions)
{
    public string Caption = caption;
    public string PicturePath = picturePath;
    public List<SubQuestion> SubQuestions = subQuestions;
}

public class SubQuestion(string questionText, Answer answer)
{
    public string QuestionText = questionText;
    public Answer Answer = answer;
    /// <summary>
    /// UserId => Results
    /// </summary>
    public Dictionary<int, Result> Results = new();
}