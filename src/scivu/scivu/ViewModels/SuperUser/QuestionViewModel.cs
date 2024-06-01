using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Model.Structures;
using Avalonia.Media.Imaging;
using DynamicData;
using ReactiveUI;
using scivu.Model;

namespace scivu.ViewModels.SuperUser;

public class QuestionViewModel : ViewModelBase
{
    private readonly Action<QuestionViewModel> _deleteCallback;
    internal Question Question;

    private bool _imageChosen;

    private string _caption;
    private Bitmap? _image;
    private string _imagePath;

    public ObservableCollection<SubQuestionViewModel> SubQuestions { get; } = new();
    
    public QuestionViewModel(Action<QuestionViewModel> deleteCallback, Question question)
    {
        _deleteCallback = deleteCallback;
        Question = question;
        
        Caption = question.Caption;
        TrySetImage(question.PicturePath);
        SetSubQuestions(question);
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

    public void Save()
    {
        Question.Caption = Caption;
        Question.PicturePath = _imagePath;
        foreach (var subquestion in SubQuestions)
        {
            subquestion.Save();
        }
    }

    public void AddSubQuestion(string variant)
    {
        SubQuestionViewModel vm;
        switch (variant)
        {
            case "scale":
            {
                var answer = new Answer(AnswerType.Scale);
                answer.AddAnswerOption("1");
                answer.AddAnswerOption("2");
                var sq = new SubQuestion(String.Empty, answer);
                vm = new SubQuestionViewModel(DeleteSubQuestion, sq);
                break;
            }
            case "multi":
            {
                var answer = new Answer(AnswerType.MultipleChoice);
                var sq = new SubQuestion(string.Empty, answer);
                vm = new SubQuestionViewModel(DeleteSubQuestion, sq);
                break;
            }
            case "text":
            {
                var answer = new Answer(AnswerType.Text);
                var sq = new SubQuestion(String.Empty, answer);
                vm = new SubQuestionViewModel(DeleteSubQuestion, sq);
                break;
            }
            default:
                throw new ArgumentException(variant);
        }
        
        SubQuestions.Add(vm);
    }
    
    private void DeleteSubQuestion(SubQuestionViewModel question)
    {
        SubQuestions.Remove(question);
        Question.SubQuestions.Remove(question.SubQuestion);
    }

    public void DeleteQuestion()
    {
        _deleteCallback(this);
    }

    public void DeleteImage()
    {
        Debug.Assert(ImageChosen);
        
        _imagePath = string.Empty;
        ImageChosen = false;
    }

    public async void AddImage()
    {
        var file = await FileExplorer.OpenImageAsync();
        if (file != null)
        {
            TrySetImage(file.Path.AbsolutePath);
        }
    }

    private void TrySetImage(string picturePath)
    {
        if (!string.IsNullOrEmpty(picturePath))
        {
            ImageChosen = true;
            _imagePath = picturePath;

            if (File.Exists(picturePath))
            {
                Image = new Bitmap(picturePath);
            }
            else
            {
                Debug.WriteLine($"Could not find file `{picturePath}`");

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

    private void SetSubQuestions(Question question)
    {
        foreach (var subQuestion in question.SubQuestions)
        {
            SubQuestions.Add(new SubQuestionViewModel(DeleteSubQuestion, subQuestion));
        }
    }
}