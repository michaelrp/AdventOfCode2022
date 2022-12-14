namespace Day04;

public class SectionRange
{
    public SectionRange(string range)
    {
        var limits = range.Split("-");
        Start = int.Parse(limits[0]);
        End = int.Parse(limits[1]);
    }

    public int Start { get; private set; }
    public int End { get; private set; }

    public bool FullOverlap(SectionRange sectionRange) =>
        ((Start <= sectionRange.Start && End >= sectionRange.End)
            || (Start >= sectionRange.Start && End <= sectionRange.End));

    public bool AnyOverlap(SectionRange sectionRange) =>
        ((Start >= sectionRange.Start && Start <= sectionRange.End)
            || (End >= sectionRange.Start && End <= sectionRange.End)
            || (Start <= sectionRange.Start && End >= sectionRange.End));
}