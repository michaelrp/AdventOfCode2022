
var sensors = LoadSensors("input.txt");
Console.WriteLine($"{sensors.Count} sensors");

// PART 1

var ytest = 2000000;

var coverage = GetTotalCoverage(sensors, ytest, true);
Console.WriteLine($"PART 1: coverage {coverage.Length}");
Console.WriteLine();

// PART 2

var bmin = 0;
var bmax = 4000000;

var edges = new Dictionary<string, int>();
sensors.ForEach(s => {
    Console.WriteLine(s);
    s.PopulateEdges(edges, bmin, bmax);
});

Console.WriteLine($"edges {edges.Count}");

var potential = edges.Where(kv => kv.Value >= 4).First();

var pt = potential.Key.Split(":");

long freq = long.Parse(pt[0]) * 4000000L + long.Parse(pt[1]);

Console.WriteLine();
Console.WriteLine($"PART 2: frequency {freq}");

// // To see the ouput of the test data coverage
// void WriteGrid(int[,] wgrid, int min, int max)
// {
//     Console.WriteLine();
//     for (int y = min; y <= max; y++)
//     {
//         for (int x = min; x <= max; x++)
//         {
//             if (x == max)
//                 Console.WriteLine(wgrid[x, y] == 0 ? "." : wgrid[x, y] == 1 ? "X" : "S");
//             else
//                 Console.Write(wgrid[x, y] == 0 ? ". " : wgrid[x, y] == 1 ? "X " : "S ");
//         }
//     }
// }

// // To see the output of the test data edges
// void WriteEdges(IEnumerable<KeyValuePair<string, int>> e, int min, int max)
// {
//     Console.WriteLine();
//     var g = new string[max + 1, max + 1];

//     for (int x = min; x <= max; x++)
//         for (int y = min; y <= max; y++)
//             g[x, y] = ".";

//     foreach (var p in e)
//     {
//         var pt = p.Key.Split(":");
//         g[int.Parse(pt[0]), int.Parse(pt[1])] = p.Value.ToString();
//     }

//     for (int y = min; y <= max; y++)
//     {
//         for (int x = min; x <= max; x++)
//             Console.Write($"{g[x, y]} ");
//         Console.WriteLine();
//     }
// }

int[] GetTotalCoverage(List<Sensor> snrs, int y, bool excludeBeacon)
{
    var sen = new HashSet<int>();
    snrs.ForEach(s =>
        s.XCoverage(y, excludeBeacon).ToList().ForEach(x => sen.Add(x))
    );
    return sen.ToArray();
}

List<Sensor> LoadSensors(string filename)
{
    var lines = File.ReadAllLines(filename);

    var result = new List<Sensor>();

    foreach (var line in lines)
    {
        var s = line.Substring(10, line.IndexOf(":") - 10).Split(", ");
        var b = line.Substring(line.LastIndexOf("x=")).Split(", ");

        var sensor = new Sensor(
            int.Parse(s[0].Substring(2)),
            int.Parse(s[1].Substring(2)),
            int.Parse(b[0].Substring(2)),
            int.Parse(b[1].Substring(2))
        );

        result.Add(sensor);
    }

    return result;
}

class Sensor
{
    public Sensor(int x, int y, int bx, int by)
    {
        X = x;
        Y = y;
        BX = bx;
        BY = by;

        Distance = Math.Abs(X - BX) + Math.Abs(Y - BY);
    }

    public int X { get; private set; }
    public int Y { get; private set; }

    public int BX { get; private set; }
    public int BY { get; private set; }

    public int Distance { get; private set; }

    public void PopulateGrid(int[,] cgrid, int min, int max)
    {
        for (int y = Y - Distance; y <= Y + Distance; y++)
        {
            var diff = Distance - Math.Abs(Y - y);

            for (int i = 0; i <= diff; i++)
            {
                var leftx = X - diff + i;
                var rightx = X + diff - i;

                if (leftx >= min && leftx <= max && y >= min && y <= max)
                    cgrid[leftx, y] = 1;
                if (rightx >= min && rightx <= max && y >= min && y <= max)
                    cgrid[rightx, y] = 1;
            }
        }
    }

    public void PopulateEdges(Dictionary<string, int> e, int min, int max)
    {
        // add top and bottom points
        if (X >= min && X <= max)
        {
            var topy = Y - Distance - 1;
            var bottomy = Y + Distance + 1;

            if (topy >= min && topy <= max)
            {
                var t = $"{X}:{topy}";
                if (e.ContainsKey(t))
                    e[t]++;
                else
                    e.Add(t, 1);
            }

            if (bottomy >= min && bottomy <= max)
            {
                var b = $"{X}:{bottomy}";
                if (e.ContainsKey(b))
                    e[b]++;
                else
                    e.Add(b, 1);
            }
        }

        // add all other edges
        for (int y = Y - Distance; y <= Y + Distance; y++)
        {
            var diff = Distance - Math.Abs(Y - y);

            var leftx = X - diff - 1;

            if (leftx >= min && leftx <= max && y >= min && y <= max)
            {
                var l = $"{leftx}:{y}";
                if (e.ContainsKey(l))
                    e[l]++;
                else
                    e.Add(l, 1);
            }

            var rightx = X + diff + 1;
            if (rightx >= min && rightx <= max && y >= min && y <= max)
            {
                var r = $"{rightx}:{y}";
                if (e.ContainsKey(r))
                    e[r]++;
                else
                    e.Add(r, 1);
            }
        }
    }

    public int[] XCoverage(int y, bool excludeBeacon)
    {
        var cx = new List<int>();
        var diff = Distance - Math.Abs(Y - y);

        if (diff > 0) cx.Add(X);

        for (int i = 0; i < diff; i++)
        {
            cx.Add(X - diff + i);
            cx.Add(X + diff - i);
        }

        if (excludeBeacon && BY == y)
            cx.Remove(BX);

        return cx.ToArray();
    }

    public override string ToString()
    {
        return $"s ({X},{Y}); b ({BX},{BY}); d {Distance}";
    }
}