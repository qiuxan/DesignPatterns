namespace Stepwise_Builder;

public enum CarType
{
    Sedan,
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
public interface ISpecifyCarType
{
    ISpecifyWheelSize OfType(CarType type);
}

public interface ISpecifyWheelSize
{
    IBuildCar WithWheelSize(int size);
}

public interface IBuildCar
{
    public Car Build();
}

public class CarBuilder
{
    private class Impl:
        ISpecifyCarType,
        ISpecifyWheelSize,
        IBuildCar
    {
        private Car car = new();
        public ISpecifyWheelSize OfType(CarType type)
        {
            car.Type = type;
            return this; // it is not returning a carBuilder, it is returning an Impl with ISpecifyWheelSize 
        }

        public IBuildCar WithWheelSize(int size)
        {
            switch (car.Type)
            {
                case CarType.Hatchback when size<17 || size > 20:
                case CarType.Sedan when size < 15 || size > 17:
                    throw new ArgumentException($"Wrong size of wheel for {car.Type}");
            }
            car.WheelSize = size;
            return this;
        }

        public Car Build()
        {
            return car;
            
        }
    }

    public static ISpecifyCarType Create()
    {
        return new Impl();
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        var car = CarBuilder.Create()
            .OfType(CarType.Hatchback)
            .WithWheelSize(18)
            .Build();   
    }
}
