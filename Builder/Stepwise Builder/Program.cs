namespace Stepwise_Builder;

public enum CarType
{
    Sedan,
    Coupe,
    Hatchback
}
public class Car
{
    public CarType Type;
    public int WheelSize;
    //task: when building a car, we want to set the type and then according to the type, set the wheel size
    //3 steps using interfaces segregation principle
    //1. define the interface for the car
    //2. define the interface for setting the wheel size
    //3. define the car builder
}

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
