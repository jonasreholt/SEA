using System;
using Model.Structures;

namespace scivu.ViewModels.SuperUser;

public class SurveyViewModel : ViewModelBase
{
    private readonly Action<SurveyViewModel> _deleteCallback;
    private readonly SurveyWrapper _surveyWrapper;
    
    public SurveyViewModel(Action<SurveyViewModel> deleteCallback, SurveyWrapper survey)
    {
        _deleteCallback = deleteCallback;
        _surveyWrapper = survey;
    }
    
    public string Name => _surveyWrapper.SurveyWrapperName;

    public void Export()
    {
        throw new NotImplementedException();
    }

    public void Modify()
    {
        throw new NotImplementedException();
    }

    public void Delete()
    {
        _deleteCallback.Invoke(this);
    }
}