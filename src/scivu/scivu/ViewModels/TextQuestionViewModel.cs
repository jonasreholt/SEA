using System;
using System.Collections.Generic;
using System.Diagnostics;
using ReactiveUI;

namespace scivu.ViewModels;

public class TextQuestionViewModel : QuestionBaseViewModel
{
    private string _text = String.Empty;
    

    public TextQuestionViewModel(string questionText)
    {
        QuestionText = questionText;
    }

    public string QuestionText { get; }
    public string Text
    {
        get => _text;
        set => this.RaiseAndSetIfChanged(ref _text, value);
    }
    
    public override List<string> GetAnswer()
    {
        return new List<string> { Text };
    }

    public override void SetResult(List<string> result)
    {
        // There can be only one answer for a free text question
        Debug.Assert(result.Count == 1);
        
        Text = result[0];
    }
}