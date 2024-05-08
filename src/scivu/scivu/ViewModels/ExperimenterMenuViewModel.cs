using System.Windows.Input;
using ReactiveUI;
using scivu.Models;

namespace scivu.ViewModels;

public class ExperimenterMenuViewModel : ViewModelBase
{
    private readonly IReadSurvey _survey;

    public ExperimenterMenuViewModel(IReadSurvey survey)
    {
        _survey = survey;
    }

    public ICommand TakeSurveyCommand => ReactiveCommand.Create(TakeSurvey);

    private void TakeSurvey()
    {
        //Console.WriteLine("Taking survey");
    }
}