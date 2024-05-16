using System.Collections.ObjectModel;
using System.Diagnostics;
using Avalonia.Media.Imaging;
using Question;
using scivu.Model;

namespace scivu.ViewModels;

public class ScaleQuestionViewModel : ViewModelBase
{
    public Bitmap? Image { get; }
    public string Caption { get; }
    public string Text { get; }

    public ObservableCollection<ScaleViewModel> Buttons { get; } = new();

    public ScaleQuestionViewModel(IGetAvaloniaQuestion question, ScaleViewModel[] buttons)
    {
        Debug.Assert(question.GetType == QuestionType.Question,
            "Handling multiple questions should not be done at this level");

        Image = question.GetPicture;
        Caption = question.GetCaption;
        Text = question.GetText;
        foreach (var button in buttons)
        {
            Buttons.Add(button);
        }
    }
}