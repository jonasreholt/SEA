namespace scivu.Model;

public enum ErrorDiagnosticsID
{
    ERR_PinCodeNotFound,
    ERR_InvalidLogin,
    
    ERR_InvalidPIN,
    ERR_DuplicatePIN,

    WAR_InvalidSurveyFileType,
    WAR_CouldNotImportSurvey,

    ERR_ScaleRangeNotInt,
    ERR_ScaleRangeInvalid,
}