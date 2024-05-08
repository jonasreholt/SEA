using System.Collections.ObjectModel;
using Avalonia.Media.Imaging;

namespace scivu.ViewModels;

public class ScaleQuestionViewModel : ViewModelBase
{
    public Bitmap? Image { get; }
    public string Caption { get; } = "Placeholder";
    public string Text { get; } = "Placeholder";

    public ObservableCollection<ScaleViewModel> Buttons { get; } = new();

    public ScaleQuestionViewModel(Bitmap? image, string caption, string text, ScaleViewModel[] buttons)
    {
        Image = image;
        Caption = caption;
        Text = text;
        foreach (var button in buttons)
        {
            Buttons.Add(button);
        }
    }
}