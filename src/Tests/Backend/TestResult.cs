namespace Tests.Backend;

using Model.Result;
using Model.Answer;

public class TestResult
{
    private const int sid = 0;
    private const int qid = 0;
    private const int uid = 0;

    public static object[] QuestionResultProvider =
    {
        new object[] { 0, 0, AnswerType.MultipleChoice, 0, new List<string> {"answer"} },
    };



    [Test]
    public void TestSingleAnswerToString()
    {
        var answ = new List<string> { "answer" };
        var res = new Result(sid, qid, AnswerType.MultipleChoice, uid, answ);

        Assert.That(res.ToString(), Is.EqualTo($"{sid},{qid},MultipleChoice,{uid},answer"));
    }

}