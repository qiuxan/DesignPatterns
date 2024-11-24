namespace Liskov_Substitution_Principle;

internal class Program
{

    public class Rectangle
    {
        public int Width { get; set; }
        public  int Height { get; set; }

        public Rectangle()
        {

        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    public class Square : Rectangle
    {
        public new int Width
        {
            set { base.Width = base.Height = value; }
        }

        public new int Height
        {
            set { base.Width = base.Height = value; }
        }
    }
    static void Main(string[] args)
    {
        static int Area(Rectangle r) => r.Width * r.Height;

        Rectangle rc = new Rectangle(2, 3);
        Console.WriteLine($"{rc} has area {Area(rc)}");

        Square sq = new Square();
        sq.Width = 4;
        Console.WriteLine($"{sq} has area {Area(sq)}");

        Rectangle sq2 = new Square(); ;
        sq2.Width = 4;
        Console.WriteLine($"{sq2} has area {Area(sq2)}");
    }
}
