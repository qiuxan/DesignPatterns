namespace Monostate;

class CEO
{
    private static string name;
    private static int age;

    public string Name
    {
        get => name;
        set => name = value;
    }
    public int Age
    {
        get => age;
        set => age = value;
    }

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
    }
}
    internal class Program
{
    static void Main(string[] args)
    {
        var ceo1 = new CEO();
        ceo1.Name = "Adam";
        ceo1.Age = 55;

        var ceo2 = new CEO();
        Console.WriteLine(ceo2);

    }

}
