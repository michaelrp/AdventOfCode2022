using System.Text;

var lines = File.ReadAllLines("input.txt");
Console.WriteLine($"{lines.Length} lines in file");


// these are used to draw the grid in ViewPosition
// and do not contributed to the calculation
var minhx = 0;
var maxhx = 0;
var minhy = 0;
var maxhy = 0;

var knotCount = 10;
var knots = new int[knotCount, knotCount];

// use a simple HashSet to keep track of visited
var visited = new HashSet<string>();
visited.Add("0:0");

foreach (var move in lines)
{
    var parts = move.Split(" ");
    var dir = parts[0];
    var steps = int.Parse(parts[1]);

    for (int i = 0; i < steps; i++)
    {
        // move head
        if (dir == "U")
            knots[0, 1]++;
        else if (dir == "D")
            knots[0, 1]--;
        else if (dir == "L")
            knots[0, 0]--;
        else // R
            knots[0, 0]++;

        minhx = knots[0, 0] < minhx ? knots[0, 0] : minhx;
        maxhx = knots[0, 0] > maxhx ? knots[0, 0] : maxhx;
        minhy = knots[0, 1] < minhy ? knots[0, 1] : minhy;
        maxhy = knots[0, 1] > maxhy ? knots[0, 1] : maxhy;

        // move tails
        MoveTail(0);

    }
    // ViewPosition();
}

// Simulate your complete hypothetical series of motions.
// How many positions does the tail of the rope visit at least once?

// Simulate your complete series of motions on a larger rope with ten knots.
// How many positions does the tail of the rope visit at least once?

Console.WriteLine($"visited count {visited.Count} with {knotCount} knots");

void MoveTail(int h)
{
    var t = h + 1;

    var hx = knots[h, 0];
    var hy = knots[h, 1];
    var tx = knots[t, 0];
    var ty = knots[t, 1];

    // get distances
    var dx = Math.Abs(hx - tx);
    var dy = Math.Abs(hy - ty);

    // Console.WriteLine($"h {h} t {t} hx {hx} hy {hy} tx {tx} ty {ty} dx {dx} dy {dy}");

    // I had this as nested ifs, but refactored to switch with pattern
    // matching. Dense but very clean.
    var p = (dx, dy) switch
    {
        (2, 0) => (hx < tx ? hx + 1 : hx - 1, ty),
        (0, 2) => (tx, hy < ty ? hy + 1 : hy - 1),
        (2, 2) => (hx < tx ? hx + 1 : hx - 1, hy < ty ? hy + 1 : hy - 1),
        (2, 1) => (hx < tx ? hx + 1 : hx - 1, hy),
        (1, 2) => (hx, hy < ty ? hy + 1 : hy - 1),
        _ => (tx, ty)
    };

    knots[t, 0] = p.Item1;
    knots[t, 1] = p.Item2;

    if (t + 1 < knotCount)
        MoveTail(t);
    else
        visited.Add($"{knots[t, 0]}:{knots[t, 1]}");
}

// useful for examining locations
void ViewPosition()
{
    Console.WriteLine("");

    var gw = Math.Abs(maxhx - minhx) + 1;
    var gh = Math.Abs(maxhy - minhy) + 1;
    var xoffset = Math.Abs(minhx);
    var yoffset = Math.Abs(minhy);

    var grid = new string[gw, gh];

    // make a grid
    for (int x = 0; x < gw; x++)
        for (int y = 0; y < gh; y++)
            grid[x, y] = ".";

    // display knots in reverse order (for stacking)
    for (int i = knotCount - 1; i >= 0; i--)
    {
        var x = knots[i, 0];
        var y = knots[i, 1];
        var display = i == 0 ? "H" : i == (knotCount - 1) ? "T" : i.ToString();

        grid[xoffset, yoffset] = "s"; // starting point

        grid[x + xoffset, y + yoffset] = display;
    }

    for (int y = gh - 1; y >= 0; y--)
    {
        var sb = new StringBuilder();

        for (int x = 0; x < gw; x++)
            sb.Append(grid[x, y]);

        Console.WriteLine(sb.ToString());
    }
}