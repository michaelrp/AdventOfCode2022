namespace Day11.Tests;

public class Tests
{
    [Test]
    [TestCase(79L, "*", "19", 1501L)]
    [TestCase(54L, "+", "6", 60L)]
    [TestCase(79L, "*", "old", 6241L)]
    [TestCase(74L, "-", "3", 71L)]
    public void CalculateNewWorry(long worry, string operation, string term, long expected)
    {
        var result = Monkey.CalculateNewWorry(worry, operation, term);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    [TestCase(2080L, 13, 1, 3, 1)]
    [TestCase(1200L, 13, 1, 3, 3)]
    public void ThrowTo(long worry, int divisor, int trueId, int falseId, int expectedId)
    {
        var result = Monkey.ThrowTo(worry, divisor, trueId, falseId);

        Assert.That(result, Is.EqualTo(expectedId));
    }
}
