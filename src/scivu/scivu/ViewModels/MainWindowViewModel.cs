using System;
using System.Reactive;
using System.Windows.Input;
using ReactiveUI;
using scivu.Models;
using scivu.Views;

namespace scivu.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _contentViewModel;

    public SurveyViewModel Surveys { get; }

    public ReactiveCommand<string, Unit> Change { get; }

    public MainWindowViewModel()
    {
        Surveys = new SurveyViewModel();
        Change = ReactiveCommand.Create<string>(ChangeViewTo);


        _contentViewModel = new MainMenuViewModel(ChangeViewTo);
    }

    public ViewModelBase ContentViewModel
    {
        get => _contentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }

    public void ChangeViewTo(string vm) => ChangeViewTo(vm, null);

    public void ChangeViewTo(string vm, object? arg)
    {
        Console.WriteLine($"Going to view `{vm}`");
        switch (vm)
        {
            case "TakeSurvey":
                ContentViewModel = new SurveyTakeViewModel();
                break;
            case "ExperimenterMenu" when arg is IReadSurvey survey:
                ContentViewModel = new ExperimenterMenuViewModel(survey);
                break;
            case "MainMenu":
                ContentViewModel = new MainMenuViewModel(ChangeViewTo);
                break;
            case "SuperUserMenu":
                throw new NotImplementedException("Changing to super user menu");
            default:
                throw new ArgumentException($"Invalid view model `{vm}` with invalid argument `{arg}`");
        }
    }
}