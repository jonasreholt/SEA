using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Avalonia.Controls.Primitives;

namespace scivu.ViewModels;

public class MultiQuestionViewModel : QuestionBaseViewModel
{
    public ObservableCollection<ToggleButton> Toggles { get; } = new();
    public string QuestionText { get; }

    public MultiQuestionViewModel(string questionText, ReadOnlyCollection<string> answers)
    {
        QuestionText = questionText;
        
        foreach (var answer in answers)
        {
            Toggles.Add(new ToggleButton
            {
                Content = answer
            });
        }
    }
    public override List<string> GetAnswer()
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

        return retLst;
    }

    public override void SetResult(List<string> results)
    {
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