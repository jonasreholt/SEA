using System;
using System.Collections.ObjectModel;
using System.Net.Mime;
using ReactiveUI;

namespace scivu.ViewModels;

public class TextQuestionViewModel : QuestionBaseViewModel
{
    private string _text = String.Empty;

    public string Text
    {
        get => _text;
        set => this.RaiseAndSetIfChanged(ref _text, value);
    }
    
    public override string GetAnswer()
    {
        return Text;
    }

    public override void SetResult(string result)
    {
        Text = result;
    }
}