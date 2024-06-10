using Model.Database;
using Model.Structures;

namespace tests.Backend;

[TestFixture]
public class TestDatabaseServices
{
    private static UserId ID = new UserId("admin", "admin");
    
    [Test]
    public void TestSetup()
    {
        var db = new DatabaseServices();

        Assert.That(db._userToSurveys, Has.Count.EqualTo(1));
        Assert.That(db._userToSurveys.TryGetValue(ID, out var sws));
        Assert.That(sws, Has.Count.EqualTo(1));
        Assert.That(sws[0].PinCode, Is.EqualTo(123456));
    }

    [Test]
    public void TestLoadCache()
    {
        // Setup the cache
        var db = new DatabaseServices();
        var sw = new SurveyWrapper(654321);
        db.Store(sw, ID);
        db.SaveCache().Wait();
        
        // Read entries from cache
        db = new DatabaseServices();
        
        Assert.That(db._userToSurveys, Has.Count.EqualTo(1));

        var sws = db.GetSurveyWrappersForSuperUser(ID);
        Assert.That(sws, Is.Not.Null);
        Assert.That(sws, Has.Count.EqualTo(2));
        Assert.That(sws[0].PinCode, Is.EqualTo(123456));
        Assert.That(sws[1].PinCode, Is.EqualTo(654321));
    }

    [Test]
    public void TestRestartUser()
    {
        var db = new DatabaseServices();

        var sw = db.GetSurveyWrappersForSuperUser(ID)[0];

        var (userId, s) = db.StartSurvey(sw);
        Assert.Multiple(() =>
        {
            Assert.That(userId, Is.EqualTo(1));
            Assert.That(s, Is.Not.Null);
            Assert.That(s.SurveyName, Is.EqualTo("A"));
        });

        // Set some result
        var p = s.GetNextPage();
        var expectedRes = new List<string> { "res" };
        p.Questions[0].SubQuestions[0].Results[userId] = new Result(expectedRes);
        db.SaveCache().Wait();
        
        // Now start a new database and see if it correctly load
        db = new DatabaseServices();
        sw = db.GetSurveyWrappersForSuperUser(ID)[0];
        var results = sw.SurveyVersions[0].GetResults();

        var numberOfQuestionPages = 4;
        Assert.That(results, Has.Count.EqualTo(numberOfQuestionPages));
        Assert.That(results[0], Has.Count.EqualTo(1));
        var result = results[0][userId];
        
        Assert.That(result.QuestionResult, Is.EquivalentTo(expectedRes));
    }

    [TearDown]
    public void TearDown()
    {
        if (Directory.Exists(DatabaseServices.DatabasePath))
        {
            Directory.Delete(DatabaseServices.DatabasePath, true);
        }
    }
}