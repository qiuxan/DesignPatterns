using System.Collections;
using System.Collections.ObjectModel;
using MoreLinq;

namespace Vector_Raster_Demo;

public class Point
{
    public int x, y;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;

    }

    protected bool Equals(Point other)
    {
        return x == other.x && y == other.y;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Point)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, y);
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

    protected bool Equals(Line other)
    {
        return Start.Equals(other.Start) && End.Equals(other.End);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Line)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Start, End);
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
public class LineToPointAdapter : IEnumerable<Point>
{
    private static int count = 0;
    static Dictionary<int, List<Point>> cache = new Dictionary<int, List<Point>>();
    public LineToPointAdapter(Line line)
    {
        var hash = line.GetHashCode();
        if (cache.ContainsKey(hash)) return;

        Console.WriteLine($"{++count}: Generating points for line [{line.Start.x},{line.Start.y}]-[{line.End.x},{line.End.y}]");
        var points = new List<Point>();

        int left = Math.Min(line.Start.x, line.End.x);
        int right = Math.Max(line.Start.x, line.End.x);
        int top = Math.Min(line.Start.y, line.End.y);
        int bottom = Math.Max(line.Start.y, line.End.y);
        int dx = right - left;
        int dy = line.End.y - line.Start.y;

        if (dx == 0)
        {
            for (int y = top; y <= bottom; ++y)
            {
               points.Add(new Point(left, y));
            }
        }
        else if (dy == 0)
        {
            for (int x = left; x <= right; ++x)
            {
                points.Add(new Point(x, top));
            }
        }
        cache.Add(hash, points);
    }

    public IEnumerator<Point> GetEnumerator()
    {
        return cache.Values.SelectMany(x => x).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
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
        Console.Write(".");
    }
    static void Main(string[] args)
    {
        Draw();
        Draw();

    }

    private static void Draw()
    {
        foreach (var vo in vectorObjects)
        {
            foreach (var line in vo)
            {
                var adapter = new LineToPointAdapter(line);
                adapter.ForEach(DrawPoint);
            }
        }
    }
}
