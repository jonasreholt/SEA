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
        Surveys.Clear();
        foreach (var survey in surveys)
        {
            Surveys.Add(new SurveyViewModel(DeleteCallback, ModifyCallback, CopyCallback, survey));
        }
    }

    private bool VerifyPinCodes()
    {
        var seen = new HashSet<int>();
        foreach (var surveyViewModel in Surveys)
        {
            var pin = surveyViewModel.SurveyWrapper.PinCode;
            if (seen.Contains(pin))
            {
                return false;
            }
            seen.Add(pin);
        }

        return true;
    }

    private void DeleteCallback(SurveyViewModel surveyToDelete)
    {
        Surveys.Remove(surveyToDelete);
    }

    private void ModifyCallback(SurveyViewModel surveyToModify)
    {
        Save();
        if (!VerifyPinCodes())
        {
            throw new ArgumentException(ErrorDiagnostics.GetErrorMessage(ErrorDiagnosticsID.ERR_DuplicatePIN));
        }
        _changeViewCommand(SharedConstants.ModifySurveyWrapperName, surveyToModify.SurveyWrapper);
    }

    private void CopyCallback(SurveyViewModel surveyToCopy)
    {
        var copy = surveyToCopy.SurveyWrapper.Copy(123456);
        copy.SurveyWrapperName += " (copy)";
        Surveys.Add(new SurveyViewModel(DeleteCallback, ModifyCallback, CopyCallback, copy));
    }

    public void Logout()
    {
        Save();
        if (!VerifyPinCodes())
        {
            throw new ArgumentException(ErrorDiagnostics.GetErrorMessage(ErrorDiagnosticsID.ERR_DuplicatePIN));
        }
        _changeViewCommand.Invoke(SharedConstants.MainMenuName, null!);
    }

    public void AddSurveyWrapper()
    {
        var s = new SurveyWrapper(123456);
        Surveys.Add(new SurveyViewModel(DeleteCallback, ModifyCallback, CopyCallback, s));
    }

    private void Save()
    {
        foreach (var s in Surveys)
        {
            s.Save();
        }
    }
}