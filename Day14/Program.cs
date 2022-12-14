var lines = File.ReadAllLines("input.txt");
Console.WriteLine($"{lines.Length} in file");

var spaces = GetSpaces(lines);
var start = new int[] { 500, 0 };
var done = false;

var left = spaces.Keys.Select(k => k.X).Min();
var right = spaces.Keys.Select(k => k.X).Max();
var top = spaces.Keys.Select(k => k.Y).Min();
var bottom = spaces.Keys.Select(k => k.Y).Max();
var floor = bottom + 2;

Console.WriteLine($"left {left} right {right} top {top} bottom {bottom} floor {floor}");

while (!done)
{
    var sand = DropSand(start[0], start[1]);

    if (sand[0] > 0)
        spaces[new Space(sand)] = "o";
}

var sands = spaces.Values.Where(v => v == "o").Count();

Console.WriteLine($"sands {sands}");

int[] DropSand(int x, int y)
{
    // Part 1 test:
    // if (x < left || x > right || y > bottom)
    // {
    //     done = true;
    //     return new int[] { -1, -1};
    // }

    if (!spaces.ContainsKey(new Space(x, y + 1)) && y + 1 < floor)
        return DropSand(x, y + 1);

    if (!spaces.ContainsKey(new Space(x - 1, y + 1)) && y + 1 < floor)
        return DropSand(x - 1, y + 1);
    else if (!spaces.ContainsKey(new Space(x + 1, y + 1)) && y + 1 < floor)
        return DropSand(x + 1, y + 1);

    // Part 2 test:
    if (x == 500 && y == 0 && spaces.Values.Any(v => v == "o"))
    {
        done = true;
    }

    return new int[] { x, y };
}

Dictionary<Space, string> GetSpaces(string[] ls)
{
    var s = new Dictionary<Space, string>();

    foreach (var line in ls)
    {
        var points = line.Split(" -> ");
        for (int i = 0; i < points.Length - 1; i++)
        {
            // take points in pairs and fill in spaces
            var r1 = new Space(points[i]);
            var r2 = new Space(points[i + 1]);

            s[r1] = "#";
            s[r2] = "#";

            if (r1.X == r2.X)
            {
                // fill in Y
                var set = new int[] { r1.Y, r2.Y };
                var min = set.Min();
                var max = set.Max();

                for (int y = min + 1; y < max; y++)
                    s[new Space(r1.X, y)] = "#";
            }
            else
            {
                // fill in X
                var set = new int[] { r1.X, r2.X };
                var min = set.Min();
                var max = set.Max();

                for (int x = min + 1; x < max; x++)
                    s[new Space(x, r1.Y)] = "#";
            }
        }
        foreach (var point in points)
        {
            s[new Space(point)] = "#";
        }
    }

    return s;
}

struct Space
{
    public Space(int[] points)
    {
        X = points[0];
        Y = points[1];
    }

    public Space(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Space(string coords)
    {
        var xy = coords.Split(",");
        X = int.Parse(xy[0]);
        Y = int.Parse(xy[1]);
    }

    public int X { get; }
    public int Y { get; }

    public override string ToString() => $"({X}, {Y})";
}
