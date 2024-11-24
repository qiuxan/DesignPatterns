using static Open_Closed_Principle.Program;

namespace Open_Closed_Principle;

internal class Program
{
    public enum Color
    {
        Red, Green, Blue
    }

    public enum Size
    {
        Small, Medium, Large, Yuge
    }
    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            if(name == null)
            {
                throw new ArgumentNullException(paramName: nameof(name));
            }
            Name = name;
            Color = color;
            Size = size;
        }
    }

    public class ProductFilter
    {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products,
            Size size)
        {
            foreach(var p in products)
                if (p.Size == size)
                    yield return p;
        }

        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products,
            Color color)
        {
            foreach (var p in products)
                if (p.Color == color)
                    yield return p;
        }

        public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products,
            Size size, Color color)
        {
            foreach (var p in products)
                if (p.Size == size && p.Color == color)
                    yield return p;
        }
    }


    public interface ISpecification<T>
    {
        bool IsSatisfied(Product p);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }


    public class ColorSpecification : ISpecification<Product>
    {
        private Color _color;

        public ColorSpecification(Color color)
        {
            _color = color;
        }
        bool ISpecification<Product>.IsSatisfied(Product p)
        {
            return p.Color == _color;
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private Size _size;

        public SizeSpecification(Size size)
        {
            _size = size;
        }
        bool ISpecification<Product>.IsSatisfied(Product p)
        {
            return p.Size == _size;
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> _first, _second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            _first = first ?? throw new ArgumentNullException(paramName: nameof(first));
            _second = second ?? throw new ArgumentNullException(paramName: nameof(second));
        }

        public bool IsSatisfied(Product p)
        {
            return _first.IsSatisfied(p) && _second.IsSatisfied(p);
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var i in items)
                if (spec.IsSatisfied(i))
                    yield return i;
        }
      
    }

    static void Main(string[] args)
    {
        var apple = new Product("Apple", Color.Green, Size.Small);
        var tree = new Product("Tree", Color.Green, Size.Large);
        var house = new Product("House", Color.Blue, Size.Large);

        Product[] products = { apple, tree, house };

        var pf = new ProductFilter();
        Console.WriteLine("Green products (old):");
        foreach (var p in pf.FilterByColor(products, Color.Green))
            Console.WriteLine($" - {p.Name} is green");


        var bf = new BetterFilter();
        Console.WriteLine("Green products (new):");
        foreach (var p in bf.Filter(products, new ColorSpecification(Color.Green)))
            Console.WriteLine($" - {p.Name} is green");

        Console.WriteLine("Large products");
        foreach (var p in bf.Filter(products, new SizeSpecification(Size.Large)))
            Console.WriteLine($" - {p.Name} is large");

        Console.WriteLine("Large blue items");
        foreach (var p in bf.Filter
            ( new AndSpecification<Product>
                (
                    new ColorSpecification(Color.Blue),
                    new SizeSpecification(Size.Large)
                )))
                   {
                      Console.WriteLine($" - {p.Name} is big and blue");

                   }

    }
}
