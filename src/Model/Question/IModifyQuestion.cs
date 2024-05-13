namespace Question;

using System.Drawing;

public interface IModifyQuestion {
    void SetType(QuestionType questionType);
    // string SetCaption
    // Image SetPicture
    void SetText(string questionText);
}