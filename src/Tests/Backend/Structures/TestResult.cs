namespace Tests.Backend;

using Model.Structures;

public class TestResult
{
    private const int sid = 0;
    private const int qid = 0;
    private const int uid = 0;

    [Test]
    public void TestSingleAnswerToStringSimple()
    {
        var answ = new List<string> { "answer" };
        var res = new Result(answ);

        Assert.That(res.ToString(), Is.EqualTo($"answer"));
    }

    [Test]
    public void TestSingleAnswerToString()
    {
        var answ = new List<string> { @"answer\" };
        var res = new Result(answ);

        Assert.That(res.ToString(), Is.EqualTo($"answer\\\\"));
    }
    
    [Test]
    public void TestMultiAnswerToString()
    {
        var answ = new List<string> { @"answer\", "answer2" };
        var res = new Result(answ);

        Assert.That(res.ToString(), Is.EqualTo($"answer\\\\;answer2"));
    }
}