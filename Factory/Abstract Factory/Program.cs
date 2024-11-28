namespace Abstract_Factory;

public interface IHotDrink
{
    void Consume();
}

internal class Tea : IHotDrink
{
    public void Consume()
    {
        Console.WriteLine("This tea is nice but I'd prefer it with milk.");
    }
}

internal class Coffee : IHotDrink
{
    public void Consume()
    {
        Console.WriteLine("This coffee is sensational!");
    }
}
//every time we add a new drink, we need to add a new class

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
