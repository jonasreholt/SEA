namespace Model.Question;

using System.Net.Mime;
using Model.Answer;

public class Question : IReadOnlyQuestion, IModifyQuestion {

    //TODO: Implement getters and setters for images
    private int id;
    private string caption;
    private string questionText;
    private string picture;

    public Question(int id, string text) {
        //id = id;
        // questionText = questionText;
    }

    public int ReadOnlyId => throw new NotImplementedException();

    public string ReadOnlyCaption => throw new NotImplementedException();

    public string ReadOnlyPicture => throw new NotImplementedException();

    public string ReadOnlyText => throw new NotImplementedException();

    public Answer ReadOnlyAnswer => throw new NotImplementedException();

    public int GetId => throw new NotImplementedException();

    public string ModifyCaption { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string ModifyPicture { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string ModifyText { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IModifyAnswer ModifyAnswer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


    // public string GetText() { return questionText; }

    // public void SetText(string questionText) { questionText = questionText; }

}