using Model.Factory;

namespace Tests.Frontend;

using System.Collections.Generic;
using Mocks;
using scivu.ViewModels;
using Model.Structures;

[TestFixture]
public class TestSurveyTakeViewModel
{
    private Page _page1 = new(new List<Question>
    {
        new Question
        {
            Caption = "Caption1",
            PicturePath = string.Empty,
            SubQuestions = new List<SubQuestion>
            {
                new SubQuestion("Question1", new Answer(AnswerType.Scale, new string[] { "1", "8" }))
            }
        },
        new Question()
        {
            Caption = string.Empty,
            PicturePath = string.Empty,
            SubQuestions =  new List<SubQuestion>
            {
                new SubQuestion("Question2", new Answer(AnswerType.Scale, new string[] { "1", "3" }))
            }
        }
    });

    private Page _page2 = new(new List<Question>
    {
        new Question()
        {
            Caption = "Caption2",
            PicturePath = string.Empty,
            SubQuestions = new List<SubQuestion>
            {
                new SubQuestion("Question3", new Answer(AnswerType.Scale, new string[] {"22", "25"}))
            }
        }
    });

    private Action<string, object> dummy = (_, _) => { };


    [Test]
    public void TestInitialState()
    {
        var survey = new Survey();
        survey.Add(_page1);
        survey.Add(_page2);
        var surveyWrap = new SurveyWrapper(42);
        surveyWrap.Add(survey);
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
        var survey = new Survey();
        survey.Add(_page1);
        survey.Add(_page2);
        var surveyWrap = new SurveyWrapper(42);
        surveyWrap.Add(survey);
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
        var survey = new Survey();
        survey.Add(_page1);
        survey.Add(_page2);
        var surveyWrap = new SurveyWrapper(42);
        surveyWrap.Add(survey);
        var vm = new SurveyTakeViewModel(default, dummy, surveyWrap, 42);

        Assert.That(vm.ChooseSurvey(surveyWrap), Is.EqualTo(survey));
    }

    [Test]
    public void TestChooseSurvey2()
    {
        var survey1 = new Survey();
        survey1.Add(_page1);
        survey1.Add(_page2);
        var survey2 = new Survey();
        survey2.Add(_page2);
        survey2.Add(_page1);
        var surveyWrap = new SurveyWrapper(42);
        surveyWrap.Add(survey1);
        surveyWrap.Add(survey2);

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