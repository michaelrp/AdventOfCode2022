var lines = File.ReadAllLines("input.txt");

Console.WriteLine($"{lines.Length} lines in file");

var current = 0;
var sums = new List<int>();

foreach (var line in lines)
{
    if (int.TryParse(line, out var amt))
    {
        current += amt;
    }
    else
    {
        sums.Add(current);
        current = 0;
    }
}

var max = sums.OrderDescending().First();
var top3 = sums.OrderDescending().Take(3).Sum();

Console.WriteLine($"max {max}, top3 = {top3}");
