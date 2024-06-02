using System;
using System.Collections.Generic;
using System.Reactive;
using Model.Database;
using ReactiveUI;
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
    private SurveyWrapperModifyViewModel _surveyWrapperModifier;

    private readonly IDatabase _database;

    public ReactiveCommand<string, Unit> Change { get; }

    public MainWindowViewModel()
    {
        _database = FrontEndFactory.CreateDatabase();

        // We cache our survey taker both to avoid creating a new version
        // at each survey start, but also for the workaround with opening
        // a dialog option
        _surveyTaker = new SurveyTakeViewModel(_database, ChangeViewTo);
        _superUser = new SuperUserMenuViewModel(ChangeViewTo, _database);
        _surveyWrapperModifier = new SurveyWrapperModifyViewModel(ChangeViewTo);

        Change = ReactiveCommand.Create<string>(ChangeViewTo);

        _contentViewModel = new MainMenuViewModel(ChangeViewTo, _database);
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
                _surveyTaker.StartNewSurvey(survey, _database.GetUserId());
                ContentViewModel = _surveyTaker;
                break;
            case SharedConstants.ExperimenterMenuName when arg is SurveyWrapper survey:
                ContentViewModel = new ExperimenterMenuViewModel(_database, ChangeViewTo, survey);
                break;
            case SharedConstants.MainMenuName:
                ContentViewModel = new MainMenuViewModel(ChangeViewTo, _database);
                break;
            case SharedConstants.PaueMenuName when arg is SurveyWrapper survey:
                ContentViewModel = new PauseMenuViewModel(ChangeViewTo, survey);
                break;
            case SharedConstants.SuperUserMenuName:
                if (arg is (UserId userId, List<SurveyWrapper> surveys))
                {
                    _superUser.Setup(userId, surveys);
                }
                ContentViewModel = _superUser;
                break;
            case SharedConstants.ModifySurveyWrapperName:
                if (arg is SurveyWrapper surveyWrapper)
                {
                    _surveyWrapperModifier.Setup(surveyWrapper);
                }
                ContentViewModel = _surveyWrapperModifier;
                break;
            case SharedConstants.ModifySurveyName when arg is Survey survey:
                ContentViewModel = new SurveyModifyViewModel(ChangeViewTo, survey);
                break;
            default:
                throw new ArgumentException($"Invalid view model `{vm}` with invalid argument `{arg}`");
        }
    }
}