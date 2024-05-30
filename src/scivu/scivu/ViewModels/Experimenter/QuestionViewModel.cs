using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Avalonia.Media.Imaging;
using Model.Structures;
using ReactiveUI;

namespace scivu.ViewModels;

public class QuestionViewModel : ViewModelBase
{
    private bool _foundImage = true;
    private QuestionBaseViewModel _content;

    private string _text;

    public bool FoundImage
    {
        get => _foundImage;
        private set => this.RaiseAndSetIfChanged(ref _foundImage, value);
    }
    public int Id { get; }
    public Bitmap? Image { get; }
    public string Caption { get; }
    public AnswerType Type { get; }

    public QuestionBaseViewModel Content
    {
        get => _content;
        private set => this.RaiseAndSetIfChanged(ref _content, value);
    }

    public QuestionViewModel(Question question)
    {
        if (!string.IsNullOrEmpty(question.PicturePath))
        {
            if (File.Exists(question.PicturePath))
            {
                Image = new Bitmap(question.PicturePath);
            }
            else
            {
                Debug.WriteLine($"Could not find file `{question.PicturePath}`");

                // Display Debug image
                FoundImage = false;

                Image = null;
            }
        }
        else
        {
            Image = null;
        }

        Caption = question.Caption;
        _text = question.QuestionText;
        

        FillContent(question.Answer);
    }

    private void FillContent(Answer answer)
    {
        Content = answer.ReadOnlyAnswerType switch
        {
            AnswerType.Scale => new ScaleQuestionViewModel(_text, answer.ReadOnlyAnswers),
            AnswerType.Text => new TextQuestionViewModel(_text),
            AnswerType.MultipleChoice => new MultiQuestionViewModel(_text, answer.ReadOnlyAnswers),
            _ => throw new ArgumentException($"'{answer.ReadOnlyAnswerType}' is not implemented")
        };
    }

    public List<string> GetResult()
    {
        return Content.GetAnswer();
    }

    public void SetResult(List<string> result)
    {
        Content.SetResult(result);
    }
}