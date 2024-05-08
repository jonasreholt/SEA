using System;
using System.Windows.Input;
using DynamicData;
using ReactiveUI;
using scivu.Models;

namespace scivu.ViewModels;

public class ExperimenterMenuViewModel : ViewModelBase
{
    private readonly IReadSurvey _survey;
    private readonly Action<string, object> _changeViewCommand;

    public ExperimenterMenuViewModel(IReadSurvey survey, Action<string, object> changeViewCommand)
    {
        _survey = survey;
        _changeViewCommand = changeViewCommand;
    }

    public void ChangeView(string view)
    {
        _changeViewCommand(view, null!);
    }







}