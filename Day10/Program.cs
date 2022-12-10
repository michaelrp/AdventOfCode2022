using System.Text;

var lines = File.ReadAllLines("input.txt");

var X = 1;
var cycle = 0;

var display = new StringBuilder(40);
var linePosition = 0;

foreach (var line in lines)
{
    cycle++;
    DrawPixel();

    if (line.StartsWith("addx"))
    {
        cycle++;
        DrawPixel();

        var xvalue = int.Parse(line.Substring(5, line.Length - 5));
        X += xvalue;
    }
}

void DrawPixel()
{
    // if the sprite overlaps the line position (sprite is X - 1, X or X + 1)
    //     draw # else draw .
    var pixel = linePosition >= X - 1 && linePosition <= X + 1 ? "#" : ".";
    display.Append(pixel);

    if (cycle % 40 == 0)
    {
        Console.WriteLine(display.ToString());
        display.Clear();
        linePosition = 0;
    }
    else
    {
        linePosition++;
    }
}

// output when using 'testinput.txt'
/*
##..##..##..##..##..##..##..##..##..##..
###...###...###...###...###...###...###.
####....####....####....####....####....
#####.....#####.....#####.....#####.....
######......######......######......####
#######.......#######.......#######.....
*/
