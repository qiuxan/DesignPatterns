
public interface IBird
{
    void Fly();
    int weight { get; set; }
}

public class Bird : IBird
{
    public int weight { get; set; }

    public void Fly()
    {
        Console.WriteLine($"Soaring in the sky with weight {weight}");
    }
}

public interface ILizard
{
    void Crawl();
    int weight { get; set; }
}

public class Lizard : ILizard
{
    public int weight { get; set; }

    public void Crawl()
    {
        Console.WriteLine($"Scooting on the ground with weight {weight}");
    }
}


public class Dragon: IBird, ILizard
{
    private Bird bird = new Bird();
    private Lizard lizard = new Lizard();

    public int weight { get; set; }


    public void Fly()
    {
        bird.Fly();
    }

    public void Crawl()
    {
        lizard.Crawl();
    }

}





static class Program
{
    static void Main(string[] args)
    {
        var dragon = new Dragon();
        dragon.weight = 123;
        dragon.Fly();
        dragon.Crawl();

    }
}
