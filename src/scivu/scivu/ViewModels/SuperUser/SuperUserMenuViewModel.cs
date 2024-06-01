using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model.Structures;
using scivu.Model;

namespace scivu.ViewModels.SuperUser;

public class SuperUserMenuViewModel : ViewModelBase
{
    private Action<string, object> _changeViewCommand;
    public ObservableCollection<SurveyViewModel> Surveys { get; } = new();
    
    public SuperUserMenuViewModel(Action<string, object> changeViewCommand)
    {
        _changeViewCommand = changeViewCommand;
    }

    public void Setup(List<SurveyWrapper> surveys)
    {
        foreach (var survey in surveys)
        {
            Surveys.Add(new SurveyViewModel(DeleteCallback, ModifyCallback, survey));
        }
    }

    private void DeleteCallback(SurveyViewModel surveyToDelete)
    {
        Surveys.Remove(surveyToDelete);
    }

    private void ModifyCallback(SurveyViewModel surveyToModify)
    {
        _changeViewCommand(SharedConstants.ModifySurveyName, surveyToModify.SurveyWrapper);
    }
}