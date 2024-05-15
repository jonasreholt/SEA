using System;
using FrontEndAPI;

namespace scivu.ViewModels;

public class ExperimenterMenuViewModel : ViewModelBase
{
    private readonly IGetSurvey _survey;
    private readonly Action<string, object> _changeViewCommand;
    // The following are placeholder, should be dynamically pulled from the survey object.
    public string SurveyName { get; }
    public int StartedSurveys { get; }
    public int FinishedSurveys { get; }
    public int CompletionRate { get; }
    public int AverageCompletionRate { get; }

    public ExperimenterMenuViewModel(IGetSurvey survey, Action<string, object> changeViewCommand)
    {
        _survey = survey;
        _changeViewCommand = changeViewCommand;
        SurveyName = "Survey 1"; // placeholder
        StartedSurveys = 20; // placeholder
        FinishedSurveys = 15; // placeholder
        CompletionRate = 75; // placeholder
        AverageCompletionRate = 70; // placeholder
    }


    public void ChangeView(string view)
    {
        _changeViewCommand(view, null!);
    }







}