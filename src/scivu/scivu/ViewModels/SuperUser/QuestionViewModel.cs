using System.Diagnostics;
using System.IO;
using Model.Structures;
using Avalonia.Media.Imaging;
using ReactiveUI;

namespace scivu.ViewModels.SuperUser;

public class QuestionViewModel : ViewModelBase
{
    private readonly Question _question;

    private bool _imageChosen;

    private string _caption;
    private Bitmap? _image;
    
    public QuestionViewModel(Question question)
    {
        _question = question;

        Caption = question.Caption;
        
        if (!string.IsNullOrEmpty(question.PicturePath))
        {
            ImageChosen = true;
            
            if (File.Exists(question.PicturePath))
            {
                Image = new Bitmap(question.PicturePath);
            }
            else
            {
                Debug.WriteLine($"Could not find file `{question.PicturePath}`");

                // Display Debug image
                Image = null;
            }
        }
        else
        {
            ImageChosen = false;
            Image = null;
        }
    }

    public bool ImageChosen
    {
        get => _imageChosen;
        private set => this.RaiseAndSetIfChanged(ref _imageChosen, value);
    }

    public string Caption
    {
        get => _caption;
        set => this.RaiseAndSetIfChanged(ref _caption, value);
    }

    public Bitmap? Image
    {
        get => _image;
        set => this.RaiseAndSetIfChanged(ref _image, value);
    }
}