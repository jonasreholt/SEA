using System;
using System.Diagnostics;
using Model.FrontEndAPI;
using ReactiveUI;
using scivu.Model;
using Model.Survey;

namespace scivu;

public class PauseMenuViewModel
{

    private readonly IReadOnlySurveyWrapper _survey;
    private readonly Action<string, object> _changeViewCommand;
    private string? _pincode;
    private bool _isLoginEnabled;
    private bool _isLoggedIn;

    private string _errorMessage = string.Empty;

    public PauseMenuViewModel(IReadOnlySurveyWrapper survey, Action<string, object> changeViewCommand)
    {
        _survey = survey;
        _isLoggedIn = false;
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

    private bool EnableLoginButton()
    {
        Debug.Assert(!_isLoggedIn);
        return !string.IsNullOrWhiteSpace(Pincode)
               && Pincode.Length == SharedConstants.PinCodeLength
               && Int32.TryParse(Pincode, out _);
    }

    private async void DoLogin()
    {
        Debug.Assert(!_isLoggedIn);

        if (Int32.TryParse(Pincode, out var pin))
        {
            // Should manually check the pin of the survey rather than trying to get a new one and see if it exists.
            var survey = FrontEndMainMenu.GetSurvey(pin);
            if (survey != null)
            {
                _isLoggedIn = true;
                return;
            }

            ErrorMessage = ErrorDiagnostics.GetErrorMessage(ErrorDiagnosticsID.ERR_PinCodeNotFound);
        }
    }
}
