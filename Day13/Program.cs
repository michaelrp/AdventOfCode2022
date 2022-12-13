var lines = File.ReadAllLines("input.txt");
Console.WriteLine($"{lines.Length} in file");

var packets = new List<string>();

foreach (var line in lines)
{
    if (line != "")
        packets.Add(line);
}

Console.WriteLine($"pairs {packets.Count / 2}");

var results = new Dictionary<int, bool>();

// part 1
for (int i = 0; i < packets.Count / 2; i++)
{
    var p1 = Parse(packets[i * 2]);
    var p2 = Parse(packets[i * 2 + 1]);

    results.Add(i + 1, Comparer.ComparePackets(p1, p2) == -1 ? true : false);
}

var sum = results.Where(r => r.Value).Select(r => r.Key).Sum();

Console.WriteLine($"sum {sum}");

// part 2
packets.Add("[[2]]");
packets.Add("[[6]]");

// instead of comparing every two, sort list using comparer
var sorted = packets.Select(p => Parse(p)).ToList();
sorted.Sort(new Comparer());

var decoder2 = Parse("[[2]]");
var decoder6 = Parse("[[6]]");

var div2 = 0;
var div6 = 0;

for (int i = 0; i < sorted.Count; i++)
{
    if (Comparer.ComparePackets(decoder2, sorted[i]) == 0)
        div2 = i + 1;
    else if (Comparer.ComparePackets(decoder6, sorted[i]) == 0)
        div6 = i + 1;
}

Console.WriteLine($"div2 {div2}, div6 {div6}, product {div2 * div6}");

List<object> Parse(string input)
{
    var packet = new List<object>();
    var data = input.Substring(1, input.Length - 2);

    while (data.Length > 0)
    {
        while (!data.StartsWith("[") && data.Length > 0)
        {
            var comma = data.IndexOf(",");
            var i = data.Substring(0, comma == -1 ? data.Length : comma);
            packet.Add(int.Parse(i));
            data = data.Substring(i.Length, data.Length - i.Length);
            data = data.TrimStart(',');
        }

        if (data.StartsWith("["))
        {
            var list = data.Substring(0, ClosingBracketIndex(data) + 1);
            packet.Add(Parse(list));
            data = data.Substring(list.Length, data.Length - list.Length).TrimStart(',');
        }
    }

    return packet;
}

int ClosingBracketIndex(string data)
{
    if (!data.StartsWith("["))
        return -1;

    var opens = 0;
    var closes = 0;

    int i = 0;

    while (i < data.Length)
    {
        var c = data[i];

        if (c == '[')
            opens++;
        else if (c == ']')
            closes++;

        if (opens == closes)
            return i;

        i++;
    }

    return -1;
}

class Comparer : IComparer<List<object>>
{
    public int Compare(List<object>? p1, List<object>? p2)
    {
        return ComparePackets(p1 ?? new List<object>(), p2 ?? new List<object>());
    }

    public static int ComparePackets(List<object> left, List<object> right)
    {
        var count = new int[] { left.Count, right.Count }.Max();

        for (int i = 0; i < count; i++)
        {
            var result = (left.Skip(i).FirstOrDefault(), right.Skip(i).FirstOrDefault()) switch
            {
                (null, null) => 0,
                (null, _) => -1,
                (_, null) => 1,
                (int l, int r) => l < r ? -1 : l > r ? 1 : 0,
                (int l, List<object> r) => ComparePackets(new List<object> { l }, r),
                (List<object> l, int r) => ComparePackets(l, new List<object> { r }),
                (List<object> l, List<object> r) => ComparePackets(l, r),
                _ => 0
            };

            if (result != 0)
                return result;
        }

        return 0;
    }
}
