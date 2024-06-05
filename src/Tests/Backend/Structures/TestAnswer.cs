namespace Tests.Backend;

using Model.Structures;

[TestFixture]
public class TestAnswer
{
    [Test]
    public void TestSimpleInit()
    {
        Assert.Multiple(() =>
        {
            Assert.DoesNotThrow(() => new Answer(AnswerType.Scale));
            Assert.DoesNotThrow(() => new Answer(AnswerType.MultipleChoice));
            Assert.DoesNotThrow(() => new Answer(AnswerType.Text));
            
            Assert.DoesNotThrow(() => new Answer(AnswerType.Scale, string.Empty));
            Assert.DoesNotThrow(() => new Answer(AnswerType.MultipleChoice, string.Empty));
            Assert.DoesNotThrow(() => new Answer(AnswerType.Text, String.Empty));
            
            Assert.DoesNotThrow(() => new Answer(AnswerType.Scale, "Faulty option"));
            Assert.DoesNotThrow(() => new Answer(AnswerType.MultipleChoice, "Faulty option"));
            Assert.DoesNotThrow(() => new Answer(AnswerType.Text, "Faulty option"));
            
            Assert.DoesNotThrow(() => new Answer(AnswerType.Scale, new List<string> {"1", "3"}));
            Assert.DoesNotThrow(() => new Answer(AnswerType.MultipleChoice, new List<string> {"a","b","c"}));
            Assert.DoesNotThrow(() => new Answer(AnswerType.Text, new List<string> {"hmm"}));
        });
    }

    [Test]
    public void TestAddOption()
    {
        var answer = new Answer(AnswerType.Scale);
        Assert.That(answer.AnswerOptions, Is.Empty);
        
        answer.AddAnswerOption("1");
        Assert.That(answer.AnswerOptions, Has.Count.EqualTo(1));
        
        answer.AddAnswerOption("2");
        Assert.That(answer.AnswerOptions, Has.Count.EqualTo(2));
        
        Assert.Multiple(() =>
        {
            Assert.That(answer.AnswerOptions[0], Is.EqualTo("1"));
            Assert.That(answer.AnswerOptions[1], Is.EqualTo("2"));
        });
    }

    [Test]
    public void TestAddAnswerOptionIndex()
    {
        var answer = new Answer(AnswerType.MultipleChoice, new List<string> { "a", "c" });
        
        answer.AddAnswerOption("b", 1);
        Assert.That(answer.AnswerOptions, Has.Count.EqualTo(3));
        Assert.That(answer.AnswerOptions[1], Is.EqualTo("b"));
        
        answer.AddAnswerOption("d", 3);
        Assert.That(answer.AnswerOptions, Has.Count.EqualTo(4));
        Assert.That(answer.AnswerOptions[3], Is.EqualTo("d"));
        
        answer.AddAnswerOption("0", 0);
        Assert.That(answer.AnswerOptions, Has.Count.EqualTo(5));
        Assert.That(answer.AnswerOptions[0], Is.EqualTo("0"));
    }

    [Test]
    public void TestDeleteAnswerOption()
    {
        var answer = new Answer(AnswerType.MultipleChoice, new List<string> { "a", "b", "c", "d" });
        
        Assert.That(answer.TryDeleteAnswerOption(0));
        Assert.That(answer.AnswerOptions, Has.Count.EqualTo(3));
        Assert.That(answer.AnswerOptions[0], Is.EqualTo("b"));
        
        Assert.That(answer.TryDeleteAnswerOption(1));
        Assert.That(answer.AnswerOptions, Has.Count.EqualTo(2));
        Assert.That(answer.AnswerOptions[1], Is.EqualTo("d"));
        
        Assert.That(answer.TryDeleteAnswerOption(2), Is.False);
        Assert.That(answer.AnswerOptions, Has.Count.EqualTo(2));
        
        Assert.That(answer.TryDeleteAnswerOption(1));
        Assert.That(answer.AnswerOptions, Has.Count.EqualTo(1));
        Assert.That(answer.AnswerOptions[0], Is.EqualTo("b"));
    }

    [Test]
    public void TestCopy()
    {
        var answer = new Answer(AnswerType.MultipleChoice, new List<string> { "a", "b" });
        var answerc = answer.Copy();
        
        Assert.That(answer.AnswerType, Is.EqualTo(answerc.AnswerType));
        Assert.That(answer.AnswerOptions, Is.EqualTo(answerc.AnswerOptions));
    }
}