using System.Net.Mime;
namespace Question;

public class Question : IReadOnlyQuestion, IModifyQuestion {

    //TODO: Implement getters and setters for images
    private int id;
    private QuestionType questionType;
    //private string caption;
    private string questionText;

    public Question(int id, QuestionType type, string questionText) {
        id = id;
        questionType = type;
        questionText = questionText;
    }
    public int GetId() { return id; }

    public QuestionType GetType() { return questionType; }

    public string GetText() { return questionText; }

    public void SetType(QuestionType questionType) { questionType = questionType; }

    public void SetText(string questionText) { questionText = questionText; }
    
    
    
    
    // string SetText {set;}

    // string GetCaption {get;}
    // string GetText {get;}
}