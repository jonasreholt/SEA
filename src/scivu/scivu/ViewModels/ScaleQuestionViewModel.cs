using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using Avalonia.Media.Imaging;
using Model.Answer;
using Model.Question;
using ReactiveUI;
using scivu.Model;

namespace scivu.ViewModels;

public class ScaleQuestionViewModel : ViewModelBase
{
    private static int _groupName;

    private bool _foundImage = true;

    public bool FoundImage
    {
        get => _foundImage;
        private set => this.RaiseAndSetIfChanged(ref _foundImage, value);
    }
    public Bitmap? Image { get; }
    public string Caption { get; }
    public string Text { get; }



    public ObservableCollection<ScaleViewModel> Buttons { get; } = new();

    public ScaleQuestionViewModel(IReadOnlyQuestion question)
    {
        if (!string.IsNullOrEmpty(question.ReadOnlyPicture))
        {
            if (File.Exists(question.ReadOnlyPicture))
            {
                Image = new Bitmap(question.ReadOnlyPicture);
            }
            else
            {
                Debug.WriteLine($"Could not find file `{question.ReadOnlyPicture}`");

                // Display Debug image
                FoundImage = false;

                Image = null;
            }
        }
        else
        {
            Image = null;
        }

        Caption = question.ReadOnlyCaption;
        Text = question.ReadOnlyText;

        var answer = question.ReadOnlyAnswer;
        Debug.Assert(answer.ReadOnlyAnswerType == AnswerType.Scale);
        var answers = answer.ReadOnlyAnswers;
        Debug.Assert(answers.Count == 2);

        if (!Int32.TryParse(answers[0], out var min)) throw new ArgumentException("Minimum value");
        if (!Int32.TryParse(answers[1], out var max)) throw new ArgumentException("Max value");

        Debug.Assert(SharedConstants.ScaleMinimumValue <= min && min < max);
        _groupName++;
        for (; min <= max; min++)
        {
            var svm = new ScaleViewModel(_groupName.ToString(), min.ToString());
            Buttons.Add(svm);
        }
    }
}