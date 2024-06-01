using System;
using System.Collections.ObjectModel;
using Model.Structures;
using scivu.Model;

namespace scivu.ViewModels.SuperUser;

public class SurveyWrapperModifyViewModel : ViewModelBase
{
    private readonly Action<string, object> _changeViewCommand;
    private SurveyWrapper _sw;

    public ObservableCollection<SurveyVersionViewModel> Versions { get; } = new();
    
    public SurveyWrapperModifyViewModel(Action<string, object> changeViewCommand)
    {
        _changeViewCommand = changeViewCommand;
    }

    public void Setup(SurveyWrapper sw)
    {
        _sw = sw;

        Versions.Clear();
        foreach (var version in sw.SurveyVersions)
        {
            Versions.Add(new SurveyVersionViewModel(Remove, Modify, Copy, version));
        }
    }

    public string Name => _sw.SurveyWrapperName;

    public void AddVersion()
    {
        var version = new Survey();
        _sw.SurveyVersions.Add(version);
        Versions.Add(new SurveyVersionViewModel(Remove, Modify, Copy, version));
    }

    private void Remove(SurveyVersionViewModel version)
    {
        _sw.SurveyVersions.Remove(version.Sw);
        Versions.Remove(version);
    }

    private void Modify(SurveyVersionViewModel version)
    {
        Save();
        _changeViewCommand.Invoke(SharedConstants.ModifySurveyName, version.Sw);
    }

    private void Copy(SurveyVersionViewModel version)
    {
        var copy = version.Sw.Copy();
        copy.SurveyName += " (copy)";
        
        _sw.SurveyVersions.Add(copy);
        Versions.Add(new SurveyVersionViewModel(Remove, Modify, Copy, copy));
    }
    
    public void Finish()
    {
        Save();
        _changeViewCommand.Invoke(SharedConstants.SuperUserMenuName, null);
    }

    private void Save()
    {
        foreach (var version in Versions)
        {
            version.Save();
        }
    }
}