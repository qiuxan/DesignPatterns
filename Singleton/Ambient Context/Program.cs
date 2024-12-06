
using System.Text;

namespace Ambient_Context;

public sealed class BuildingContext: IDisposable
{
    public int WallHeight = 0;
    private static Stack<BuildingContext> stack
        = new Stack<BuildingContext>();

    static BuildingContext()
    {
        // ensure there's at least one state
        stack.Push(new BuildingContext(0));
    }

    public BuildingContext(int wallHeight)
    {
        WallHeight = wallHeight;
        stack.Push(this);
    }

    public static BuildingContext Current => stack.Peek();

    public void Dispose()
    {
        // not strictly necessary
        if (stack.Count > 1)
            stack.Pop();
    }
}


public class Building
{
    public List<Wall> Walls { get; set; } = new List<Wall>();
    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var wall in Walls)
            sb.AppendLine(wall.ToString());
        return sb.ToString();
    }
}

public struct Point
{
    private int x, y;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
    }
}

public class Wall
{
    public override string ToString()
    {
        return $"{nameof(Start)}: {Start}, {nameof(End)}: {End}, {nameof(Height)}: {Height}";
    }

    public Point Start, End;
    public int Height;

    public Wall(Point start, Point end)
    {
        Start = start;
        End = end;
        Height = BuildingContext.Current.WallHeight;
    }
}

internal class Program
{
    static void Main(string[] args)
    {

        var house = new Building();

        using (new BuildingContext(3000))
        {            //gnd floor
            house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0)));
            house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000)));
            //1st floor

            using (new BuildingContext(3500))
            {
                house.Walls.Add(new Wall(new Point(0, 0), new Point(6000, 0)));
                house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000)));
            }

            //gnd
            house.Walls.Add(new Wall(new Point(5000, 0), new Point(5000, 4000)));


        }




        Console.WriteLine(house);
    }
}

