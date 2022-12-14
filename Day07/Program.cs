var _lines = File.ReadAllLines("input.txt");

Console.WriteLine($"{_lines.Length} lines in file");

var _current = "";

var _items = new List<Item>();

_items.Add(GetItem("dir /", null));

foreach (var line in _lines)
{
    if (line.StartsWith("$ ls"))
    {
        continue;
    }
    else if (line.StartsWith("$ cd"))
    {
        _current = MoveTo(line, _current);
    }
    else
    {
        var item = GetItem(line, _current);

        // only add once
        if (_items.Where(i => i.Parent == item.Parent && i.Name == item.Name).Count() == 0)
        {
            _items.Add(item);
        }
    }
}

// recursively sum dir sizes
var root = _items.Where(i => i.Name == "/" && i.Type == "D").First();
SetDirectorySize(root);

// Find all of the directories with a total size of at most 100000. What is the sum of the total sizes of those directories?
var sum = _items.Where(i => i.Type == "D" && i.Size <= 100000).Sum(i => i.Size);

Console.WriteLine($"sum of sizes {sum}");

// Find the smallest directory that, if deleted, would free up enough space on the filesystem to run the update. What is the total size of that directory?
var freeSpace = 70000000d - _items.First(i => i.Name == "/").Size;
var neededSpace = 30000000d - freeSpace;

Console.WriteLine($"freeSpace {freeSpace}, neededSpace {neededSpace}");

var dirToDelete = _items.Where(i => i.Type == "D" && i.Size > neededSpace).OrderBy(i => i.Size).Take(1).First();

Console.WriteLine($"{dirToDelete.Type}  {dirToDelete.Parent}  {dirToDelete.Name}  {dirToDelete.Size}");


void SetDirectorySize(Item item)
{
    var parentName = item.Name == "/" ? "/" : item.Parent == "/" ? $"/{item.Name}" : $"{item.Parent}/{item.Name}";

    item.Size += _items.Where(i => i.Parent == parentName && i.Type == "F").Sum(f => f.Size);

    var childDirs = _items.Where(i => i.Parent == parentName && i.Type == "D");

    foreach (var child in childDirs)
    {
        SetDirectorySize(child);
        item.Size += child.Size;
    }
}

string MoveTo(string line, string current)
{
    var parts = line.Split();

    if (parts[2] == "/")
    {
        return "/";
    }
    else if (parts[2] == "..")
    {
        var currentParts = current.Split("/");
        return currentParts.Length <= 2 ? "/" : string.Join("/", currentParts.Take(currentParts.Length - 1));
    }
    else
    {
        return current == "/" ? $"/{parts[2]}" : $"{current}/{parts[2]}";
    }
}

Item GetItem(string line, string? parent)
{
    var parts = line.Split();

    var type = parts[0] == "dir" ? "D" : "F";
    var name = parts[1];
    var size = type == "F" ? long.Parse(parts[0]) : 0;

    return new Item { Parent = parent, Name = name, Type = type, Size = size };
}

class Item
{
    public string? Parent { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public long? Size { get; set; }
}