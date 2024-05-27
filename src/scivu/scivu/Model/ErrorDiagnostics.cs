using System;
using System.Collections.Generic;

namespace scivu.Model;

public static class ErrorDiagnostics
{
    private static readonly Dictionary<ErrorDiagnosticsID, string> Errors = new();

    static ErrorDiagnostics()
    {
        Errors[ErrorDiagnosticsID.ERR_PinCodeNotFound] = "PIN does not match any survey.";
        Errors[ErrorDiagnosticsID.ERR_InvalidLogin] = "Invalid username or password";
        Errors[ErrorDiagnosticsID.ERR_ScaleRangeNotInt] = "Range must consist of integers";
        Errors[ErrorDiagnosticsID.ERR_ScaleRangeInvalid] = $"Range must be [a,b] where {SharedConstants.ScaleMinimumValue}<a<b";
        Errors[ErrorDiagnosticsID.WAR_InvalidSurveyFileType] = $"Invalid file type for survey";
        Errors[ErrorDiagnosticsID.WAR_CouldNotImportSurvey] = "Survey could not be imported";
    }

    public static string GetErrorMessage(ErrorDiagnosticsID id)
    {
        if (!Errors.TryGetValue(id, out var msg))
        {
            throw new ArgumentException("ID does not match a saved diagnostic message", nameof(id));
        }

        return msg;
    }
}