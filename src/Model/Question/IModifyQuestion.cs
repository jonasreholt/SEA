namespace Question;

using System.Drawing;

public interface IModifyQuestion {
QuestionType SetType {set;}
string SetCaption {set;}
Image SetPicture {set;}
string SetText {set;}

}