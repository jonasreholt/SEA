using System;
using Model.Question;
using scivu.Model;


namespace scivu.ViewModels;
public class SurveyModifyViewModel : ViewModelBase
{

    public SurveyModifyViewModel()
    {
    }

    public void ChangeView(string view)
    {
        throw new NotImplementedException();
    }

    public async void AddPicture(IModifyQuestion question)
    {
        var file = await FileExplorer.OpenImageAsync();
        question.ModifyPicture = file?.Path.ToString() ?? string.Empty;
    }
}
