namespace Day5;

public class Move
{
    public Move (string moveLine)
    {
        var parts = moveLine.Split(" ");
        Crates = int.Parse(parts[1]);
        From = int.Parse(parts[3]);
        To = int.Parse(parts[5]);
    }

    public int Crates { get; private set; }
    public int From { get; private set; }
    public int To { get; private set; }
}