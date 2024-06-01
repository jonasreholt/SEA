using Model.Structures;
using ReactiveUI;

namespace scivu.ViewModels.SuperUser;

public class SubQuestionTextViewModel : SubQuestionBaseViewModel
{
    private SubQuestion _question;
    private string _questionText;
        
    public SubQuestionTextViewModel(SubQuestion question)
    {
        _question = question;
        _questionText = question.QuestionText;
    }

    public string QuestionText
    {
        get => _questionText;
        set => this.RaiseAndSetIfChanged(ref _questionText, value);
    }

    public override void Save()
    {
        _question.QuestionText = QuestionText;
    }
}