﻿using Autofac;

namespace Bridge;


public interface IRenderer
{
    void RenderCircle(float radius);
}

public class VectorRenderer : IRenderer
{
    public void RenderCircle(float radius)
    {
        Console.WriteLine($"Drawing a circle of radius {radius}");
    }
}

public class RasterRenderer : IRenderer
{
    public void RenderCircle(float radius)
    {
        Console.WriteLine($"Drawing pixels for a circle of radius {radius}");
    }
}

public abstract class Shape
{
    protected IRenderer renderer;

    public Shape(IRenderer renderer)
    {
        this.renderer = renderer;
    }

    public abstract void Draw();
    public abstract void Resize(float factor);
}

public class Circle : Shape
{
    private float radius;

    public Circle(IRenderer renderer, float radius) : base(renderer)
    {
        this.radius = radius;
    }

    public override void Draw()
    {
        renderer.RenderCircle(radius);
    }

    public override void Resize(float factor)
    {
        radius *= factor;
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        var cb = new ContainerBuilder();

        cb.RegisterType<VectorRenderer>().As<IRenderer>()
            .SingleInstance();// make a sigleton

        cb.Register((c, p) =>
            new Circle(c.Resolve<IRenderer>(),
                p.Positional<float>(0)));

        using (var c = cb.Build())
        {
            var circle = c.Resolve<Circle>(
                new PositionalParameter(0, 5.0f)
            );
            circle.Draw();
            circle.Resize(2.0f);
            circle.Draw();
        }
    }
}
