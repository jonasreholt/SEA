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
            Console.WriteLine("Hej from yesh");
            return (object?)true;
        });
        NoCommand = ReactiveCommand.Create(() =>
        {
            Console.WriteLine("Hej from no");
            return (object?)false;
        });
    }
}