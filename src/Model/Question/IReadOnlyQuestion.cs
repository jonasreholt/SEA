namespace Question;

using System.Drawing;

public interface IReadOnlyQuestion {
int GetId {get;}
QuestionType GetType {get;}
string GetCaption {get;}
Image GetPicture {get;}
string GetText {get;}
}
