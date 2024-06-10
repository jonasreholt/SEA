using System.Text;
using Model.Database;
using Model.Structures;

namespace tests.Backend;

[TestFixture]
public class TestStringBuilderExtension
{
    [Test]
    public void TestAppendQuestionHeader()
    {
        var sb = new StringBuilder();
        sb.AppendQuestionHeader(5);
        Assert.That(sb.ToString(), Is.EqualTo(",UserID,0,1,2,3,4"+Environment.NewLine));
        
        sb.Clear();
        sb.AppendQuestionHeader(-1);
        Assert.That(sb.ToString(), Is.EqualTo(string.Empty));

        sb.Clear();
        sb.AppendQuestionHeader(0);
        Assert.That(sb.ToString(), Is.EqualTo(string.Empty));
    }

    [Test]
    public void TestAppendResults()
    {
        var resultsq1 = new Dictionary<int, Result>
        {
            { 0, new Result(new List<string> { "res1a", "res1b" }) },
            { 1, new Result(new List<string>()) },
            { 2, new Result(new List<string>{"res2"}) }
        };
        var results = new List<Dictionary<int, Result>>
        {
            resultsq1
        };
        
        var sb = new StringBuilder();
        sb.AppendResults(results);
        var expected = ",0,\"res1a;res1b\""+Environment.NewLine
            +",1,\"\""+Environment.NewLine
            +",2,\"res2\""+Environment.NewLine;
        Assert.That(sb.ToString(), Is.EqualTo(expected));

        sb.Clear();
        results.Clear();
        sb.AppendResults(results);
        Assert.That(sb.ToString(), Is.EqualTo(string.Empty));
    }
}