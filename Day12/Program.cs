var lines = File.ReadAllLines("input.txt");
// Console.WriteLine($"{lines.Length} in file");

var w = lines[0].Length;
var h = lines.Length;

var vals = "abcdefghijklmnopqrstuvwxyz";

var graph = new List<Node>();
var shortest = -1;

for (int x = 0; x < w; x++)
{
    for (int y = 0; y < h; y++)
    {
        var id = $"{x}:{y}";
        var val = lines[y].Substring(x, 1);
        var elev = vals.IndexOf(val);
        var isStart = false;
        var isEnd = false;

        if (val == "S")
        {
            isStart = true;
            elev = 0;
        }
        else if (val == "E")
        {
            isEnd = true;
            elev = 25;
        }
        else
        {
            elev = vals.IndexOf(val);
        }

        var node = new Node(id, elev, isStart, isEnd);

        // Console.WriteLine(node);

        graph.Add(node);
    }
}

// now find connections
foreach(var node in graph)
{
    var idparts = node.Id.Split(":");
    var x = int.Parse(idparts[0]);
    var y = int.Parse(idparts[1]);

    // up
    var uId = $"{x}:{y + 1}";
    if (AddNode(uId, node.Elev))
        node.Nodes.Add(uId);

    // down
    var dId = $"{x}:{y - 1}";
    if (AddNode(dId, node.Elev))
        node.Nodes.Add(dId);

    // left
    var lId = $"{x - 1}:{y}";
    if (AddNode(lId, node.Elev))
        node.Nodes.Add(lId);

    // right
    var rId = $"{x + 1}:{y}";
    if (AddNode(rId, node.Elev))
        node.Nodes.Add(rId);
}

var starts = graph.Where(n => n.Elev == 0).Select(n => n.Id ?? "").ToArray();
//var starts = new string[] { "0:20" };

var end = graph.Where(n => n.IsEnd).First().Id;

Console.WriteLine($"starts {starts.Length}");

int counter = 0;

// for question 2, just brute forcing it...will take a bit to search 2000
// maybe could do this starting from the end and search _down_ to a, but, nah...
foreach(var a in starts)
{
    counter++;
    graph.ForEach(n =>
    {
        n.Distance = -1;
        n.Visited = false;
    });

    var dist = GetDistance(a, end);
    if (dist > 0)
        shortest = shortest < 0 || shortest >= dist ? dist : shortest;

    Console.WriteLine($"{counter}: start {a} dist {dist} shortest {shortest}");
}

Console.WriteLine($"distance {shortest}");

// graph.ToList().ForEach(n => Console.WriteLine(n));

// using Dijkstra to find distance
int GetDistance(string startId, string endId)
{
    var initial = graph.First(n => n.Id == startId);
    initial.Distance = 0;

    var current = initial;

    while(current != null && current.Id != end)
    {
        var nodesToVisit = graph.Where(n => current.Nodes.Contains(n.Id)
            && n.Visited == false).OrderByDescending(n => n.Distance);

        foreach(var neighbor in nodesToVisit)
        {
            var distance = current.Distance + 1;
            neighbor.Distance = neighbor.Distance == -1 || neighbor.Distance > distance
                ? distance
                : neighbor.Distance;
        }

        current.Visited = true;

        current = graph
            .Where(n => n.Visited == false && n.Distance >= 0)
            .OrderBy(n => n.Distance)
            .Take(1).FirstOrDefault();
    }

    return current?.Distance ?? -1;
}

bool AddNode(string id, int elev)
{
    var add = graph.FirstOrDefault(n => n.Id == id);
    return (add != null && elev >= add.Elev - 1);
}

class Node
{
    public Node (string id, int elev, bool isStart, bool isEnd )
    {
        Id = id;
        Elev = elev;
        IsStart = isStart;
        IsEnd = isEnd;
        Distance = -1;
    }
    public string Id { get; private set; } = "";
    public int Elev { get; private set; }
    public bool IsEnd { get; private set; }
    public bool IsStart { get; private set; }
    public int Distance { get; set; }
    public bool Visited { get; set; }
    public List<string> Nodes { get; set; } = new();

    public override string ToString()
    {
        return $"{Id}: Elev={Elev} Dist={Distance} Nodes={string.Join(", ", Nodes)}";
    }
}
