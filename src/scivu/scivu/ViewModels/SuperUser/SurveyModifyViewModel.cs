using System;
using System.Collections.ObjectModel;
using DynamicData;
using Model.Structures;
using scivu.Model;


namespace scivu.ViewModels.SuperUser;
public class SurveyModifyViewModel : ViewModelBase
{
    private readonly Survey _survey;
    
    public ObservableCollection<QuestionViewModel> Questions { get; } = new();

    public SurveyModifyViewModel(Survey survey)
    {
        _survey = survey;

        if (survey.NextPageExist())
        {
            var page = survey.GetNextPage()!.Value;
            foreach (var question in page)
            {
                var vm = new QuestionViewModel(question);
                Questions.Add(vm);
            }
        }
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

    public void Finish()
    {
        throw new NotImplementedException();
    }

    public void CreatePage()
    {
        throw new NotImplementedException();
    }

    public void NextPage()
    {
        throw new NotImplementedException();
    }

    public void PreviousPage()
    {
        throw new NotImplementedException();
    }
}
