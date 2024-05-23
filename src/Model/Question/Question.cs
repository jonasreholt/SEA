namespace Model.Question;

using System.Net.Mime;
using Model.Answer;

public class Question : IReadOnlyQuestion, IModifyQuestion {
    private int id;

    private string caption;

    private string questionText;
    
    private string picture;
    
    private string text;
    
    private Answer answer;

    public string ReadOnlyCaption => caption;

    public string ReadOnlyPicture => picture;

    public string ReadOnlyText => text;

    public IReadOnlyAnswer ReadOnlyAnswer => answer;

    public int QuestionId => id;

    public string ModifyCaption { get => caption; set => caption = value; }
    public string ModifyPicture { get => picture; set => picture = value; }
    public string ModifyText { get => text; set => text = value; }
    public IModifyAnswer ModifyAnswer { get => answer; }

    public Question(int id) {
        this.id = id;
        caption = string.Empty;
        questionText = string.Empty;
        picture = string.Empty;
        text = string.Empty;
        answer = new Answer();
    }
}