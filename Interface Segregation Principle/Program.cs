namespace Interface_Segregation_Principle;

internal class Program
{
    public class Document
    {

    }
    public interface IMachine
    {
        void Print(Document d);
        void Fax(Document d);
        void Scan(Document d);
    }

    public class MultiFunctionPrinter : IMachine
    {
        public void Fax(Document d)
        {
            //
        }

        public void Print(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            //
        }
    }
    public class OldFashionedPrinter : IMachine
    {
        public void Fax(Document d)
        {
            //
        }

        public void Print(Document d)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document d)
        {
            throw new NotImplementedException();
        }
    }

    // Segregated interfaces example:

    public interface IPrinter
    {
        void Print(Document d);
    }


    public interface IScanner
    {
        void Scan(Document d);
    }

    public class Photocopier : IPrinter, IScanner
    {
        public void Print(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            //
        }
    }

    public interface IMultiFunctionDevice : IPrinter, IScanner // other interfaces are inherited
    {

    }

    public class MultifunctionMachine : IMultiFunctionDevice
    {
        private IPrinter _printer;
        private IScanner _scanner;

        public MultifunctionMachine(IPrinter printer, IScanner scanner)
        {
            if (printer == null)
            {
                throw new ArgumentNullException(paramName: nameof(printer));
            }
            if(scanner == null)
            {
                throw new ArgumentNullException(paramName: nameof(scanner));
            }
            _printer = printer;
            _scanner = scanner;
        }

        public void Print(Document d)
        {
            _printer.Print(d);
        }

        public void Scan(Document d)
        {
            _scanner.Scan(d);
        }//decorator pattern
    }


    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
