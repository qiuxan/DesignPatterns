using System.Runtime.CompilerServices;

namespace Asynchronous_Factory_Method;

public class Foo
{
    public Foo()
    {
        //await Task.Delay(1000); // can't use await here!
    }

    public async Task<Foo> InitAsync()
    {
        await Task.Delay(1000);
        return this;
    }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        var foo = new Foo();

        await foo.InitAsync();

    }
}
