﻿using System;
using System.Reactive;
using ReactiveUI;
using Model.FrontEndAPI;
using Model.Factory;
using Model.Structures;


namespace scivu.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    internal ViewModelBase _contentViewModel;
    internal SurveyTakeViewModel _surveyTaker;
    private readonly IFrontEndExperimenter _experimenterClient;

    private readonly IFrontEndMainMenu _mainMenuClient;

    public ReactiveCommand<string, Unit> Change { get; }

    public MainWindowViewModel()
    {
        _mainMenuClient = FrontEndFactory.CreateMainMenu();
        _experimenterClient = FrontEndFactory.CreateExperimenterMenu();

        // We cache our survey taker both to avoid creating a new version
        // at each survey start, but also for the workaround with opening
        // a dialog option
        _surveyTaker = new SurveyTakeViewModel(_experimenterClient, ChangeViewTo);

        Change = ReactiveCommand.Create<string>(ChangeViewTo);

        _mainMenuClient = FrontEndFactory.CreateMainMenu();

        _contentViewModel = new MainMenuViewModel(ChangeViewTo, _mainMenuClient);
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
            case "TakeSurvey" when arg is SurveyWrapper survey:
                _surveyTaker.StartNewSurvey(survey, 42);
                ContentViewModel = _surveyTaker;
                break;
            case "ExperimenterMenu" when arg is SurveyWrapper survey:
                ContentViewModel = new ExperimenterMenuViewModel(_experimenterClient, ChangeViewTo, survey);
                break;
            case "MainMenu":
                ContentViewModel = new MainMenuViewModel(ChangeViewTo, _mainMenuClient);
                break;
            case "PauseMenu" when arg is SurveyWrapper survey:
                ContentViewModel = new PauseMenuViewModel(ChangeViewTo, survey);
                break;
            case "SuperUserMenu":
                throw new NotImplementedException("Changing to super user menu");
            default:
                throw new ArgumentException($"Invalid view model `{vm}` with invalid argument `{arg}`");
        }
    }
}