using Model.Structures;

namespace Tests.Backend;

[TestFixture]
public class TestSubQuestion
{
    [TestCase("stringMe")]
    public void TestInit(string text)
    {
        var a = new Answer(AnswerType.Text);
        var sq = new SubQuestion(text, a);
        
        Assert.Multiple(() =>
        {
            Assert.That(sq.QuestionText, Is.EqualTo(text));
            Assert.That(sq.Answer, Is.EqualTo(a));
        });
    }

    [TestCase("Hello World")]
    public void TestCopy(string text)
    {
        var options = new List<string> { "1", "3" };
        var a = new Answer(AnswerType.Scale, options);
        var sq = new SubQuestion(text, a);

        var sqc = sq.Copy();
        
        Assert.Multiple(() =>
        {
            Assert.That(sqc, Is.Not.EqualTo(sq));
            Assert.That(sqc.QuestionText, Is.EqualTo(sq.QuestionText));
            
            Assert.That(sqc.Answer, Is.Not.EqualTo(sq.Answer));
            Assert.That(sqc.Answer.AnswerType, Is.EqualTo(sq.Answer.AnswerType));
            Assert.That(sqc.Answer.AnswerOptions, Is.EquivalentTo(sq.Answer.AnswerOptions));
        });
    }
}