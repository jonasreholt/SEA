using System;
using System.Reactive;
using System.Windows.Input;
using ReactiveUI;
using scivu.Models;

namespace scivu.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _contentViewModel;

    private readonly LoginMenuViewModel _loginMenu;

    public SurveyViewModel Surveys { get; }

    public ReactiveCommand<string, Unit> Change { get; }
    
    public MainWindowViewModel()
    {
        Surveys = new SurveyViewModel();
        Change = ReactiveCommand.Create<string>(ChangeViewTo);
        
        _loginMenu = new LoginMenuViewModel(ChangeViewTo);

        _contentViewModel = _loginMenu;
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
            case "MainMenu" when arg is IReadSurvey survey:
                ContentViewModel = new MainMenuViewModel(survey);
                break;
            case "LoginMenu":
                ContentViewModel = _loginMenu;
                break;
            case "SuperUserMenu":
                throw new NotImplementedException("Changing to super user menu");
            default:
                throw new ArgumentException($"Invalid view model `{vm}` with invalid argument `{arg}`");
        }
    }
}