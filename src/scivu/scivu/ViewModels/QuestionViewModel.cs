using System;
using System.Diagnostics;
using System.IO;
using Avalonia.Media.Imaging;
using Model.Answer;
using Model.Question;
using ReactiveUI;

namespace scivu.ViewModels;

public class QuestionViewModel : ViewModelBase
{
    private bool _foundImage = true;
    private QuestionBaseViewModel _content;

    public bool FoundImage
    {
        get => _foundImage;
        private set => this.RaiseAndSetIfChanged(ref _foundImage, value);
    }
    public int Id { get; }
    public Bitmap? Image { get; }
    public string Caption { get; }
    public string Text { get; }
    public AnswerType Type { get; }

    public QuestionBaseViewModel Content
    {
        get => _content;
        private set => this.RaiseAndSetIfChanged(ref _content, value);
    }

    public QuestionViewModel(IReadOnlyQuestion question)
    {
        Id = question.QuestionId;
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

        FillContent(question.ReadOnlyAnswer);
    }

    private void FillContent(IReadOnlyAnswer answer)
    {
        Content = answer.ReadOnlyAnswerType switch
        {
            AnswerType.Scale => new ScaleQuestionViewModel(answer.ReadOnlyAnswers),
            AnswerType.Text => throw new NotImplementedException(),
            AnswerType.MultipleChoice => throw new NotImplementedException(),
            _ => throw new ArgumentException($"'{answer.ReadOnlyAnswerType}' is not implemented")
        };
    }

    public string GetResult()
    {
        return Content.GetAnswer();
    }

    public void SetResult(string result)
    {
        Content.SetResult(result);
    }
}