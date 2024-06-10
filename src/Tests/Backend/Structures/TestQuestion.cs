namespace Tests.Backend;

using Model.Structures;

[TestFixture]
public class TestQuestion
{
    [TestCase("stringMe", "")]
    public void TestInit(string caption, string imagePath)
    {
        var a = new Answer(AnswerType.Text);
        var sq = new SubQuestion("Hello World", a);
        var sqs = new List<SubQuestion> { sq };
        var q = new Question(caption, imagePath, sqs);
        
        Assert.Multiple(() =>
        {
            Assert.That(q.PicturePath, Is.EqualTo(imagePath));
            Assert.That(q.Caption, Is.EqualTo(caption));
            Assert.That(q.SubQuestions, Is.EquivalentTo(sqs));
        });
    }

    [TestCase("Hello World", "pla.png")]
    public void TestCopy(string caption, string imagePath)
    {
        var options = new List<string> { "1", "3" };
        var a = new Answer(AnswerType.Scale, options);
        var sq = new SubQuestion("Hello World", a);
        var sqs = new List<SubQuestion> { sq };
        var q = new Question(caption, imagePath, sqs);

        var qc = q.Copy();
        
        Assert.Multiple(() =>
        {
            Assert.That(qc, Is.Not.EqualTo(q));
            Assert.That(qc.Caption, Is.EqualTo(q.Caption));
            Assert.That(qc.PicturePath, Is.EqualTo(q.PicturePath));
            
            Assert.That(qc.SubQuestions, Is.Not.EqualTo(q.SubQuestions));
            Assert.That(qc.SubQuestions, Has.Count.EqualTo(q.SubQuestions.Count));
            Assert.That(qc.SubQuestions[0].QuestionText, Is.EqualTo(q.SubQuestions[0].QuestionText));
            Assert.That(qc.SubQuestions[0].Answer.AnswerType, Is.EqualTo(q.SubQuestions[0].Answer.AnswerType));
            Assert.That(qc.SubQuestions[0].Answer.AnswerOptions, Is.EquivalentTo(q.SubQuestions[0].Answer.AnswerOptions));
        });
    }
}