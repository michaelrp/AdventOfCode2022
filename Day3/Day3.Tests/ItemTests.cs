namespace Day3.Tests;

public class ItemTests
{

    [Test]
    [TestCase("p", 16)]
    [TestCase("L", 38)]
    [TestCase("P", 42)]
    [TestCase("v", 22)]
    [TestCase("t", 20)]
    [TestCase("s", 19)]
    public void Test1(string name, int priority)
    {
        Assert.That(Item.Priority(name), Is.EqualTo(priority));
    }
}