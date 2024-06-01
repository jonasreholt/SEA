using System;
using Model.Structures;
using ReactiveUI;

namespace scivu.ViewModels.SuperUser;

public class SubQuestionViewModel : ViewModelBase
{
    internal readonly SubQuestion SubQuestion;
    private readonly Action<SubQuestionViewModel> _deleteCallback;
    private SubQuestionBaseViewModel _vm;
    
    public SubQuestionViewModel(Action<SubQuestionViewModel> deleteCallback, SubQuestion subQuestion)
    {
        _deleteCallback = deleteCallback;
        SubQuestion = subQuestion;

        switch (subQuestion.Answer.ModifyAnswerType)
        {
            case AnswerType.Scale:
                Question = new SubQuestionScaleViewModel(subQuestion);
                break;
            case AnswerType.Text:
                Question = new SubQuestionTextViewModel(subQuestion);
                break;
            case AnswerType.MultipleChoice:
                Question = new SubQuestionMultiViewModel(subQuestion);
                break;
            default:
                throw new ArgumentException(nameof(subQuestion.Answer.ModifyAnswerType));
        }
    }

    public SubQuestionBaseViewModel Question
    {
        get => _vm;
        private set => this.RaiseAndSetIfChanged(ref _vm, value);
    }

    public void Delete()
    {
        _deleteCallback(this);
    }

    public void Save()
    {
        _vm.Save();
    }
}