using System.Windows.Input;
using ReactiveUI;
using scivu.Models;

namespace scivu.ViewModels;

public class MainMenuViewModel : ViewModelBase
{
    private IReadSurvey _survey;
    public MainMenuViewModel(IReadSurvey survey)
    {
        _survey = survey;
    }
}