var lines = File.ReadAllLines("input.txt");
Console.WriteLine($"{lines.Length} lines in file");

var hx = 0;
var hy = 0;
var tx = 0;
var ty = 0;

var visited = new HashSet<string>();
visited.Add("0:0");

foreach(var move in lines)
{
    var parts = move.Split(" ");
    var dir = parts[0];
    var steps = int.Parse(parts[1]);

    for (int i = 0; i < steps; i++)
    {
        if (dir == "U")
            hy++;
        else if (dir == "D")
            hy--;
        else if (dir == "L")
            hx--;
        else // R
            hx++;
        
        // check x distance
        if (Math.Abs(hx - tx) > 1)
        {
            tx = dir == "L" ? hx + 1 : hx - 1;
            ty = hy;
        }

        // check y distance
        if (Math.Abs(hy - ty) > 1)
        {
            ty = dir == "U" ? hy - 1 : hy + 1;
            tx = hx;
        }

        visited.Add($"{tx}:{ty}");

        // Console.WriteLine($"h ({hx}, {hy}), t ({tx},{ty})");
    }

}

// Simulate your complete hypothetical series of motions.
// How many positions does the tail of the rope visit at least once?

Console.WriteLine($"visited count {visited.Count}");
