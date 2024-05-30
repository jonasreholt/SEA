using System;
using Model.Structures;
using scivu.Model;


namespace scivu.ViewModels.SuperUser;
public class SurveyModifyViewModel : ViewModelBase
{

    public SurveyModifyViewModel()
    {
    }

    public void ChangeView(string view)
    {
        throw new NotImplementedException();
    }

    public async void AddPicture(Question question)
    {
        var file = await FileExplorer.OpenImageAsync();
        question.PicturePath = file?.Path.ToString() ?? string.Empty;
    }
}
