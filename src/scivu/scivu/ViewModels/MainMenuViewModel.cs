using System;
using System.Threading.Tasks;
using ReactiveUI;
using scivu.Models;

namespace scivu.ViewModels;

public class MainMenuViewModel : ViewModelBase
{
    private readonly ILoginManager _loginManager;
    private readonly Action<string, object> _changeViewCommand;

    private string? _username;
    private string? _password;
    
    private bool _isFirstTry;
    private bool _isExperimenterLogin;
    private bool _isSuperLogin;
    
    
    public MainMenuViewModel(Action<string, object> changeViewCommand)
    {
        _isFirstTry = true;
        _isExperimenterLogin = false;
        _isSuperLogin = false;

        _loginManager = new LoginMock();
        _changeViewCommand = changeViewCommand;
    }

    public string? Username
    {
        get => _username;
        set => this.RaiseAndSetIfChanged(ref _username, value);
    }

    public string? Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }

    public bool IsFirstTry
    {
        get => _isFirstTry;
        private set => this.RaiseAndSetIfChanged(ref _isFirstTry, value);
    }

    public bool IsExperimenterLogin
    {
        get => _isExperimenterLogin;
        private set => this.RaiseAndSetIfChanged(ref _isExperimenterLogin, value);
    }

    public bool IsSuperLogin
    {
        get => _isSuperLogin;
        private set => this.RaiseAndSetIfChanged(ref _isSuperLogin, value);
    }

    public bool IsLogin => IsExperimenterLogin || IsSuperLogin;

    public void GoToLogin(bool isSuperUser)
    {
        if (isSuperUser) IsSuperLogin = true;
        else IsExperimenterLogin = true;
        IsFirstTry = true;
        
        // Need to raise that fields under IsLogin has changed!
        this.RaisePropertyChanged(nameof(IsLogin));
    }

    public void GoBackToMenu()
    {
        IsSuperLogin = false;
        IsExperimenterLogin = false;
        IsFirstTry = true;

        Username = null;
        Password = null;
        
        // Need to raise that fields under IsLogin has changed!
        this.RaisePropertyChanged(nameof(IsLogin));
    }

    public async void DoLogin()
    {
        if (string.IsNullOrWhiteSpace(Password))
        {
            IsFirstTry = false;
            return;
        }

        var trySuperUser = Username != null;
        var result = false;
        var survey = default(IReadSurvey?);
        if (trySuperUser)
        {
            result = await _loginManager.Login(Username!, Password);
        }
        else if (Int32.TryParse(Password, out var pin))
        {
            (result, survey) = await _loginManager.GetSurvey(pin);
        }

        if (result)
        {
            // do the login thingy somehow
            if (trySuperUser) _changeViewCommand.Invoke("SuperUserMenu", null);
            else  _changeViewCommand.Invoke("ExperimenterMenu", survey!);
            return;
        }

        IsFirstTry = false;
    }
}

public class LoginMock : ILoginManager
{
    public async Task<(bool, IReadSurvey?)> GetSurvey(int pin)
    {
        return pin == 123
            ? (true, new Survey())
            : (false, null);
    }

    public async Task<bool> Login(string usrname, string password)
    {
        return true;
    }
}