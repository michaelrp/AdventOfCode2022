namespace Day3.Tests;

public class BadgeTests
{

    [Test]
    [TestCase("r", "vJrwpWtwJgWrhcsFMMfFFhFp", "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", "PmmdzqPrVvPwwTWBwg")]
    [TestCase("Z", "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn", "ttgJtRGJQctTZtZT", "CrZsJsPPZsGzwwsLwLmpwMDw")]
    public void Test1(string name, params string[] contents)
    {
        Assert.That(Badge.GetBadge(contents), Is.EqualTo(name));
    }
}