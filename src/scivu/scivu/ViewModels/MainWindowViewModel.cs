using System;
using System.Collections.Generic;
using System.Reactive;
using ReactiveUI;
using Model.FrontEndAPI;
using Model.Factory;
using Model.Structures;
using scivu.Model;
using scivu.ViewModels.SuperUser;


namespace scivu.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    internal ViewModelBase _contentViewModel;
    internal SurveyTakeViewModel _surveyTaker;
    private SuperUserMenuViewModel _superUser;
    
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
        _superUser = new SuperUserMenuViewModel(ChangeViewTo);

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
            case SharedConstants.TakeSurveyName when arg is SurveyWrapper survey:
                _surveyTaker.StartNewSurvey(survey, _experimenterClient.GetUserId());
                ContentViewModel = _surveyTaker;
                break;
            case SharedConstants.ExperimenterMenuName when arg is SurveyWrapper survey:
                ContentViewModel = new ExperimenterMenuViewModel(_experimenterClient, ChangeViewTo, survey);
                break;
            case SharedConstants.MainMenuName:
                ContentViewModel = new MainMenuViewModel(ChangeViewTo, _mainMenuClient);
                break;
            case SharedConstants.PaueMenuName when arg is SurveyWrapper survey:
                ContentViewModel = new PauseMenuViewModel(ChangeViewTo, survey);
                break;
            case SharedConstants.SuperUserMenuName:
                if (arg is List<SurveyWrapper> surveys)
                {
                    _superUser.Setup(surveys);
                }
                ContentViewModel = _superUser;
                break;
            case SharedConstants.ModifySurveyName when arg is SurveyWrapper surveyWrapper:
                // TODO: This needs to be fixed big time!!
                if (!surveyWrapper.TryGetSurveyVersion(0, out var s))
                {
                    throw new UnhandledErrorException();
                }
                ContentViewModel = new SurveyModifyViewModel(ChangeViewTo, s);
                break;
            default:
                throw new ArgumentException($"Invalid view model `{vm}` with invalid argument `{arg}`");
        }
    }
}