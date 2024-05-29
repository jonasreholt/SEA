using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using scivu.Model;

namespace scivu.ViewModels;

public class ScaleQuestionViewModel : QuestionBaseViewModel
{
    private static int _groupName;

    public ObservableCollection<ScaleViewModel> Buttons { get; } = new();
    public string Text { get; }

    public ScaleQuestionViewModel(string questionText, ReadOnlyCollection<string> answers)
    {
        Text = questionText;
        
        if (answers.Count != 2)
        {
            throw new ArgumentException(ErrorDiagnostics.GetErrorMessage(ErrorDiagnosticsID.ERR_ScaleRangeInvalid));
        }

        if (!Int32.TryParse(answers[0], out var min) || !Int32.TryParse(answers[1], out var max))
        {
            throw new ArgumentException(ErrorDiagnostics.GetErrorMessage(ErrorDiagnosticsID.ERR_ScaleRangeNotInt));
        }
        if (!(SharedConstants.ScaleMinimumValue <= min && min < max && max-min < SharedConstants.ScaleMaxRange))
        {
            throw new ArgumentException(ErrorDiagnostics.GetErrorMessage(ErrorDiagnosticsID.ERR_ScaleRangeInvalid));
        }

        _groupName++;
        for (; min <= max; min++)
        {
            var svm = new ScaleViewModel(_groupName.ToString(), min.ToString());
            Buttons.Add(svm);
        }
    }

    public override List<string> GetAnswer()
    {
        foreach (var button in Buttons)
        {
            if (button.IsChecked)
            {
                return new List<string> { button.Text };
            }
        }

        return new List<string> { string.Empty };
    }

    public override void SetResult(List<string> results)
    {
        // There can only be one result in a scale question
        Debug.Assert(results.Count == 1);

        var result = results[0];
        // check if any result was given
        if (result == string.Empty)
        {
            return;
        }
        
        // Find the correct radiobutton to check
        foreach (var button in Buttons)
        {
            if (button.Text.Equals(result))
            {
                button.IsChecked = true;
                return;
            }
        }

        throw new ArgumentException($"The Given result `{result}` does not match the scale");
    }
}