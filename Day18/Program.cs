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


foreach (var cube in cubes)
{
    var exposed = 6;

    //up
    if (cubes.Any(c => c.X == cube.X && c.Y == cube.Y + 1 && c.Z == cube.Z))
        exposed--;
    //down
    if (cubes.Any(c => c.X == cube.X && c.Y == cube.Y - 1 && c.Z == cube.Z))
        exposed--;
    //left
    if (cubes.Any(c => c.X == cube.X - 1 && c.Y == cube.Y && c.Z == cube.Z))
        exposed--;
    //right
    if (cubes.Any(c => c.X == cube.X + 1 && c.Y == cube.Y && c.Z == cube.Z))
        exposed--;
    //front
    if (cubes.Any(c => c.X == cube.X && c.Y == cube.Y && c.Z == cube.Z + 1))
        exposed--;
    //back
    if (cubes.Any(c => c.X == cube.X && c.Y == cube.Y && c.Z == cube.Z - 1))
        exposed--;

    cube.ExposedSides = exposed;

}

var sum = cubes.Select(c => c.ExposedSides).Sum();

Console.WriteLine("{0:mm:ss.ffff} - Part 1", DateTime.Now);
Console.WriteLine("{0:mm:ss.ffff} - Exposed sides {1}", DateTime.Now, sum);

// // Part 2

// Console.WriteLine();

// Console.WriteLine("{0:mm:ss.ffff} - Part 2", DateTime.Now);

class Cube
{
    public int X { get; }
    public int Y { get; }
    public int Z { get; }
    public int ExposedSides { get; set; }

    public Cube(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public override string ToString()
    {
        return $"{X},{Y},{Z}";
    }
}