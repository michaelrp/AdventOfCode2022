var lines = File.ReadAllLines("input.txt");
Console.WriteLine($"{lines.Length} lines in file");

var rows = lines[0].Length;
var cols = lines.Length;

var grid = new int[rows, cols];

// load grid
for (int r = 0; r < rows; r++)
{
    for (int c = 0; c < cols; c++)
    {
        grid[r, c] = int.Parse(lines[r].Substring(c, 1));
    }
}

// count the number of not visible trees
// ignore the first and last row, first and last col
// need to look at every tree to the edge of the grid

var invisible = 0;
var highestScenicScore = 0;

for (int r = 1; r < rows - 1; r++)
{
    for (int c = 1; c < cols - 1; c++)
    {
        if (IsInvisibleUp(r, c)
            && IsInvisibleDown(r, c)
            && IsInvisibleLeft(r, c)
            && IsInvisibleRight(r, c))
        {
            invisible++;
        }

        var scenicScore = ScenicScoreUp(r, c) * ScenicScoreDown(r, c)
            * ScenicScoreLeft(r, c) * ScenicScoreRight(r, c);
        
        highestScenicScore = scenicScore > highestScenicScore ? scenicScore : highestScenicScore;
    }
}

// Consider your map; how many trees are visible from outside the grid?
var visible = (rows * cols) - invisible;

Console.WriteLine($"{visible} visible trees");

// Consider each tree on your map. What is the highest scenic score possible for any tree?
Console.WriteLine($"highest scenic score {highestScenicScore}");

bool IsInvisibleUp(int r, int c)
{
    var idx = r - 1;

    while (idx >= 0)
    {
        if (grid[r, c] <= grid[idx, c])
            return true;

        idx--;
    }

    return false;
}

bool IsInvisibleDown(int r, int c)
{
    var idx = r + 1;

    while (idx < rows)
    {
        if (grid[r, c] <= grid[idx, c])
            return true;

        idx++;
    }

    return false;
}

bool IsInvisibleLeft(int r, int c)
{
    var idx = c - 1;

    while (idx >= 0)
    {
        if (grid[r, c] <= grid[r, idx])
            return true;

        idx--;
    }

    return false;
}

bool IsInvisibleRight(int r, int c)
{
    var idx = c + 1;

    while (idx < cols)
    {
        if (grid[r, c] <= grid[r, idx])
            return true;

        idx++;
    }

    return false;
}

int ScenicScoreUp(int r, int c)
{
    var idx = r - 1;
    var score = 1;

    while (idx > 0)
    {
        if (grid[r, c] > grid[idx, c])
            score++;
        else
            return score;

        idx--;
    }

    return score;
}

int ScenicScoreDown(int r, int c)
{
    var idx = r + 1;
    var score = 1;

    while (idx < rows - 1)
    {
        if (grid[r, c] > grid[idx, c])
            score++;
        else
            return score;

        idx++;
    }

    return score;
}

int ScenicScoreLeft(int r, int c)
{
    var idx = c - 1;
    var score = 1;

    while (idx > 0)
    {
        if (grid[r, c] > grid[r, idx])
            score++;
        else
            return score;

        idx--;
    }

    return score;
}

int ScenicScoreRight(int r, int c)
{
    var idx = c + 1;
    var score = 1;

    while (idx < cols - 1)
    {
        if (grid[r, c] > grid[r, idx])
            score++;
        else
            return score;

        idx++;
    }

    return score;
}