namespace Day03;

public class Item
{
    private static string _names = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public static int Priority(string name)
    {
        return _names.IndexOf(name) + 1;
    }
}
