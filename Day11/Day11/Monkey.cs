namespace Day11;

public class Monkey
{
    public int Id { get; private set; }
    public Queue<long> Items { get; set; } = new();
    public string Operation { get; private set; }
    public string Term { get; private set; }
    public int Divisor { get; private set; }
    public int IfTestTrue { get; private set; }
    public int IfTestFalse { get; private set; }
    public long Inspected { get; private set; }

    public Monkey(string[] init)
    {
        Id = int.Parse(init[0].Substring(7, init[0].Length - 8));
        
        init[1].Substring(18, init[1].Length - 18)
            .Split(", ")
            .Select(i => long.Parse(i)).ToList()
            .ForEach(i => Items.Enqueue(i));

        Operation = init[2].Substring(23, 1);

        Term = init[2].Substring(25, init[2].Length - 25);

        Divisor = int.Parse(init[3].Substring(21, init[3].Length - 21));

        IfTestTrue = int.Parse(init[4].Substring(29, init[4].Length - 29));

        IfTestFalse = int.Parse(init[5].Substring(30, init[5].Length - 30));
    }

    public bool HasItem()
        => Items.Count > 0;

    public long GetNextCalculatedWorry()
    {
        Inspected++;
        return CalculateNewWorry(Items.Dequeue(), Operation, Term);
    }

    public int GetThrowTo(long value)
        => ThrowTo(value, Divisor, IfTestTrue, IfTestFalse);

    public static long CalculateNewWorry(long worry, string operation, string term)
    {
        // thought at first I'd use delegates, but instead used the simple switch
        
        var newWorry = (operation, term) switch
        {
            ("*", "old") => worry * worry,
            ("-", _) => worry - long.Parse(term),
            ("+", _) => worry + long.Parse(term),
            ("*", _) => worry * long.Parse(term),
            _ => throw new Exception("Unknown calc") 
        };

        return newWorry;
    }

    public static long ReduceWorry(long worry, int divisor)
        => (long)Math.Floor((double)worry / (double)divisor);
    
    // public static long ReduceWorry(long worry, int divisor)
    //     => worry % divisor;

    public static int ThrowTo(long worry, int divisor, int trueId, int falseId)
        => worry % divisor == 0 ? trueId : falseId;

    /*
    Monkey 0:
    Monkey inspects an item with a worry level of 79.
        Worry level is multiplied by 19 to 1501.
        Monkey gets bored with item. Worry level is divided by 3 to 500.
        Current worry level is not divisible by 23.
        Item with worry level 500 is thrown to monkey 3.
    */

    public override string ToString()
    {
        var s = new StringBuilder();

        // s.Append(Id.ToString());
        // s.Append(":\n  Items: ");
        // s.Append(string.Join(", ", Items));
        // s.Append("\n  Operation: ");
        // s.Append(Operation);
        // s.Append("\n  Divisor: divisible by ");
        // s.Append(Test.ToString());
        // s.Append("\n    If true: throw to monkey ");
        // s.Append(IfTestTrue.ToString());
        // s.Append("\n    If false: throw to monkey ");
        // s.Append(IfTestFalse.ToString());

        s.Append(Id.ToString());
        s.Append(": ");
        s.Append(string.Join(", ", Items));

        return s.ToString();
    }
}

/*
Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

*/