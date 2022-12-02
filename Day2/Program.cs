// A = Rock      1
// B = Paper     2
// C = Scissors  3

// Lose = 0
// Draw = 3
// Win =  6

var lines = File.ReadAllLines("input.txt");

Console.WriteLine($"{lines.Length} lines in file");

var partOneTotalScore = 0;
var partTwoTotalScore = 0;

foreach (var line in lines)
{
    var opponent = line.Substring(0, 1);
    var me = line.Substring(2, 1);


    // Part One
    // X = Rock      1
    // Y = Paper     2
    // Z = Scissors  3

    partOneTotalScore += me switch
    {
        "X" => 1 + opponent switch
        {
            "A" => 3,
            "B" => 0,
            "C" => 6
        },
        "Y" => 2 + opponent switch
        {
            "A" => 6,
            "B" => 3,
            "C" => 0
        },
        "Z" => 3 + opponent switch
        {
            "A" => 0,
            "B" => 6,
            "C" => 3
        }
    };


    // Part Two
    // X = lose
    // Y = draw
    // Z = win

    partTwoTotalScore += me switch
    {
        "X" => 0 + opponent switch
        {
            "A" => 3,
            "B" => 1,
            "C" => 2
        },
        "Y" => 3 + opponent switch
        {
            "A" => 1,
            "B" => 2,
            "C" => 3
        },
        "Z" => 6 + opponent switch
        {
            "A" => 2,
            "B" => 3,
            "C" => 1
        }
    };
}

Console.WriteLine($"part one total score {partOneTotalScore}");
Console.WriteLine($"part two total score {partTwoTotalScore}");