namespace Day3.Tests;

public class RucksackTests
{
    [Test]
    [TestCase("vJrwpWtwJgWrhcsFMMfFFhFp", "vJrwpWtwJgWr", "hcsFMMfFFhFp")]
    [TestCase("jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", "jqHRNqRjqzjGDLGL", "rsFMfFZSrLrFZsSL")]
    public void FillContents(string contents, string compartment1, string compartment2)
    {
        var rucksack = new Rucksack(contents);

        Assert.That(rucksack.Compartment1, Is.EqualTo(compartment1));
        Assert.That(rucksack.Compartment2, Is.EqualTo(compartment2));
    }

    [Test]
    [TestCase("PmmdzqPrV", "vPwwTWBwg", "P")]
    [TestCase("wMqvLMZHhHMvwLH", "jbvcjnnSBnvTQFn", "v")]
    [TestCase("ttgJtRGJ", "QctTZtZT", "t")]
    [TestCase("CrZsJsPPZsGz", "wwsLwLmpwMDw", "s")]
    public void FindFirstMatch(string s1, string s2, string match)
    {
        Assert.That(Rucksack.FindFirstmatch(s1, s2), Is.EqualTo(match));
    }
}