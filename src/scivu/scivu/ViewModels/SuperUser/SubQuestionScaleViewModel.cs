using System;
using ReactiveUI;
using Model.Structures;
using scivu.Model;

namespace scivu.ViewModels.SuperUser;

public class SubQuestionScaleViewModel : SubQuestionBaseViewModel
{
    private SubQuestion _question;
    
    private string _text;
    private string _min;
    private string _max;

    public SubQuestionScaleViewModel(SubQuestion question)
    {
        _question = question;
        
        _text = question.QuestionText;

        var answers = question.Answer.AnswerOptions;
        if (answers.Count != 2)
        {
            throw new ArgumentException(ErrorDiagnostics.GetErrorMessage(ErrorDiagnosticsID.ERR_ScaleRangeInvalid));
        }

        ValidateInput(answers[0], answers[1]);

        // Now we know they are actual ints
        _min = answers[0];
        _max = answers[1];
    }

    public void ValidateInput(string arg1, string arg2)
    {
        var min = GetValue(arg1);
        var max = GetValue(arg2);
        
        if (!(SharedConstants.ScaleMinimumValue <= min && min < max && max-min < SharedConstants.ScaleMaxRange))
        {
            throw new ArgumentException(ErrorDiagnostics.GetErrorMessage(ErrorDiagnosticsID.ERR_ScaleRangeInvalid));
        }
    }

    private int GetValue(string arg)
    {
        if (!Int32.TryParse(arg, out var val))
        {
            throw new ArgumentException(ErrorDiagnostics.GetErrorMessage(ErrorDiagnosticsID.ERR_ScaleRangeNotInt));
        }

        return val;
    }

    public string Text
    {
        get => _text;
        set => this.RaiseAndSetIfChanged(ref _text, value);
    }

    public string Min
    {
        get => _min;
        set => this.RaiseAndSetIfChanged(ref _min, value);
    }

    public string Max
    {
        get => _max;
        set => this.RaiseAndSetIfChanged(ref _max, value);
    }

    public override void Save()
    {
        ValidateInput(Min, Max);
        
        _question.QuestionText = Text;
        _question.Answer.AnswerOptions.Clear();
        _question.Answer.AddAnswerOption(Min);
        _question.Answer.AddAnswerOption(Max);
    }
}