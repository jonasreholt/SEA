using System;
using System.Reactive;
using ReactiveUI;

namespace scivu.ViewModels;

public class ExitSurveyViewModel
{
    public ReactiveCommand<Unit, object?> YesCommand { get; }
    public ReactiveCommand<Unit, object?> NoCommand { get; }

    public ExitSurveyViewModel()
    {
        YesCommand = ReactiveCommand.Create(() =>
        {
            return (object?)true;
        });
        NoCommand = ReactiveCommand.Create(() =>
        {
            return (object?)false;
        });
    }
}