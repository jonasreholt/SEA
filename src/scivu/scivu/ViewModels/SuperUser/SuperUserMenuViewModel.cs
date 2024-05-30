using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model.Structures;

namespace scivu.ViewModels.SuperUser;

public class SuperUserMenuViewModel : ViewModelBase
{
    public ObservableCollection<SurveyViewModel> Surveys { get; } = new();
    
    public SuperUserMenuViewModel(List<SurveyWrapper> surveys)
    {
        foreach (var survey in surveys)
        {
            Surveys.Add(new SurveyViewModel(DeleteCallback, survey));
        }
    }

    private void DeleteCallback(SurveyViewModel surveyToDelete)
    {
        Surveys.Remove(surveyToDelete);
    }
}