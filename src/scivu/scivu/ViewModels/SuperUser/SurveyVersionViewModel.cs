using System;
using Model.Structures;
using ReactiveUI;

namespace scivu.ViewModels.SuperUser;

public class SurveyVersionViewModel : ViewModelBase
{
    private readonly Action<SurveyVersionViewModel> _deleteCallback;
    private readonly Action<SurveyVersionViewModel> _modifyCallback;
    private readonly Action<SurveyVersionViewModel> _copyCallback;
    
    internal readonly Survey Sw;
    private string _name;

    public SurveyVersionViewModel(
        Action<SurveyVersionViewModel> deleteCallback,
        Action<SurveyVersionViewModel> modifyCallback,
        Action<SurveyVersionViewModel> copyCallback,
        Survey sw)
    {
        _deleteCallback = deleteCallback;
        _modifyCallback = modifyCallback;
        _copyCallback = copyCallback;
        
        Sw = sw;
        Name = sw.SurveyName;
    }

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public void Delete()
    {
        _deleteCallback.Invoke(this);
    }

    public void Modify()
    {
        _modifyCallback.Invoke(this);
    }

    public void Copy()
    {
        _copyCallback.Invoke(this);       
    }

    public void Save()
    {
        Sw.SurveyName = Name;
    }
}