using scivu.Model;

namespace Tests.Frontend;

using Mocks;
using Model.Structures;
using scivu.ViewModels;

public class TestScaleViewModel
{
    private QuestionViewModel SetupVars(string[] arr, string caption, string text, string imagePath)
    {
        var answer = new Answer(AnswerType.Scale, arr);
        var question = new Question()
        {
            Caption = caption,
            PicturePath = imagePath,
            SubQuestions = new List<SubQuestion>
            {
                new SubQuestion(text, answer)
            }
        };
        return new QuestionViewModel(42, question);
    }

    [Test]
    public void TestSetupNoPicture()
    {
        string[] arr = ["1", "5"];
        var caption = "Title caption";
        var text = "This is a scale question?";
        var content = SetupVars(arr, caption, text, string.Empty);

        Assert.Multiple(() =>
        {
            Assert.That(content.Image, Is.Null);
            Assert.That(content.FoundImage, Is.True);
            Assert.That(((ScaleQuestionViewModel)content.Content[0]).Buttons.Count, Is.EqualTo(5));
            Assert.That(content.Caption, Is.EqualTo(caption));
            Assert.That(((ScaleQuestionViewModel)content.Content[0]).Text, Is.EqualTo(text));
        });
    }

    [Test]
    public void TestSetupInvalidPicture()
    {
        string[] arr = ["1", "5"];
        var caption = "Title caption";
        var text = "This is a scale question?";
        var path = "Invalid path";

        var content = SetupVars(arr, caption, text, path);

        Assert.Multiple(() =>
        {
            Assert.That(content.Image, Is.Null);
            Assert.That(content.FoundImage, Is.False);
            Assert.That(((ScaleQuestionViewModel)content.Content[0]).Buttons.Count, Is.EqualTo(5));
            Assert.That(content.Caption, Is.EqualTo(caption));
            Assert.That(((ScaleQuestionViewModel)content.Content[0]).Text, Is.EqualTo(text));
        });
    }

    public static object[] InvalidRangeProvider =
    {
        new object[] { new [] {(SharedConstants.ScaleMinimumValue-1).ToString(), "5"} },
        new object[] { new [] {"3", "3"} },
        new object[] { new [] {"4", "3"} },
        new object[] { new [] {"4g", "5"} },
        new object[] { new [] {"2", "5g"} },
        new object[] { new [] {"1", "5", "7"} },
        new object[] { new [] {"1", (SharedConstants.ScaleMaxRange+1).ToString()} },
    };

    [TestCaseSource(nameof(InvalidRangeProvider))]
    public void TestSetupInvalidRange(string[] arr)
    {
        var caption = "Title caption";
        var text = "This is a scale question?";
        var path = string.Empty;

        Assert.That(() => SetupVars(arr, caption, text, path), Throws.ArgumentException);
    }

}