var lines = File.ReadAllLines("../Day5/input.txt");

Console.WriteLine($"{lines.Length} lines in file");

var moves = lines.Skip(10).Select(l => new Move(l)).ToList();

// move one crate at a time

var stacks = GetStacks();

foreach (var move in moves)
{
    var fromStack = move.From - 1;
    var toStack = move.To - 1;

    for (int i = 0; i < move.Crates; i++)
        stacks[toStack].Push(stacks[fromStack].Pop());
}

var topCrates = "";

stacks.ForEach(s => topCrates += s.Pop());

Console.WriteLine($"top crates {topCrates}");


// move multiple crates at one time

var multiStacks = GetStacks();

foreach (var move in moves)
{
    var fromStack = move.From - 1;
    var toStack = move.To - 1;

    var tempStack = new Stack<string>();

    for (int i = 0; i < move.Crates; i++)
        tempStack.Push(multiStacks[fromStack].Pop());

    for (int i = 0; i < move.Crates; i++)
        multiStacks[toStack].Push(tempStack.Pop());
}

var multiTopCrates = "";

multiStacks.ForEach(s => multiTopCrates += s.Pop());

Console.WriteLine($"multi top crates {multiTopCrates}");


// just brute forcing the initial stacks
static List<Stack<string>> GetStacks() =>
    new List<Stack<string>> {
        new Stack<string>(new string[] { "B", "W", "N" }),
        new Stack<string>(new string[] { "L", "Z", "S", "P", "T", "D", "M", "B" }),
        new Stack<string>(new string[] { "Q", "H", "Z", "W", "R" }),
        new Stack<string>(new string[] { "W", "D", "V", "J", "Z", "R" }),
        new Stack<string>(new string[] { "S", "H", "M", "B" }),
        new Stack<string>(new string[] { "L", "G", "N", "J", "H", "V", "P", "B" }),
        new Stack<string>(new string[] { "J", "Q", "Z", "F", "H", "D", "L", "S" }),
        new Stack<string>(new string[] { "W", "S", "F", "J", "G", "Q", "B" }),
        new Stack<string>(new string[] { "Z", "W", "M", "S", "C", "D", "J" })
    };