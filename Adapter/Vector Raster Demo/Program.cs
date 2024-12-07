using System.Collections.ObjectModel;

namespace Vector_Raster_Demo;

public class Point
{
    public int x, y;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;

    }
}

public class Line
{
    public Point Start, End;

    public Line(Point start, Point end)
    {
        Start = start;
        End = end;
    }
}
public class VectorObject : Collection<Line>
{
}

public class VectorRectangle : VectorObject
{
    public VectorRectangle(int x, int y, int width, int height)
    {
        Add(new Line(new Point(x, y), new Point(x + width, y)));
        Add(new Line(new Point(x + width, y), new Point(x + width, y + height)));
        Add(new Line(new Point(x, y), new Point(x, y + height)));
        Add(new Line(new Point(x, y + height), new Point(x + width, y + height)));

    }

}


class Program
{
    private static readonly List<VectorObject> vectorObjects = new List<VectorObject>
    {
        new VectorRectangle(1, 1, 10, 10),
        new VectorRectangle(3, 3, 6, 6)
    };
    public static void DrawPoint(Point p)
    {
        Console.WriteLine(".");
    }
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
