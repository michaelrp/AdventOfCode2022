using System.Text;

Console.WriteLine("{0:mm:ss.ffff} - Load", DateTime.Now);

//var jets = ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>";
var jets = File.ReadAllText("input.txt");


// Part 1
Console.WriteLine("Part 1");

var part1Falls = 2022;
var part1Height = GetHeight(part1Falls, jets, 0);

Console.WriteLine("{0:mm:ss.ffff} - Tower height: {1}", DateTime.Now, part1Height);

// Part 2
Console.WriteLine();
Console.WriteLine("Part 2");

var part2Falls = 1000000000000L;
var part2Height = GetHeight(part2Falls, jets, 0);

Console.WriteLine("{0:mm:ss.ffff} - Tower height: {1}", DateTime.Now, part2Height);

//--------------------------------------------------------

long GetHeight(long rockCount, string jets, int displayHeight)
{
    if (rockCount < 1)
        return 0;

    var rocks = GetRocks();

    var fallen = 0L;
    var space = 7;
    var jet = 0;
    var points = new List<Point>();
    var rockIndex = 0;
    var patternSize = 7;

    // Console.WriteLine("{0:mm:ss.ffff} - {1} jets, rockCount {2}", DateTime.Now, jets.Length, rockCount);

    var patterns = new Dictionary<string, long[]>();

    while (fallen < rockCount)
    {
        rockIndex = rockIndex == 5 ? 0 : rockIndex;
        var rock = rocks[rockIndex];
        var tmp = new long[rock.Length];
        rock.CopyTo(tmp, 0);
        var starty = (points.Any() ? points.Max(p => p.Y) + 1 : 0) + 3;

        // set starting x position
        for (int x = 0; x < tmp.Length; x += 2)
            tmp[x] += 2;
        for (int y = 1; y < tmp.Length; y += 2)
            tmp[y] += starty;

        var resting = false;

        while (!resting)
        {
            // trim points - no need to keep everything, just enough to block falling rocks
            if (points.Count > 500)
                points.RemoveRange(0, 100);

            jet = jet == jets.Length ? 0 : jet;

            var dir = jets.Substring(jet, 1) == "<" ? -1 : 1;

            // move left/right
            for (int x = 0; x < tmp.Length; x += 2)
                tmp[x] += dir;

            // if any contact move back
            if (Collision(tmp, points, space))
                for (int x = 0; x < tmp.Length; x += 2)
                    tmp[x] -= dir;

            // move down
            for (int y = 1; y < tmp.Length; y += 2)
                tmp[y] -= 1;

            // check for contact
            if (Collision(tmp, points, space))
            {
                // move back
                for (int y = 1; y < tmp.Length; y += 2)
                    tmp[y] += 1;

                // add current points
                for (int i = 0; i < tmp.Length; i += 2)
                {
                    points.Add(new Point(tmp[i], tmp[i + 1]));
                }

                var pattern = WriteGrid(points, patternSize, false).TrimStart('\n');
                var key = $"{rockIndex}:{jet}:{pattern}";
                var fullWidth = HasFullWidth(pattern, patternSize);

                if (fullWidth)
                {
                    if (!patterns.ContainsKey(key))
                    {
                        patterns.Add(key, new long[] { fallen + 1, points.Max(p => p.Y) + 1 });
                    }
                    else
                    {
                        var currentFallen = fallen + 1;
                        var currentHeight = points.Max(p => p.Y) + 1;

                        var phaseFallen = currentFallen - patterns[key][0];
                        var phaseHeight = currentHeight - patterns[key][1];

                        // now cheat death

                        // get remaining rocks
                        var remaining = rockCount - currentFallen;
                        // find out how many phases fit in remaining
                        var phases = remaining / phaseFallen;

                        Console.WriteLine($"fallen {currentFallen}, phases {phases}, key {key}");

                        // skip ahead 
                        fallen += phases * phaseFallen;
                        var heightToAdd = phaseHeight * phases;

                        // add heightsToAdd to all existing points
                        for (int i = 0; i < points.Count; i++)
                        {
                            points[i] = new Point(points[i].X, points[i].Y + heightToAdd);
                        }

                        // erase pattern dictionary (don't get new matches in the remainder)
                        patterns = new Dictionary<string, long[]>();
                    }
                }

                resting = true;
                fallen++;
            }
            jet++;
        }
        rockIndex++;
    }

    return points.Max(p => p.Y) + 1;
}

// need to check to see if pairs of rows completely block fallthrough
bool HasFullWidth(string pattern, int rows)
{
    for (int i = 0; i < rows - 1; i++)
    {
        var r1 = pattern.Substring(i * 7, 7);
        var r2 = pattern.Substring((i + 1) * 7, 7);

        var cols = new int[] { 0, 0, 0, 0, 0, 0, 0 };

        for (int j = 0; j < 7; j++)
            cols[j] = r1.Substring(j, 1) == "#" || r2.Substring(j, 1) == "#" ? 1 : 0;

        if (!cols.Any(c => c == 0))
            return true;
    }

    return false;
}

string WriteGrid(List<Point> pts, int displayHeight, bool lineBreaks = true)
{
    var pattern = new StringBuilder();
    pattern.AppendLine();

    var w = 7;
    var h = pts.Max(p => p.Y) + 1;
    var dh = displayHeight == -1 ? h : displayHeight;
    var lh = h - dh;

    var grid = new string[w, dh];
    for (int x = 0; x < w; x++)
        for (int y = 0; y < dh; y++)
            grid[x, y] = pts.Contains(new Point(x, y + lh)) ? "#" : ".";

    for (int y = grid.GetLength(1) - 1; y >= 0; y--)
    {
        for (int x = 0; x < w; x++)
        {
            if (x == w - 1 && lineBreaks)
                pattern.AppendLine(grid[x, y]);
            else
                pattern.Append(grid[x, y]);
        }
    }

    return pattern.ToString();
}

bool Collision(long[] sprite, List<Point> pts, int space)
{
    // look at each pare of coords for values
    for (int i = 0; i < sprite.Length; i += 2)
    {
        var point = new Point(sprite[i], sprite[i + 1]);

        if (point.X < 0
            || point.X > space - 1
            || point.Y < 0
            || pts.Exists(p => p.Collision(point)))
        {
            return true;
        }
    }

    return false;
}

List<int[]> GetRocks()
{
    // (x, y), ...
    return new List<int[]> {
        new int[] { 0, 0, 1, 0, 2, 0, 3, 0 },
        new int[] { 0, 1, 1, 0, 1, 1, 1, 2, 2, 1 },
        new int[] { 0, 0, 1, 0, 2, 0, 2, 1, 2, 2 },
        new int[] { 0, 0, 0, 1, 0, 2, 0, 3 },
        new int[] { 0, 0, 0, 1, 1, 0, 1, 1, }
    };
}

struct Point
{
    public long X { get; }
    public long Y { get; }

    public Point(long x, long y)
    {
        X = x;
        Y = y;
    }

    public bool Collision(Point point)
        => X == point.X && Y == point.Y;
}
