
public interface IBird
{
    void Fly();
}

public class Bird : IBird
{
    public void Fly()
    {
        Console.WriteLine("Soaring in the sky");
    }
}

public interface ILizard
{
    void Crawl();
}

public class Lizard : ILizard
{
    public void Crawl()
    {
        Console.WriteLine("Scooting on the ground");
    }
}


public class Dragon: IBird, ILizard
{
    private Bird bird = new Bird();
    private Lizard lizard = new Lizard();

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
        dragon.Fly();
        dragon.Crawl();

    }
}
