using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using DynamicData;
using Model.Structures;
using ReactiveUI;

namespace scivu.ViewModels.SuperUser;

public class SubQuestionMultiViewModel : SubQuestionBaseViewModel
{
    private SubQuestion _question;

    private string _questionText;
    public ObservableCollection<TextBox> Toggles { get; } = new();
    
    public SubQuestionMultiViewModel(SubQuestion question)
    {
        _question = question;
        
        _questionText = question.QuestionText;
        
        foreach (var answer in question.Answer.AnswerOptions)
        {
            Toggles.Add(new TextBox
            {
                Text = answer
            });
        }
    }

    public string QuestionText
    {
        get => _questionText;
        set => this.RaiseAndSetIfChanged(ref _questionText, value);
    }

    public void AddToggle()
    {
        Toggles.Add(new TextBox());
    }

    public void DeleteToggle()
    {
        Toggles.RemoveAt(Toggles.Count-1);
    }

    public override void Save()
    {
        _question.QuestionText = QuestionText;

        _question.Answer.AnswerOptions.Clear();
        foreach (var toggle in Toggles)
        {
            _question.Answer.AddAnswerOption(toggle.Text!);
        }
    }
}