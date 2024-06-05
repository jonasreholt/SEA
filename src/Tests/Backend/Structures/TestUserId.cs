namespace Tests.Backend;

using Model.Structures;

[TestFixture]
public class TestUserId
{
    [Test]
    public void TestSimpleInit()
    {
        Assert.Multiple(() =>
        {
            Assert.DoesNotThrow(() => new UserId());
            Assert.DoesNotThrow(() => new UserId(string.Empty, string.Empty));
            Assert.DoesNotThrow(() => new UserId("he@ j", string.Empty));
            Assert.DoesNotThrow(() => new UserId("he@ j", "med$@£"));
            Assert.DoesNotThrow(() => new UserId("he@ j", new byte[]{1,2,3,4}));
        });
    }

    [Test]
    public void TestEquality()
    {
        var uid1 = new UserId();
        var uid2 = new UserId(String.Empty, String.Empty);
        Assert.That(uid1, Is.Not.EqualTo(uid2));

        uid1 = new UserId("hello", "world");
        uid2 = new UserId("hello", "worlD");
        Assert.That(uid1, Is.Not.EqualTo(uid2));
        
        uid1 = new UserId("hello", "world");
        uid2 = new UserId("hello", "world");
        Assert.That(uid1, Is.EqualTo(uid2));
        
        uid1 = new UserId("hello", "world");
        var uid2Str = new UserId("hello", "worlD").ToString();
        uid2 = UserId.Parse(uid2Str);
        Assert.That(uid1, Is.Not.EqualTo(uid2));
        
        uid1 = new UserId("hello", "world");
        uid2Str = uid1.ToString();
        uid2 = UserId.Parse(uid2Str);
        Assert.That(uid1, Is.EqualTo(uid2));
    }
}