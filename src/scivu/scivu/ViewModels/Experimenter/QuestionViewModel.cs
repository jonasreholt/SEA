using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using Avalonia.Media.Imaging;
using Model.Structures;
using ReactiveUI;

namespace scivu.ViewModels;

public class QuestionViewModel : ViewModelBase
{
    private readonly int _userId;
    private bool _foundImage = true;
    private QuestionBaseViewModel _content;

    public bool FoundImage
    {
        get => _foundImage;
        private set => this.RaiseAndSetIfChanged(ref _foundImage, value);
    }
    public Bitmap? Image { get; }
    public string Caption { get; }
    public AnswerType Type { get; }

    public ObservableCollection<QuestionBaseViewModel> Content { get; } = new();

    public QuestionViewModel(int userId, Question question)
    {
        _userId = userId;
        
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
        

        FillContent(question.SubQuestions);
    }

    private void FillContent(List<SubQuestion> qs)
    {
        foreach (var q in qs)
        {
            var result = q.Results.GetValueOrDefault(_userId);
            Content.Add(q.Answer.AnswerType switch
            {
                AnswerType.Scale => new ScaleQuestionViewModel(q, result),
                AnswerType.Text => new TextQuestionViewModel(q, result),
                AnswerType.MultipleChoice => new MultiQuestionViewModel(q, result),
                _ => throw new ArgumentException($"'{q.Answer.AnswerType}' is not implemented")
            });
        }
    }

    public void SaveResult()
    {
        foreach (var vm in Content)
        {
            vm.SaveResult(_userId);
        }
    }
}