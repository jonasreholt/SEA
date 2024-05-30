namespace Model.Structures;

public struct Question(string caption, string picturePath, string questionText, Answer answer)
{
    public string Caption = caption;

    public string QuestionText = questionText;
    
    public string PicturePath = picturePath;
    
    public Answer Answer = answer;

    public List<Result> Results = [];
}