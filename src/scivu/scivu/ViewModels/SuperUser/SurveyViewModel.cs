using System;
using Model.Structures;

namespace scivu.ViewModels.SuperUser;

public class SurveyViewModel : ViewModelBase
{
    private readonly Action<SurveyViewModel> _deleteCallback;
    private readonly Action<SurveyViewModel> _modifyCallback;
    
    private readonly SurveyWrapper _surveyWrapper;
    
    public SurveyViewModel(
        Action<SurveyViewModel> deleteCallback, 
        Action<SurveyViewModel> modifyCallback, 
        SurveyWrapper survey)
    {
        _deleteCallback = deleteCallback;
        _modifyCallback = modifyCallback;
        
        _surveyWrapper = survey;
    }
    
    public string Name => _surveyWrapper.SurveyWrapperName;
    public SurveyWrapper SurveyWrapper => _surveyWrapper;

    public void Export()
    {
        throw new NotImplementedException();
    }

    public void Modify()
    {
        _modifyCallback.Invoke(this);
    }

    public void Delete()
    {
        _deleteCallback.Invoke(this);
    }
}