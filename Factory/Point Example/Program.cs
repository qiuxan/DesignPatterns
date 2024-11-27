namespace Point_Example;

public enum CoordinateSystem
{
    Cartesian,
    Polar
}

public static class PointFactory
{
    public static Point NewCartesianPoint(double x, double y)
    {
        return new Point(x, y);
    }

    public static Point NewPolarPoint(double rho, double theta)
    {
        return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
    }
}

public class Point
{
    private double x, y;

    public Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    //factory method

    public override string ToString()
    {
        return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        var p = new Point(2, 3);// now we can create a point like this

        var point = PointFactory.NewPolarPoint(1.0, Math.PI / 2);
        Console.WriteLine(point);

    }
}
