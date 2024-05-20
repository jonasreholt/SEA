using System.Net.Mime;

namespace Model.Question;
using Model.Answer;

public class Question : IReadOnlyQuestion, IModifyQuestion {

    //TODO: Implement getters and setters for images
    private int id;
    private QuestionType questionType;
    //private string caption;
    private string questionText;

    int IReadOnlyQuestion.GetId => throw new NotImplementedException();

    public string GetCaption => throw new NotImplementedException();

    public string GetPicture => throw new NotImplementedException();

    string IReadOnlyQuestion.GetText => throw new NotImplementedException();

    public Answer GetAnswer => throw new NotImplementedException();

    int IModifyQuestion.GetId => throw new NotImplementedException();

    public QuestionType ModifyType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string ModifyCaption { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string ModifyPicture { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string ModifyText { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Answer ModifyAnswer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    Model.Answer IModifyQuestion.ModifyAnswer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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