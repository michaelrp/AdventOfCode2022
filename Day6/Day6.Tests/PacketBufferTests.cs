namespace Day6.Tests;

public class PacketBufferTests
{
    [Test]
    [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7)]
    [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
    [TestCase("nppdvjthqldpwncqszvftbrmjlhg", 6)]
    [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
    [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
    public void FindFirstMarker_FourUnique(string input, int length)
    {
        Assert.That(PacketBuffer.FindFirstMarker(input, 4), Is.EqualTo(length));
    }

    [Test]
    [TestCase("abc", true)]
    [TestCase("aonewq", true)]
    [TestCase("abijha", false)]
    [TestCase("yy", false)]
    public void IsUnique(string input, bool expectedResult)
    {
        Assert.That(PacketBuffer.IsUnique(input), Is.EqualTo(expectedResult));
    }
}