using System;
using System.Reactive;
using System.Windows.Input;
using ReactiveUI;
using scivu.Models;

namespace scivu.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _contentViewModel;

    private readonly MainMenuViewModel _mainMenu;

    public SurveyViewModel Surveys { get; }

    public ReactiveCommand<string, Unit> Change { get; }
    
    public MainWindowViewModel()
    {
        Surveys = new SurveyViewModel();
        Change = ReactiveCommand.Create<string>(ChangeViewTo);
        
        _mainMenu = new MainMenuViewModel(ChangeViewTo);

        _contentViewModel = _mainMenu;
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
                throw new NotImplementedException("Changing to experimenter menu");
                break;
            case "MainMenu":
                ContentViewModel = _mainMenu;
                break;
            case "SuperUserMenu":
                throw new NotImplementedException("Changing to super user menu");
            default:
                throw new ArgumentException($"Invalid view model `{vm}` with invalid argument `{arg}`");
        }
    }
}