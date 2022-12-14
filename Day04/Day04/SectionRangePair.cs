namespace Day04;

public class SectionRangePair
{
    public SectionRangePair(string ranges)
    {
        var r = ranges.Split(",");
        SectionRange1 = new SectionRange(r[0]);
        SectionRange2 = new SectionRange(r[1]);
    }

    public SectionRange SectionRange1 { get; private set; }
    public SectionRange SectionRange2 { get; private set; }
    
    public bool IsFullOverlap => SectionRange1.FullOverlap(SectionRange2);

    public bool IsAnyOverlap => SectionRange1.AnyOverlap(SectionRange2);
}