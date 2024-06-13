using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using DynamicData;
using Model.Structures;
using ReactiveUI;
using scivu.Model;


namespace scivu.ViewModels.SuperUser;
public class SurveyModifyViewModel : ViewModelBase
{
    private Survey _survey;
    private Action<string, object> _changeViewCommand;

    private bool _isFirstPage;
    private bool _isLastPage;
    
    public ObservableCollection<QuestionViewModel> Questions { get; } = new();

    public SurveyModifyViewModel(Action<string, object> changeViewCommand, Survey survey)
    {
        _changeViewCommand = changeViewCommand;
        _survey = survey;
        
        survey.ResetCounter();

        _isFirstPage = true;
        if (survey.NextPageExist())
        {
            var page = survey.GetNextPage()!;
            FillQuestions(page);
        }

        _isLastPage = !survey.NextPageExist();
    }

    public bool IsFirstPage
    {
        get => _isFirstPage;
        private set => this.RaiseAndSetIfChanged(ref _isFirstPage, value);
    }

    public bool IsLastPage
    {
        get => _isLastPage;
        private set => this.RaiseAndSetIfChanged(ref _isLastPage, value);
    }

    private void FillQuestions(Page page)
    {
        Questions.Clear();
        foreach (var question in page)
        {
            var vm = new QuestionViewModel(Remove, question);
            Questions.Add(vm);
        }
    }

    public void Finish()
    {
        Save();
        _changeViewCommand(SharedConstants.ModifySurveyWrapperName, null);
    }

    public void CreatePage()
    {
        Save();

        var page = new Page(new List<Question>());
        _survey.Add(page);
        FillQuestions(_survey.GetNextPage()!);
        IsFirstPage = false;
        IsLastPage = !_survey.NextPageExist();
    }

    public void NextPage()
    {
        Debug.Assert(_survey.NextPageExist());
        
        Save();
        FillQuestions(_survey.GetNextPage()!);
        IsFirstPage = false;
        IsLastPage = !_survey.NextPageExist();
    }

    public void PreviousPage()
    {
        Debug.Assert(_survey.PreviousPageExist());
        
        Save();
        FillQuestions(_survey.GetPreviousPage()!);
        IsFirstPage = !_survey.PreviousPageExist();
        IsLastPage = false;
    }

    public void AddQuestion()
    {
        var question = new Question(string.Empty, String.Empty, new List<SubQuestion>());
        var questionVm = new QuestionViewModel(Remove, question);
        Questions.Add(questionVm);

        var current = _survey.GetCurrent();
        if (current == null)
        {
            current = new Page(new List<Question>());
            _survey.Add(current);
            _survey.GetNextPage();
        }
        
        current.Add(question);
    }

    private void Remove(QuestionViewModel question)
    {
        Questions.Remove(question);
        _survey.GetCurrent()!.Remove(question.Question);
    }

    private void Save()
    {
        foreach (var question in Questions)
        {
            question.Save();
        }
    }
}
