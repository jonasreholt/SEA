namespace scivu.ViewModels;

using System.Collections.Generic;

/// <summary>
/// Used to distinguish question view models from other.
/// </summary>
public abstract class QuestionBaseViewModel : ViewModelBase
{
    public abstract List<string> GetAnswer();
    public abstract void SetResult(List<string> result);
}