namespace Question;

//using System.Drawing;

public interface IReadOnlyQuestion {
    int GetId();
    QuestionType GetType();
    //string GetCaption {get;}
    //Image GetPicture {get;}
    string GetText();
}
