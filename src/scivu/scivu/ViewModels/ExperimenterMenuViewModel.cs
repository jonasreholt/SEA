using System;
using Model.FrontEndAPI;
using Model.Survey;

namespace scivu.ViewModels;

public class ExperimenterMenuViewModel : ViewModelBase
{
    private readonly IReadOnlySurveyWrapper _survey;
    private readonly Action<string, object> _changeViewCommand;
    // The following are placeholder, should be dynamically pulled from the survey object.
    public string SurveyName { get; }
    public int SurveyId { get; }
    public int StartedSurveys { get; }
    public int FinishedSurveys { get; }
    public int CompletionRate { get; }
    public int AverageCompletionRate { get; }

    public ExperimenterMenuViewModel(IReadOnlySurveyWrapper survey, Action<string, object> changeViewCommand)
    {
        _survey = survey;
        _changeViewCommand = changeViewCommand;
        SurveyName = survey.SurveyWrapperName; // placeholder
        SurveyId = survey.SurveyWrapperId; // placeholder
        StartedSurveys = 20; // placeholder
        FinishedSurveys = 15; // placeholder
        CompletionRate = 75; // placeholder
        AverageCompletionRate = 70; // placeholder
    }


    public void ChangeView(string view)
    {
        _changeViewCommand(view, view == "MainMenu" ? null! : _survey);

    }

    public void StartSurveyCommand()
    {
        _changeViewCommand("TakeSurvey", _survey);
    }







}