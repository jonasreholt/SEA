using System;
using System.Reactive;
using System.Windows.Input;
using ReactiveUI;

namespace scivu.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _contentViewModel;

    public SurveyViewModel Surveys { get; }

    public ReactiveCommand<string, Unit> Change { get; }
    
    public MainWindowViewModel()
    {
        _contentViewModel = new MainMenuViewModel();
        Surveys = new SurveyViewModel();
        Change = ReactiveCommand.Create<string>(ChangeViewTo);
    }

    public ViewModelBase ContentViewModel
    {
        get => _contentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }

    public void ChangeViewTo(string vm)
    {
        Console.WriteLine($"Going to view `{vm}`");
        switch (vm)
        {
            case "TakeSurvey":
                ContentViewModel = new SurveyTakeViewModel();
                break;
            case "MainMenu":
                ContentViewModel = new MainMenuViewModel();
                break;
            default:
                throw new ArgumentException($"Invalid argument `{vm}`");
        }
    }
}