using System;
using System.Diagnostics;
using Model.FrontEndAPI;
using ReactiveUI;
using scivu.Model;
using Model.Survey;
using scivu.ViewModels;

namespace scivu.ViewModels;

public class PauseMenuViewModel : ViewModelBase
{

    private readonly Action<string, object> _changeViewCommand;
    private string? _pincode;
    private bool _isLoginEnabled;
    private bool _isLoggedIn;
    public IReadOnlySurveyWrapper Survey { get; }

    private string _errorMessage = string.Empty;

    public PauseMenuViewModel(Action<string, object> changeViewCommand, IReadOnlySurveyWrapper survey)
    {
        Survey = survey;
        _isLoggedIn = false;
        _changeViewCommand = changeViewCommand;
    }

    public bool IsLoggedIn
    {
        get => _isLoggedIn;
        set => this.RaiseAndSetIfChanged(ref _isLoggedIn, value);
    }

    public bool IsLoginEnabled
    {
        get => _isLoginEnabled;
        set => this.RaiseAndSetIfChanged(ref _isLoginEnabled, value);
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        private set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }

    public string? Pincode
    {
        get => _pincode;
        set
        {
            this.RaiseAndSetIfChanged(ref _pincode, value);
            IsLoginEnabled = EnableLoginButton();
        }
    }

    public void ChangeView(string view)
    {
        _changeViewCommand(view, Survey);
    }

    private bool EnableLoginButton()
    {
        Debug.Assert(!_isLoggedIn);
        return !string.IsNullOrWhiteSpace(Pincode)
               && Pincode.Length == SharedConstants.PinCodeLength
               && Int32.TryParse(Pincode, out _);
    }

    public async void DoLogin()
    {
        Debug.Assert(!IsLoggedIn);
        if (Int32.TryParse(Pincode, out var pin))
        {
            if (Survey.SurveyWrapperId == pin)
            {
                IsLoggedIn = true;
                return;
            }

            ErrorMessage = ErrorDiagnostics.GetErrorMessage(ErrorDiagnosticsID.ERR_PinCodeNotFound);
        }
    }
}
