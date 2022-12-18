var lines = File.ReadAllLines("input.txt");
var valves = lines.Select(l => new Valve(l)).ToList();

// valves.ForEach(v => Console.WriteLine(v));

var minutes = 30;

// mins remaining after open * rate = value
// potential = (mins - distance - 1) * rate = value

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

var paths = new Dictionary<string, int>();
var mostRelased = (Path: "", Released: 0);

var start = valves.First(v => v.Id == "AA");

GetPotentials(minutes, "AA", new string[] { }, 0, 0, "AA");

// var sortedPaths = paths.OrderByDescending(kv => kv.Value).ToDictionary(kv => kv.Key, kv => kv.Value);

Console.WriteLine($"Most: {mostRelased.Released}: {mostRelased.Path}");

// find all potential values
void GetPotentials(int remaining, string fromId, string[] visited, int released, int depth, string path)
{
    if (remaining < 3) // no point as there is no time to move and open
        return;

    var from = valves!.First(v => v.Id == fromId);
    var toVisit = ratedValves!.Except(visited).ToArray();

    foreach (var toId in toVisit)
    {
        var to = valves!.First(v => v.Id == toId);

        int distance = valveDistances!.Where(kv => kv.Key == $"{fromId}:{toId}" || kv.Key == $"{toId}:{fromId}").First().Value;
        var remain = remaining - distance - 1;

        if (remain > 0)
        {
            var potential = (remain * to.Rate) + released;

            if (!paths.ContainsKey(path))
                paths.Add(path, potential);

            if (potential > mostRelased.Released)
                mostRelased = (Path: path, Released: potential);

            // Console.WriteLine($"{new string('\t', depth)}{fromId}->{toId}: (({remaining} - {distance} - 1) * {to.Rate}) + {released} = {potential}");

            var newVisited = visited.Concat(new string[] { toId }).ToArray();

            GetPotentials(remain, toId, newVisited, potential, depth + 1, path += $"->{toId}");
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