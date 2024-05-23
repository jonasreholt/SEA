namespace scivu.ViewModels;

public class ScaleViewModel
{
    public string GroupName { get; }
    public string Text { get; }

    public ScaleViewModel(string groupName, string text)
    {
        GroupName = groupName;
        Text = text;
    }
}