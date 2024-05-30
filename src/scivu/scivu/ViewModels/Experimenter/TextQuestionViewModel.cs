using System;
using System.Collections.Generic;
using System.Diagnostics;
using ReactiveUI;
using Model.Structures;

namespace scivu.ViewModels;

public class TextQuestionViewModel : QuestionBaseViewModel
{
    private readonly SubQuestion _question;
    
    private string _text = String.Empty;
    

    public TextQuestionViewModel(SubQuestion question, Result? result)
    {
        _question = question;
        
        QuestionText = question.QuestionText;
        
        SetResult(result);
    }

    public string QuestionText { get; }
    public string Text
    {
        get => _text;
        set => this.RaiseAndSetIfChanged(ref _text, value);
    }
    
    public override void SaveResult(int userId)
    {
        var answer = new List<string> { Text };
        if (_question.Results.TryGetValue(userId, out var result))
        {
            result.QuestionResult = answer;
        }
        else
        {
            result = new Result(answer);
            _question.Results.Add(userId, result);
        }
    }

    private void SetResult(Result? result)
    {
        if (result == null) return;
        
        // There can be only one answer for a free text question
        Debug.Assert(result.QuestionResult.Count == 1);
        
        Text = result.QuestionResult[0];
    }
}