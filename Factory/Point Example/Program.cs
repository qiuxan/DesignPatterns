﻿namespace Point_Example;

public enum CoordinateSystem
{
    Cartesian,
    Polar
}

public class Point
{
    private double x, y;

    private Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    //factory method
    public static Point NewCartesianPoint(double x, double y)
    {
        return new Point(x, y);
    }

    public static Point NewPolarPoint(double rho, double theta)
    {
        return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
    }

    public override string ToString()
    {
        return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        var point = Point.NewPolarPoint(1.0, Math.PI / 2);
        Console.WriteLine(point);

    }
}
