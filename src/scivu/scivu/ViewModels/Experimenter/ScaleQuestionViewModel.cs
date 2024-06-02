using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using scivu.Model;
using Model.Structures;

namespace scivu.ViewModels;

public class ScaleQuestionViewModel : QuestionBaseViewModel
{
    private static int _groupName;

    private SubQuestion _question;

    public ObservableCollection<ScaleViewModel> Buttons { get; } = new();
    public string Text { get; }

    public ScaleQuestionViewModel(SubQuestion question, Result? result)
    {
        _question = question;
        
        Text = question.QuestionText;

        var answers = question.Answer.AnswerOptions;
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
        
        SetResult(result);
    }

    public override void SaveResult(int userId)
    {
        foreach (var button in Buttons)
        {
            if (button.IsChecked)
            {
                var answer = new List<string> { button.Text };
                if (_question.Results.TryGetValue(userId, out var result))
                {
                    result.QuestionResult = answer;
                }
                else
                {
                    result = new Result(answer);
                    _question.Results.Add(userId, result);
                }

                return;
            }
        }
    }

    private void SetResult(Result? results)
    {
        if (results == null) return;
        
        // There can only be one result in a scale question
        Debug.Assert(results.QuestionResult.Count == 1);
        
        var result = results.QuestionResult[0];
        if (result == string.Empty) return;
        
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