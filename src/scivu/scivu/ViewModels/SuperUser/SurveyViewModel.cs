using System;
using System.IO;
using Model.Database;
using Model.Structures;
using ReactiveUI;
using scivu.Model;

namespace scivu.ViewModels.SuperUser;

public class SurveyViewModel : ViewModelBase
{
    private readonly IDatabase _client;
    private readonly Action<SurveyViewModel> _deleteCallback;
    private readonly Action<SurveyViewModel> _modifyCallback;
    private readonly Action<SurveyViewModel> _copyCallback;
    
    private readonly SurveyWrapper _surveyWrapper;

    private string _pinCode;
    
    public SurveyViewModel(
        IDatabase client,
        Action<SurveyViewModel> deleteCallback, 
        Action<SurveyViewModel> modifyCallback, 
        Action<SurveyViewModel> copyCallback,
        SurveyWrapper survey)
    {
        _client = client;
        _deleteCallback = deleteCallback;
        _modifyCallback = modifyCallback;
        _copyCallback = copyCallback;
        
        _surveyWrapper = survey;

        PinCode = survey.PinCode.ToString();
    }
    
    public string Name => _surveyWrapper.SurveyWrapperName;
    public SurveyWrapper SurveyWrapper => _surveyWrapper;

    public string PinCode
    {
        get => _pinCode;
        set => this.RaiseAndSetIfChanged(ref _pinCode, value);
    }

    public void Save()
    {
        if (!Int32.TryParse(PinCode, out var pin) || PinCode.Length != 6)
        {
            throw new ArgumentException(ErrorDiagnostics.GetErrorMessage(ErrorDiagnosticsID.ERR_InvalidPIN));
        }

        _surveyWrapper.PinCode = pin;
    }

    public async void Export()
    {
        Save();
        var folder = await FileExplorer.SaveSurveyAsync();

        if (folder != null)
        {
            // Save the results next to the actual survey to not ruin the state when starting
            // a new experiment from imported survey
            _client.ExportResults(_surveyWrapper, Path.Combine(folder.Path.LocalPath, "results.csv"));
            _surveyWrapper.ClearResults();
            
            _client.Serialize(_surveyWrapper, folder.Path.LocalPath);
        }
    }

    public void Modify()
    {
        Save();
        
        // Since we are modifying the survey, results will no longer be valid
        SurveyWrapper.ClearResults();
        
        _modifyCallback.Invoke(this);
    }

    public void Copy()
    {
        Save();
        _copyCallback.Invoke(this);
    }

    public void Delete()
    {
        _deleteCallback.Invoke(this);
    }
}