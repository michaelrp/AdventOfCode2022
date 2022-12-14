var lines = File.ReadAllLines("../Day03/input.txt");

Console.WriteLine($"{lines.Length} lines in file");

var rucksacks = lines.Select(l => new Rucksack(l)).ToList();

//Find the item type that appears in both compartments of each rucksack.
//What is the sum of the priorities of those item types?

var sum = rucksacks.Sum(r => Item.Priority(r.MatchingItem()));

Console.WriteLine($"sum {sum}");

//Find the item type that corresponds to the badges of each three-Elf group.
//What is the sum of the priorities of those item types?

var groupSum = 0;

for (int i = 0; i < rucksacks.Count; i += 3)
{
    var groupContents = rucksacks.Skip(i).Take(3).Select(r => r.Contents).ToArray();
    var badge = Badge.GetBadge(groupContents);
    groupSum += Item.Priority(badge);
}

Console.WriteLine($"groupSum {groupSum}");
