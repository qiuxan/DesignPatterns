
public class Bird
{
    public void Fly()
    {
        Console.WriteLine("Soaring in the sky");
    }
}

public class Lizard
{
    public void Crawl()
    {
        Console.WriteLine("Scooting on the ground");
    }
}


public class Dragon: Bird, Lizard// cannot use multiple inheritance in C#
{

}





static class Program
{
    static void Main(string[] args)
    {
     
    }
}
