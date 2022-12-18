using Day16;

Console.WriteLine("{0:mm:ss.ffff} - Load", DateTime.Now);

var lines = File.ReadAllLines("input.txt");
var valves = lines.Select(l => new Valve(l)).ToList();

// valves.ForEach(v => Console.WriteLine(v));

var remainHack = 8;
var releaseHack = .5d;

Console.WriteLine("{0:mm:ss.ffff} - Get Distances", DateTime.Now);

// get a set of all the shortest distances from each valve > 0 to other valves > 0
var ratedValves = valves.Where(v => v.Rate > 0).Select(v => v.Id).ToArray();

var valveDistances = new Dictionary<string, int>();

for (int i = 0; i < ratedValves.Length; i++)
{
    valveDistances.Add($"AA:{ratedValves[i]}", GetDistance("AA", ratedValves[i]));
}

for (int i = 0; i < ratedValves.Length; i++)
{
    for (int j = i + 1; j < ratedValves.Length; j++)
    {
        valveDistances.Add($"{ratedValves[i]}:{ratedValves[j]}", GetDistance(ratedValves[i], ratedValves[j]));
    }
}

// valveDistances.ToList().ForEach(kv => Console.WriteLine($"{kv.Key} -> {kv.Value}"));

// PART 1
Console.WriteLine("{0:mm:ss.ffff} - PART 1", DateTime.Now);

// var paths = new Dictionary<Path, int>();
var mostRelased = (Path: new Path("AA"), Released: 0);
var pathsChecked = 0;

var start = valves.First(v => v.Id == "AA");

Console.WriteLine("{0:mm:ss.ffff} - Get Potentials", DateTime.Now);

GetPotentials(30, ratedValves, "AA", new string[] { }, 0, 0, new Path("AA"));

Console.WriteLine("{0:mm:ss.ffff} - Paths checked: {1}", DateTime.Now, pathsChecked);
Console.WriteLine("{0:mm:ss.ffff} - Released {1}: {2}", DateTime.Now, mostRelased.Released, mostRelased.Path.Segments);

// PART 2

// for each combination of groups
//      find best for the two of them...maybe can't use current hack to trim?

var bestPathPair = new PathPair(new Path("AA"), new Path("AA"), 0, 0);
var sets = ratedValves.Combinations(7).ToArray();

Console.WriteLine("{0:mm:ss.ffff} - {1} sets", DateTime.Now, sets.Count());
int s = 1;

foreach (var set in sets)
{
    var g1 = set.ToArray();
    var g2 = ratedValves.Except(g1).ToArray();

    // Console.WriteLine($"{string.Join(", ", g1)} :: {string.Join(", ", g2)}");

    mostRelased = (Path: new Path("AA"), Released: 0);
    pathsChecked = 0;

    GetPotentials(26, g1.ToArray(), "AA", new string[] { }, 0, 0, new Path("AA"));

    var g1released = mostRelased.Released;
    var g1path = mostRelased.Path.Segments;
    var g1pathsChecked = pathsChecked;

    mostRelased = (Path: new Path("AA"), Released: 0);
    pathsChecked = 0;

    GetPotentials(26, g2, "AA", new string[] { }, 0, 0, new Path("AA"));

    var g2released = mostRelased.Released;
    var g2path = mostRelased.Path.Segments;
    var g2pathsChecked = pathsChecked;

    if (g1released + g2released > bestPathPair.Total)
        bestPathPair = new PathPair(new Path(g1path), new Path(g2path), g1released, g2released);

    Console.WriteLine("{0:mm:ss.ffff} - [{5}] Paths: {1} + {2} = {3}; Released {4}; Best {6}", DateTime.Now,
        g1pathsChecked, g2pathsChecked, (g1pathsChecked + g2pathsChecked), (g1released + g2released),
        s, bestPathPair.Total);
    
    s++;
}

Console.WriteLine("{0:mm:ss.ffff} - Best pair: {1}: {2} :: {3}",
    DateTime.Now, bestPathPair.Total, bestPathPair.Path1.Segments, bestPathPair.Path2.Segments);

//------------------------------------------------

// find all potential values
void GetPotentials(int remaining, string[] rated, string fromId, string[] visited, int released, int depth, Path path)
{
    if (remaining < 3) // no point as there is no time to move and open
        return;

    var from = valves!.First(v => v.Id == fromId);
    var toVisit = rated!.Except(visited).ToArray();

    foreach (var toId in toVisit)
    {
        var to = valves!.First(v => v.Id == toId);

        int distance = valveDistances!.Where(kv => kv.Key == $"{fromId}:{toId}" || kv.Key == $"{toId}:{fromId}").First().Value;
        var remain = remaining - distance - 1;

        if (remain > 0)
        {
            pathsChecked++;

            var potential = (remain * to.Rate) + released;

            // A hack! if remain < 10 minutes and potential is < 50% of current mostReleased, stop checking
            if (remain < remainHack && potential / mostRelased.Released < releaseHack)
                continue;

            if (potential > mostRelased.Released)
                mostRelased = (Path: path, Released: potential);

            // Console.WriteLine($"{new string('\t', depth)}{fromId}->{toId}: (({remaining} - {distance} - 1) * {to.Rate}) + {released} = {potential}");

            var newVisited = visited.Concat(new string[] { toId }).ToArray();
            var newPath = string.Format("{0}->{1}", path.Segments, toId);

            GetPotentials(remain, rated, toId, newVisited, potential, depth + 1, new Path(newPath));
        }

    }
}

// using Dijkstra to find distance
int GetDistance(string startId, string endId)
{
    var graph = valves.Select(v => new Node(v.Id, v.TunnelsTo)).ToList();
    var initial = graph.First(n => n.Id == startId);
    initial.Distance = 0;

    var current = initial;

    while (current != null && current.Id != endId)
    {
        var nodesToVisit = graph.Where(n => current.To.Contains(n.Id)
            && n.Visited == false).OrderByDescending(n => n.Distance);

        foreach (var neighbor in nodesToVisit)
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

// only used to find set of distances, not great, don't care
class Node
{
    public string? Id { get; set; }
    public bool Visited { get; set; }
    public int Distance { get; set; }
    public string[] To { get; set; } = new string[0];

    public Node(string id, string[] to)
    {
        Id = id;
        To = to;
        Distance = -1;
    }
}

struct Path
{
    public string Segments { get; }
    public Path(string segments)
    {
        Segments = segments;
    }
}

struct PathPair
{
    public Path Path1 { get; }
    public Path Path2 { get; }
    public int Path1Released { get; }
    public int Path2Released { get; }

    public PathPair(Path p1, Path p2, int p1released, int p2released)
    {
        Path1 = p1;
        Path2 = p2;
        Path1Released = p1released;
        Path2Released = p2released;
    }

    public int Total
        => Path1Released + Path2Released;

    public override string ToString()
    {
        return string.Format("{0}: {1} ({3}) :: {2} ({4})", Total, Path1, Path2, Path1Released, Path2Released);
    }
}

class Valve
{
    public string Id { get; private set; }
    public int Rate { get; private set; }
    public string[] TunnelsTo { get; private set; }

    public Valve(string input)
    {
        var parts = input.Split("; ");

        Id = parts[0].Substring(6, 2);
        Rate = int.Parse(parts[0].Substring(parts[0].IndexOf("=") + 1));
        TunnelsTo = parts[1].Contains("tunnels lead to valves")
            ? parts[1].Substring(23).Split(", ")
            : parts[1].Substring(22).Split(", ");
    }

    public override string ToString()
    {
        return $"{Id} rate {Rate} to {string.Join(", ", TunnelsTo)}";
    }
}