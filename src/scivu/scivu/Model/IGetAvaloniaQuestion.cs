using Avalonia.Media.Imaging;
using Question;

namespace scivu.Model;

/// This class is used to for easier handling of get questions specifically with
/// Avalonia bitmap imaging.
public interface IGetAvaloniaQuestion
{
    int GetId {get;}
    QuestionType GetType {get;}
    string GetCaption {get;}
    Bitmap GetPicture {get;}
    string GetText {get;}
}