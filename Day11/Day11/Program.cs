var lines = File.ReadAllLines("../Day11/input.txt");
Console.WriteLine($"{lines.Length} in file");

var monkeys = new List<Monkey>();
var monkeyCount = lines.Length / 7;

var rounds = 10000;

for(int i = 0; i <= monkeyCount; i++)
{
    var start = i * 7;
    var mlines = lines[start..(start + 6)];
    monkeys.Add(new Monkey(mlines));
}

var commonDenominator = 1;

foreach(var monkey in monkeys)
{
    commonDenominator *= monkey.Divisor;
}

monkeys.ForEach(m => Console.WriteLine(m));

for(int r = 0; r < rounds; r++)
{
    foreach(var monkey in monkeys)
    {
        while(monkey.HasItem())
        {
            var worry = monkey.GetNextCalculatedWorry();
            worry = worry % commonDenominator;
            var throwTo = monkey.GetThrowTo(worry);
            monkeys[throwTo].Items.Enqueue(worry);
        }
    }
}

var sorted = monkeys.OrderByDescending(m => m.Inspected).ToList();

Console.WriteLine("");

monkeys.ForEach(m => Console.WriteLine(m));

var value = sorted[0].Inspected * sorted[1].Inspected;

Console.WriteLine("");

Console.WriteLine($"product of two values: {value}");
