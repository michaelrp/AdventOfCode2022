var lines = File.ReadAllLines("input.txt");
Console.WriteLine($"{lines.Length} in file");

var sum = 0;
var ribbon = 0;

foreach(var line in lines)
{
    var parts = line.Split("x");

    var l = int.Parse(parts[0]);
    var w = int.Parse(parts[1]);
    var h = int.Parse(parts[2]);

    var s1 = l*w;
    var s2 = w*h;
    var s3 = h*l;
    var slack = new [] { s1, s2, s3 }.Min();

    sum += (s1 * 2 + s2 * 2 + s3 * 2 + slack);

    ribbon += new [] { l, w, h }.Order().Take(2).Sum() * 2;
    ribbon += (l * w * h);
}

Console.WriteLine($"sum {sum}, ribbon {ribbon}");
