using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Model.Database;
using Model.Structures;
using ReactiveUI;

namespace scivu.ViewModels;


public class SurveyTakeViewModel : ViewModelBase
{
    private UserId _superUserId;
    private readonly IDatabase _client;
    private readonly Action<string, object> _changeViewCommand;
    private SurveyWrapper _wrapper;
    private Survey _survey;
    private int _userId;

    private bool _isFirstPage;
    private bool _isLastPage;

    public ObservableCollection<QuestionViewModel> Questions { get; } = new();

    private readonly List<List<Result>> _results = new();
    private int _resultIdx;

    public SurveyTakeViewModel(IDatabase client, Action<string, object> changeViewCommand)
    {
        _client = client;
        _changeViewCommand = changeViewCommand;

        // Create dialog for quitting survey
        ShowDialog = new Interaction<ExitSurveyViewModel, bool>();

        QuitCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var dialog = new ExitSurveyViewModel();

            var result = await ShowDialog.Handle(dialog);

            if (result)
            {
                // Do the quit
                _client.StopSurvey();
                SaveQuestionResults();
                _changeViewCommand.Invoke("PauseMenu", (_superUserId, _wrapper));
            }
        });
    }

    public SurveyTakeViewModel(IDatabase client, Action<string, object> changeViewCommand, UserId superUserId, SurveyWrapper wrapper) : this(client, changeViewCommand)
    {
        StartNewSurvey(superUserId, wrapper);
    }

    public void StartNewSurvey(UserId superUserId, SurveyWrapper surveyWrapper)
    {
        _superUserId = superUserId;
        _wrapper = surveyWrapper;
        _results.Clear();
        _results.Add(new List<Result>());
        _resultIdx = 0;

        (_userId, _survey) = _client.StartSurvey(surveyWrapper);
        _survey.ResetCounter();
        
        // Load first page of questions
        NextQuestions();
        IsFirstQuestion = true;
    }

    public ICommand QuitCommand { get; }
    public Interaction<ExitSurveyViewModel, bool> ShowDialog { get; }

    public bool IsFirstQuestion
    {
        get => _isFirstPage;
        set => this.RaiseAndSetIfChanged(ref _isFirstPage, value);
    }

    public bool IsLastPage
    {
        get => _isLastPage;
        set => this.RaiseAndSetIfChanged(ref _isLastPage, value);
    }

    /// <summary>
    /// Retrieve the next questions to display
    /// </summary>
    private void NextQuestions()
    {
        if (!_survey.NextPageExist())
        {
            throw new InvalidOperationException();
        }


        var questions = _survey.GetNextPage();
        Debug.Assert(questions != null);

        FillQuestions(Questions, questions);

        IsFirstQuestion = false;
        IsLastPage = !_survey.NextPageExist();
    }

    private void PreviousQuestions()
    {
        if (!_survey.PreviousPageExist())
        {
            throw new InvalidOperationException();
        }

        var questions = _survey.GetPreviousPage();
        Debug.Assert(questions != null);
        FillQuestions(Questions, questions);

        IsFirstQuestion = !_survey.PreviousPageExist();
        IsLastPage = false;
    }

    private void FillQuestions(ICollection<QuestionViewModel> target, IEnumerable<Question> questions)
    {
        target.Clear();
        foreach (var question in questions)
        {
            target.Add(new QuestionViewModel(_userId, question));
        }
    }

    private void SaveQuestionResults()
    {
        foreach (var question in Questions)
        {
            question.SaveResult();
        }
        _client.Store(_wrapper, _superUserId, true);
    }

    public void DoNext()
    {
        // Save any questions we have
        SaveQuestionResults();
        _resultIdx++;

        // Make room for the new page of questions results
        if (_resultIdx >= _results.Count) _results.Add(new List<Result>());

        // Change to next set of questions
        NextQuestions();
    }

    public void DoPrevious()
    {
        SaveQuestionResults();
        if (_resultIdx >= 0) _resultIdx--;

        // Change to the previous set of questions
        PreviousQuestions();
    }
}