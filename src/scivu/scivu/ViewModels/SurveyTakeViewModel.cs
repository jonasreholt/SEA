using System;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Model.Answer;
using Model.FrontEndAPI;
using Model.Question;
using Model.Result;
using Model.Survey;
using Answer = scivu.Model.Answer;

namespace scivu.ViewModels;

using System.Collections.ObjectModel;
using ReactiveUI;

public class SurveyTakeViewModel : ViewModelBase
{
    private readonly IClientRequest _client;
    private readonly Action<string, object> _changeViewCommand;
    private readonly IReadOnlySurvey _survey;
    private readonly int _userId;

    private bool _isFirstPage;
    private bool _isLastPage;

    public ObservableCollection<QuestionBaseViewModel> Questions { get; } = new();

    private readonly List<List<IResult>> _results = new();
    private int _resultIdx;

    private readonly Random rnd;

    public SurveyTakeViewModel(IClientRequest client, Action<string, object> changeViewCommand, IReadOnlySurveyWrapper surveyWrapper, int userId)
    {
        _client = client;
        rnd = new Random();
        _changeViewCommand = changeViewCommand;
        _userId = userId;
        _survey = ChooseSurvey(surveyWrapper);

        // Make room for results for this specific line of questions
        _results.Add(new List<IResult>());
        _resultIdx = 0;

        // Load first page of questions
        NextQuestions();
        IsFirstQuestion = true;

        // Create dialog for quitting survey
        ShowDialog = new Interaction<ExitSurveyViewModel, bool>();

        QuitCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var dialog = new ExitSurveyViewModel();

            var result = await ShowDialog.Handle(dialog);

            if (result)
            {
                // Do the quit
                Console.WriteLine("Quitting survey");
                _changeViewCommand.Invoke("PauseMenu", surveyWrapper);
            }
        });
    }

    internal IReadOnlySurvey ChooseSurvey(IReadOnlySurveyWrapper surveyWrapper)
    {
        var count = surveyWrapper.GetVersionCount();
        var idx = rnd.Next(0, count);

        return surveyWrapper.TryGetReadOnlySurveyVersion(idx);
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
        if (!_survey.NextQuestionExist())
        {
            throw new InvalidOperationException();
        }


        var questions = _survey.TryGetNextReadOnlyQuestion();
        Debug.Assert(questions != null);

        FillQuestions(Questions, questions);

        IsFirstQuestion = false;
        IsLastPage = !_survey.NextQuestionExist();

        FillSavedAnswerToQuestions();
    }

    private void PreviousQuestions()
    {
        if (!_survey.PreviousQuestionExist())
        {
            throw new InvalidOperationException();
        }

        var questions = _survey.TryGetPreviousReadOnlyQuestion();
        Debug.Assert(questions != null);
        FillQuestions(Questions, questions);

        IsFirstQuestion = !_survey.PreviousQuestionExist();
        IsLastPage = false;

        FillSavedAnswerToQuestions();
    }

    private static void FillQuestions(ICollection<QuestionBaseViewModel> target, IEnumerable<IReadOnlyQuestion> questions)
    {
        target.Clear();
        foreach (var question in questions)
        {
            var answer = question.ReadOnlyAnswer;
            var t = answer.ReadOnlyAnswerType;
            target.Add(
                t switch
                {
                    AnswerType.Text => throw new NotImplementedException(t.ToString()),
                    AnswerType.Scale => new ScaleQuestionViewModel(question),
                    AnswerType.MultipleChoice => throw new NotImplementedException(t.ToString()),
                    _ => throw new UnreachableException($"Setting questions with `{t}`")
                });
        }
    }

    private void SaveQuestionAnswers()
    {
        var currentAnswerList = _results[_resultIdx];
        // Clear any possible old answers as we are about to overwrite anyway
        currentAnswerList.Clear();
        foreach (var question in Questions)
        {
            var answer = new Answer(
                question.GetQuestionType(),
                question.GetAnswer(),
                _userId,
                question.GetId(),
                _survey.SurveyId
                );
            currentAnswerList.Add(answer);

            // currently we send in the answers each time
            _client.StoreResultFromQuestion(answer);
        }
    }

    private void FillSavedAnswerToQuestions()
    {
        var answers = _results[_resultIdx];
        // Check if we actually have anything saved here
        if (answers.Count <= 0) return;

        Debug.Assert(answers.Count == Questions.Count);

        for (var i = 0; i < Questions.Count; i++)
        {
            var answer = answers[i];
            var question = Questions[i];

            // If we have a saved result
            if (answer.QuestionResult != string.Empty)
            {
                question.SetResult(answer.QuestionResult);
            }
        }
    }

    public void DoNext()
    {
        // Save any questions we have
        SaveQuestionAnswers();
        _resultIdx++;

        // Make room for the new page of questions results
        if (_resultIdx >= _results.Count) _results.Add(new List<IResult>());

        // Change to next set of questions
        NextQuestions();
    }

    public void DoPrevious()
    {
        SaveQuestionAnswers();
        if (_resultIdx >= 0) _resultIdx--;

        // Change to the previous set of questions
        PreviousQuestions();
    }
}