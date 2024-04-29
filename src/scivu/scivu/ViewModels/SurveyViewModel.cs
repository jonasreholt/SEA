using Avalonia.Controls;

namespace scivu.ViewModels;

public class SurveyViewModel : ViewModelBase
{
    
}

class Question
{
    public Image? image;
    public string questionText;
}

class MultiChoice : Question
{
    // Which option do you prefer?
    // A --- B --- C
    // [] --[] ---[]
    private string[] _choises;
}


class ScrollChoice : Question
{
    // Which option do you prefer?
    // A --- B --- C
    // [] --[] ---[]
    private string[] _choises;
}

class TextAnswer : Question
{
}

