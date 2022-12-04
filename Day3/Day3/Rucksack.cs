namespace Day3;

public class Rucksack
{
    public Rucksack(string contents)
    {
        Contents = contents;
        Fill(contents);
    }

    public string Contents { get; private set; }

    public string? Compartment1 { get; private set; }

    public string? Compartment2 { get; private set; }

    private void Fill(string contents)
    {
        var half = contents.Length / 2;
        Compartment1 = contents.Substring(0, half);
        Compartment2 = contents.Substring(half);
    }

    public string MatchingItem()
    {
        return FindFirstmatch(Compartment1, Compartment2);
    }

    public static string FindFirstmatch(string? s1, string? s2)
    {
        if (s1 is null) throw new ArgumentNullException(nameof(s1));
        if (s2 is null) throw new ArgumentNullException(nameof(s2));

        for (int i = 0; i < s1.Length; i++)
        {
            var n = s1.Substring(i, 1);

            for (int j = 0; j < s2.Length; j++)
            {
                if (n == s2.Substring(j, 1))
                    return n;
            }
        }

        throw new Exception("Could not find match");
    }
}