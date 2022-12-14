namespace Day03;

public class Badge
{
    public static string GetBadge(string[] contents)
    {
        var counts = new Dictionary<string, int>();

        for(int i = 0; i < contents.Length; i++)
        {
            var uniqueNames = contents[i].ToHashSet().ToArray();

            for (int j = 0; j < uniqueNames.Length; j++)
            {
                var n = uniqueNames[j].ToString();
                if(!counts.ContainsKey(n))
                    counts.Add(n, 0);
                counts[n] = counts[n] + 1;
            }
        }

        return counts.FirstOrDefault(c => c.Value == 3).Key;
    }
}