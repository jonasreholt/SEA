namespace Tests.Backend;

using Model.Structures;

[TestFixture]
public class TestSurvey
{
    [Test]
    public void TestResults()
    {
        var sq = new SubQuestion("test", new Answer(AnswerType.Text));
        sq.Results[0] = new Result(new List<string>{"Result"});
        var q = new Question("TestC", string.Empty, new List<SubQuestion>{sq});
        var p = new Page(new List<Question>{q});
        var s = new Survey();
        s.Add(p);

        var res = s.GetResults();
        Assert.That(res, Has.Count.EqualTo(1));
        Assert.That(res[0], Is.EquivalentTo(sq.Results));
        
        s.ClearResults();
        res = s.GetResults();
        Assert.That(res, Has.Count.EqualTo(1));
        Assert.That(res[0], Is.Empty);
    }

    [Test]
    public void TestIteration()
    {
        var sq = new SubQuestion("test", new Answer(AnswerType.Text));
        sq.Results[0] = new Result(new List<string>{"Result"});
        var q = new Question("TestC", string.Empty, new List<SubQuestion>{sq});
        var p = new Page(new List<Question>{q});
        var pc = p.Copy();
        var s = new Survey();
        s.Add(p);
        s.Add(pc);
        
        Assert.Multiple(() =>
        {
            Assert.That(s.PreviousPageExist(), Is.False);
            Assert.That(s.NextPageExist(), Is.True);
        });

        var p1 = s.GetNextPage();
        Assert.Multiple(() =>
        {
            Assert.That(s.PreviousPageExist(), Is.False);
            Assert.That(s.NextPageExist(), Is.True);
            Assert.That(p1, Is.Not.Null);
            Assert.That(p1, Is.EqualTo(p));
        });

        var p0 = s.GetPreviousPage();
        Assert.Multiple(() =>
        {
            Assert.That(s.PreviousPageExist(), Is.False);
            Assert.That(s.NextPageExist(), Is.True);
            Assert.That(p0, Is.Null);
        });

        var p2 = s.GetNextPage();
        Assert.Multiple(() =>
        {
            Assert.That(s.PreviousPageExist(), Is.True);
            Assert.That(s.NextPageExist(), Is.False);
            Assert.That(p2, Is.Not.Null);
            Assert.That(p2, Is.EqualTo(pc));
        });

        var p3 = s.GetNextPage();
        Assert.Multiple(() =>
        {
            Assert.That(s.PreviousPageExist(), Is.True);
            Assert.That(s.NextPageExist(), Is.False);
            Assert.That(p3, Is.Null);
        });
    }
}