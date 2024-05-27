using Model.Factory;

namespace Tests.Frontend;

using System.Collections.Generic;
using Mocks;
using scivu.ViewModels;
using Model.Question;
using Model.Answer;
using Model.Survey;

[TestFixture]
public class TestSurveyTakeViewModel
{
    private List<IReadOnlyQuestion> _page1 = new()
    {
        new ReadOnlyQuestionMock()
        {
            QuestionId = 1,
            ReadOnlyCaption = "Caption1",
            ReadOnlyText = "Question1",
            ReadOnlyPicture = string.Empty,
            ReadOnlyAnswer = new ReadOnlyAnswerMock()
            {
                ReadOnlyAnswerType = AnswerType.Scale,
                ReadOnlyAnswers = (new List<string> {"1", "8"}).AsReadOnly()
            }
        },
        new ReadOnlyQuestionMock()
        {
            QuestionId = 2,
            ReadOnlyCaption = string.Empty,
            ReadOnlyText = "Question2",
            ReadOnlyPicture = string.Empty,
            ReadOnlyAnswer = new ReadOnlyAnswerMock()
            {
                ReadOnlyAnswerType = AnswerType.Scale,
                ReadOnlyAnswers = new List<string> {"1","3"}.AsReadOnly()
            }
        }
    };

    private List<IReadOnlyQuestion> _page2 = new()
    {
        new ReadOnlyQuestionMock()
        {
            QuestionId = 3,
            ReadOnlyCaption = "Caption2",
            ReadOnlyText = "Question3",
            ReadOnlyPicture = string.Empty,
            ReadOnlyAnswer = new ReadOnlyAnswerMock()
            {
                ReadOnlyAnswerType = AnswerType.Scale,
                ReadOnlyAnswers = new List<string> {"22", "25"}.AsReadOnly()
            }
        }
    };

    private Action<string, object> dummy = (_, _) => { };


    [Test]
    public void TestInitialState()
    {
        var survey = new ReadOnlySurveyMock()
        {
            Questions = new List<List<IReadOnlyQuestion>> {_page1, _page2}
        };
        var surveyWrap = new ReadOnlySurveyWrapperMock(survey);
        var vm = new SurveyTakeViewModel(default, dummy, surveyWrap, 42);

        Assert.Multiple(() =>
        {
            Assert.That(vm.IsFirstQuestion, Is.True);
            Assert.That(vm.IsLastPage, Is.False);
        });
    }

    [Test]
    public void TestSwitchPages()
    {
        var client = FrontEndFactory.CreateExperimenterMenu();
        var survey = new ReadOnlySurveyMock()
        {
            Questions = new List<List<IReadOnlyQuestion>> {_page1, _page2}
        };
        var surveyWrap = new ReadOnlySurveyWrapperMock(survey);
        var vm = new SurveyTakeViewModel(client, dummy, surveyWrap, 42);

        vm.DoNext();
        Assert.Multiple(() =>
        {
            Assert.That(vm.IsFirstQuestion, Is.False);
            Assert.That(vm.IsLastPage, Is.True);
        });

        vm.DoPrevious();
        Assert.Multiple(() =>
        {
            Assert.That(vm.IsFirstQuestion, Is.True);
            Assert.That(vm.IsLastPage, Is.False);
        });
    }

    [Test]
    public void TestChooseSurvey1()
    {
        var survey = new ReadOnlySurveyMock()
        {
            Questions = new List<List<IReadOnlyQuestion>> {_page1, _page2}
        };
        var surveyWrap = new ReadOnlySurveyWrapperMock(survey);
        var vm = new SurveyTakeViewModel(default, dummy, surveyWrap, 42);

        Assert.That(vm.ChooseSurvey(surveyWrap), Is.EqualTo(survey));
    }

    [Test]
    public void TestChooseSurvey2()
    {
        var survey1 = new ReadOnlySurveyMock()
        {
            Questions = new List<List<IReadOnlyQuestion>> {_page1, _page2}
        };
        var survey2 = new ReadOnlySurveyMock()
        {
            Questions = new List<List<IReadOnlyQuestion>> {_page2, _page1}
        };
        var surveyWrap = new ReadOnlySurveyWrapperMock(new List<IReadOnlySurvey> {survey1, survey2});

        var vm = new SurveyTakeViewModel(default, dummy, surveyWrap, 42);

        var found1 = false;
        var found2 = false;
        for (var i = 0; i < 50; i++)
        {
            var survey = vm.ChooseSurvey(surveyWrap);
            found1 |= survey.Equals(survey1);
            found2 |= survey.Equals(survey2);
        }

        Assert.Multiple(() =>
        {
            Assert.That(found1, "survey 1 was never found");
            Assert.That(found2, "survey 2 was never found");
        });
    }
}