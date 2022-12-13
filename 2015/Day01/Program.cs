var lines = File.ReadAllLines("input.txt");
// Console.WriteLine($"{lines.Length} in file");

var l = File.ReadAllText("input.txt");

var up = l.Where(t => t == '(').Count();
var down = l.Where(t => t == ')').Count();

Console.WriteLine($"up {up} down {down} floor = {up - down}");

var floor = 0;

for(int i = 0; i < l.Length; i++)
{
    floor = l[i] == '(' ? floor + 1 : floor - 1;
    if (floor == -1)
    {
        Console.WriteLine($"c {i + 1}");
        break;
    }
}