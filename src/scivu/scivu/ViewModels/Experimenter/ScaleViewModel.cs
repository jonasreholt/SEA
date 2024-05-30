using Avalonia.Controls;
using ReactiveUI;

namespace scivu.ViewModels;

public class ScaleViewModel : ViewModelBase
{
    public bool _isChecked;

    public string GroupName { get; }
    public string Text { get; }

    public bool IsChecked
    {
        get => _isChecked;
        set => this.RaiseAndSetIfChanged(ref _isChecked, value);
    }

    public ScaleViewModel(string groupName, string text)
    {
        GroupName = groupName;
        Text = text;
        IsChecked = false;
    }
}