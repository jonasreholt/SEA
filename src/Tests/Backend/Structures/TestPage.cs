namespace Tests.Backend;

using Model.Structures;

[TestFixture]
public class TestPage
{
    public void TestSimpleInit()
    {
        Assert.Multiple(() =>
        {
            Assert.DoesNotThrow(() => new Page(new List<Question>()));
            Assert.DoesNotThrow(() => new Page(new List<Question>{default}));
        });
    }

    [Test]
    public void TestAddRemove()
    {
        var p = new Page(new List<Question>());
        var q = new Question("hej", string.Empty, new List<SubQuestion>());
        p.Add(q);

        foreach (var qp in p)
        {
            Assert.That(qp.Caption, Is.EqualTo("hej"));
        }
        
        p.Remove(q);
        foreach (var qp in p)
        {
            Assert.Fail("The iterator should be empty");
        }
    }

    [Test]
    public void TestInsert()
    {
        var q = new Question("hej", string.Empty, new List<SubQuestion>());
        var p = new Page(new List<Question> {q});

        foreach (var qp in p)
        {
            Assert.That(qp.Caption, Is.EqualTo("hej"));
        }

        var q2 = new Question("med", String.Empty, new List<SubQuestion>());
        p.Insert(0, q2);

        using var enumerator = p.GetEnumerator();
        for (var i = 0; enumerator.MoveNext(); i++)
        {
            Assert.That(enumerator.Current.Caption, Is.EqualTo(i==0 ? "med" : "hej"));
        }
    }

    [Test]
    public void TestCopy()
    {
        var q = new Question("hej", string.Empty, new List<SubQuestion>());
        var p = new Page(new List<Question> {q});

        var pc = p.Copy();
        
        Assert.That(p.Questions, Has.Count.EqualTo(pc.Questions.Count));
        for (var i = 0; i < p.Questions.Count; i++)
        {
            var q1 = p.Questions[i];
            var q2 = pc.Questions[i];
            Assert.That(q1.Caption, Is.EqualTo(q2.Caption));
        }
    }
}