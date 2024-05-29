namespace Tests.Backend;

using Model.Result;
using Model.Answer;

public class TestResult
{
    private const int sid = 0;
    private const int qid = 0;
    private const int uid = 0;

    [Test]
    public void TestSingleAnswerToStringSimple()
    {
        var answ = new List<string> { "answer" };
        var res = new Result(sid, qid, AnswerType.MultipleChoice, uid, answ);

        Assert.That(res.ToString(), Is.EqualTo($"{sid},{qid},MultipleChoice,{uid},answer"));
    }

    [Test]
    public void TestSingleAnswerToString()
    {
        var answ = new List<string> { @"answer\" };
        var res = new Result(sid, qid, AnswerType.MultipleChoice, uid, answ);

        Assert.That(res.ToString(), Is.EqualTo($"{sid},{qid},MultipleChoice,{uid},answer\\\\"));
    }
    
    [Test]
    public void TestMultiAnswerToString()
    {
        var answ = new List<string> { @"answer\", "answer2" };
        var res = new Result(sid, qid, AnswerType.MultipleChoice, uid, answ);

        Assert.That(res.ToString(), Is.EqualTo($"{sid},{qid},MultipleChoice,{uid},answer\\\\;answer2"));
    }
}