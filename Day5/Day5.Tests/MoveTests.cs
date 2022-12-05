namespace Day5.Tests;

public class MoveTests
{
    [Test]
    [TestCase("move 1 from 5 to 4", 1, 5, 4)]
    [TestCase("move 11 from 9 to 7", 11, 9, 7)]
    public void Constructor(string input, int crates, int from, int to)
    {
        var move = new Move(input);
        
        Assert.That(move.Crates, Is.EqualTo(crates));
        Assert.That(move.From, Is.EqualTo(from));
        Assert.That(move.To, Is.EqualTo(to));
    }
}