Console.WriteLine("{0:mm:ss.ffff} - Load", DateTime.Now);

//var lines = File.ReadAllLines("testinput.txt");
var lines = File.ReadAllLines("input.txt");

var cubes = new List<Cube>();

foreach (var line in lines)
{
    var parts = line.Split(",");
    cubes.Add(new Cube(
        int.Parse(parts[0]),
        int.Parse(parts[1]),
        int.Parse(parts[2])
    ));
}

// Part 1
// load them all, then iterate them all identifying when adjacent and storing count
// sum result

SetExposure(cubes);

var sum = cubes.Select(c => c.ExposedSides).Sum();

Console.WriteLine("{0:mm:ss.ffff} - Part 1", DateTime.Now);
Console.WriteLine("{0:mm:ss.ffff} - Exposed sides {1}", DateTime.Now, sum);

// Part 2
// DFS

Console.WriteLine();

Console.WriteLine("{0:mm:ss.ffff} - Part 2", DateTime.Now);

var sides = GetDFSExposedSides(cubes);

Console.WriteLine("{0:mm:ss.ffff} - Sides from DFS {1}", DateTime.Now, sides);

int GetDFSExposedSides(List<Cube> cubes)
{
    int sides = 0;

    var maxx = cubes.Select(c => c.X).Max() + 1;
    var maxy = cubes.Select(c => c.Y).Max() + 1;
    var maxz = cubes.Select(c => c.Z).Max() + 1;

    var minx = cubes.Select(c => c.X).Min() - 1;
    var miny = cubes.Select(c => c.Y).Min() - 1;
    var minz = cubes.Select(c => c.Z).Min() - 1;

    Console.WriteLine("max X {0}->{1}, Y {2}->{3}, Z {4}->{5}", minx, maxx, miny, maxy, minz, maxz);

    var stack = new Stack<Cube>();
    var visited = new List<string>();
    var starting = new Cube(0, 0, 0);

    stack.Push(starting);

    while (stack.Count > 0)
    {
        var cube = stack.Pop();

        if (visited.Contains(cube.ToString()))
            continue;

        visited.Add(cube.ToString());

        //up
        if (cube.Y + 1 <= maxy)
            if (cubes.Any(c => c.X == cube.X && c.Y == cube.Y + 1 && c.Z == cube.Z))
                sides++;
            else
                stack.Push(new Cube(cube.X, cube.Y + 1, cube.Z));

        //down
        if (cube.Y - 1 >= miny)
            if (cubes.Any(c => c.X == cube.X && c.Y == cube.Y - 1 && c.Z == cube.Z))
                sides++;
            else
                stack.Push(new Cube(cube.X, cube.Y - 1, cube.Z));

        //left
        if (cube.X - 1 >= minx)
            if (cubes.Any(c => c.X == cube.X - 1 && c.Y == cube.Y && c.Z == cube.Z))
                sides++;
            else
                stack.Push(new Cube(cube.X - 1, cube.Y, cube.Z));

        //right
        if (cube.X + 1 <= maxx)
            if (cubes.Any(c => c.X == cube.X + 1 && c.Y == cube.Y && c.Z == cube.Z))
                sides++;
            else
                stack.Push(new Cube(cube.X + 1, cube.Y, cube.Z));

        //front
        if (cube.Z + 1 <= maxz)
            if (cubes.Any(c => c.X == cube.X && c.Y == cube.Y && c.Z == cube.Z + 1))
                sides++;
            else
                stack.Push(new Cube(cube.X, cube.Y, cube.Z + 1));

        //back
        if (cube.Z - 1 >= minz)
            if (cubes.Any(c => c.X == cube.X && c.Y == cube.Y && c.Z == cube.Z - 1))
                sides++;
            else
                stack.Push(new Cube(cube.X, cube.Y, cube.Z - 1));
    }

    return sides;
}

// // I made this nice "view output" in one more my failed attempts
// void WriteSlices(List<Cube> cs, List<Cube> air)
// {
//     var mx = cs.Select(c => c.X).Max();
//     var my = cs.Select(c => c.Y).Max();
//     var mz = cs.Select(c => c.Z).Max();

//     for (int z = 0; z <= mz; z++)
//     {
//         Console.WriteLine();
//         Console.WriteLine($"z = {z}");

//         var grid = new string[mx + 1, my + 1];
//         for (int i = 0; i < grid.GetLength(0); i++)
//             for (int j = 0; j < grid.GetLength(1); j++)
//                 grid[i, j] = " ";

//         cs.Where(c => c.Z == z).ToList().ForEach(c => grid[c.X, c.Y] = "#");
//         air.Where(c => c.Z == z).ToList().ForEach(c =>
//         {
//             if (grid[c.X, c.Y] == "#")
//                 throw new Exception($"Conflict at ({c.X},{c.Y})");
//             grid[c.X, c.Y] = ".";
//         });

//         for (int y = 0; y <= my; y++)
//         {
//             if (y % 5 == 0)
//             {
//                 for (int i = 0; i <= mx; i++)
//                     if (i == mx)
//                         Console.WriteLine("-----");
//                     else
//                         Console.Write("-");
//             }

//             for (int x = 0; x <= mx; x++)
//             {
//                 if (x % 5 == 0)
//                     Console.Write("|");

//                 if (x == mx)
//                     Console.WriteLine(grid[x, y]);
//                 else
//                     Console.Write(grid[x, y]);

//             }
//         }
//     }
// }

void SetExposure(List<Cube> cs)
{
    foreach (var cube in cs)
    {
        var exposed = 6;

        //up
        if (cs.Any(c => c.X == cube.X && c.Y == cube.Y + 1 && c.Z == cube.Z))
            exposed--;
        //down
        if (cs.Any(c => c.X == cube.X && c.Y == cube.Y - 1 && c.Z == cube.Z))
            exposed--;
        //left
        if (cs.Any(c => c.X == cube.X - 1 && c.Y == cube.Y && c.Z == cube.Z))
            exposed--;
        //right
        if (cs.Any(c => c.X == cube.X + 1 && c.Y == cube.Y && c.Z == cube.Z))
            exposed--;
        //front
        if (cs.Any(c => c.X == cube.X && c.Y == cube.Y && c.Z == cube.Z + 1))
            exposed--;
        //back
        if (cs.Any(c => c.X == cube.X && c.Y == cube.Y && c.Z == cube.Z - 1))
            exposed--;

        cube.ExposedSides = exposed;
    }
}

class Cube
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Z { get; private set; }
    public int ExposedSides { get; set; }

    public Cube(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Cube(string xyz)
    {
        var parts = xyz.Split(",");

        X = int.Parse(parts[0]);
        Y = int.Parse(parts[1]);
        Z = int.Parse(parts[2]);
    }

    public override string ToString()
    {
        return $"{X},{Y},{Z}";
    }
}