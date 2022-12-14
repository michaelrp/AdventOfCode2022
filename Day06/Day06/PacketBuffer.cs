namespace Day06;

public static class PacketBuffer
{
    public static int FindFirstMarker(string input, int uniqueLength)
    {
        if (input.Length < uniqueLength)
            throw new ArgumentException($"Input of '{input}' too short, must be at least {uniqueLength} characters.", nameof(input));

        int startPos = 0;

        while (startPos + uniqueLength <= input.Length)
        {
            var clip = input.Substring(startPos, uniqueLength);
            if (IsUnique(clip))
                return startPos + uniqueLength;

            startPos++;
        }

        return 0;
    }

    public static bool IsUnique(string input)
    {
        var set = new HashSet<char>(input.ToArray());
        return set.Count == input.Length;
    }
}