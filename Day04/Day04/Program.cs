var lines = File.ReadAllLines("../Day04/input.txt");

Console.WriteLine($"{lines.Length} lines in file");

var sectionRangePairs = lines.Select(l => new SectionRangePair(l)).ToList();

// In how many assignment pairs does one range fully contain the other?

var fullOverlap = sectionRangePairs.Where(s => s.IsFullOverlap).Count();

Console.WriteLine($"count full overlap {fullOverlap}");

// In how many assignment pairs do the ranges overlap?

var anyOverlap = sectionRangePairs.Where(s => s.IsAnyOverlap).Count();

Console.WriteLine($"count any overlap {anyOverlap}");