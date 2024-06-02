using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Avalonia.Controls.Primitives;
using Model.Structures;

namespace scivu.ViewModels;

public class MultiQuestionViewModel : QuestionBaseViewModel
{
    private readonly SubQuestion _question;
    
    public ObservableCollection<ToggleButton> Toggles { get; } = new();
    public string QuestionText { get; }

    public MultiQuestionViewModel(SubQuestion question, Result? result)
    {
        _question = question;
        
        QuestionText = question.QuestionText;
        
        foreach (var answer in question.Answer.AnswerOptions)
        {
            Toggles.Add(new ToggleButton
            {
                Content = answer
            });
        }
        
        SetResult(result);
    }
    public override void SaveResult(int userId)
    {
        var retLst = new List<string>();
        foreach (var toggle in Toggles)
        {
            Debug.Assert(toggle.IsChecked.HasValue);
            if (toggle.IsChecked.Value)
            {
                retLst.Add(toggle.Content!.ToString()!);
            }
        }

        if (_question.Results.TryGetValue(userId, out var result))
        {
            result.QuestionResult = retLst;
        }
        else
        {
            result = new Result(retLst);
            _question.Results.Add(userId, result);
        }
    }

    private void SetResult(Result? result)
    {
        if (result == null) return;

        var results = result.QuestionResult;
        var i = 0;
        var j = 0;
        for (; i < results.Count && j < Toggles.Count;)
        {
            var res = results[i];
            var toggle = Toggles[j];

            if (res == toggle.Content!.ToString())
            {
                toggle.IsChecked = true;
                i++;
            }
            j++;
        }
    }
}