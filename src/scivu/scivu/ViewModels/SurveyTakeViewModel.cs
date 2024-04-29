using System.Collections.ObjectModel;
using ReactiveUI;

namespace scivu.ViewModels;

public class SurveyTakeViewModel : ViewModelBase
{
    public ObservableCollection<SurveyViewModel> Surveys { get; } = new();

    private bool _isFirstPage;
    private bool _isLastPage;
    

    public SurveyTakeViewModel()
    {
        
    }

    public bool IsFirstQuestion
    {
        get => _isFirstPage;
        set => this.RaiseAndSetIfChanged(ref _isFirstPage, value);
    }

    public bool IsLastPage
    {
        get => _isLastPage;
        set => this.RaiseAndSetIfChanged(ref _isLastPage, value);
    }
}