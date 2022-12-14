namespace Day04.Tests;

public class SectionRangeTests
{
    [Test]
    [TestCase("71-87", 71, 87)]
    [TestCase("6-6", 6, 6)]
    [TestCase("8-92", 8, 92)]
    public void StartEndRanges(string range, int start, int end)
    {
        var sectionRange = new SectionRange(range);
        Assert.That(sectionRange.Start, Is.EqualTo(start));
        Assert.That(sectionRange.End, Is.EqualTo(end));
    }

    [Test]
    [TestCase("2-4", "6-8", false)]
    [TestCase("2-3", "4-5", false)]
    [TestCase("5-7", "7-9", false)]
    [TestCase("2-8", "3-7", true)]
    [TestCase("6-6", "4-6", true)]
    [TestCase("2-6", "4-8", false)]
    public void FullOverlap(string range1, string range2, bool fullOverlap)
    {
        var sectionRange1 = new SectionRange(range1);
        var sectionRange2 = new SectionRange(range2);

        Assert.That(sectionRange1.FullOverlap(sectionRange2), Is.EqualTo(fullOverlap));
        Assert.That(sectionRange2.FullOverlap(sectionRange1), Is.EqualTo(fullOverlap));
    }

    [Test]
    [TestCase("2-4", "6-8", false)]
    [TestCase("2-3", "4-5", false)]
    [TestCase("5-7", "7-9", true)]
    [TestCase("2-8", "3-7", true)]
    [TestCase("6-6", "4-6", true)]
    [TestCase("2-6", "4-8", true)]
    public void AnyOverlap(string range1, string range2, bool anyOverlap)
    {
        var sectionRange1 = new SectionRange(range1);
        var sectionRange2 = new SectionRange(range2);

        Assert.That(sectionRange1.AnyOverlap(sectionRange2), Is.EqualTo(anyOverlap));
        Assert.That(sectionRange2.AnyOverlap(sectionRange1), Is.EqualTo(anyOverlap));
    }
}