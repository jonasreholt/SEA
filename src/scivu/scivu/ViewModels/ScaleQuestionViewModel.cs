using System;
using System.Collections.ObjectModel;
using scivu.Model;

namespace scivu.ViewModels;

public class ScaleQuestionViewModel : QuestionBaseViewModel
{
    private static int _groupName;

    public ObservableCollection<ScaleViewModel> Buttons { get; } = new();

    public ScaleQuestionViewModel(ReadOnlyCollection<string> answers)
    {
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

    public override string GetAnswer()
    {
        foreach (var button in Buttons)
        {
            if (button.IsChecked)
            {
                return button.Text;
            }
        }

        return string.Empty;
    }

    public override void SetResult(string result)
    {
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