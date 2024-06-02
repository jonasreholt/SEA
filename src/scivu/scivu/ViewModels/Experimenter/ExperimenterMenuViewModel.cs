using System;
using Model.Database;
using Model.Structures;
using scivu.Model;

namespace scivu.ViewModels;

public class ExperimenterMenuViewModel : ViewModelBase
{
    private readonly IDatabase _client;
    
    private readonly SurveyWrapper _survey;
    private readonly Action<string, object> _changeViewCommand;
    // The following are placeholder, should be dynamically pulled from the survey object.
    public string SurveyName { get; }
    public int SurveyId { get; }
    public int StartedSurveys { get; }
    public int FinishedSurveys { get; }
    public int CompletionRate { get; }
    public int AverageCompletionRate { get; }

    public ExperimenterMenuViewModel(IDatabase client, Action<string, object> changeViewCommand, SurveyWrapper survey)
    {
        _client = client;
        _survey = survey;
        _changeViewCommand = changeViewCommand;
        SurveyName = survey.SurveyWrapperName; // placeholder
        SurveyId = survey.PinCode; // placeholder
        StartedSurveys = 20; // placeholder
        FinishedSurveys = 15; // placeholder
        CompletionRate = 75; // placeholder
        AverageCompletionRate = 70; // placeholder
    }


    public void ChangeView(string view)
    {
        _changeViewCommand(view, view == SharedConstants.MainMenuName ? null! : _survey);

    }

    public void ExportData()
    {
        throw new NotImplementedException();
    }
}