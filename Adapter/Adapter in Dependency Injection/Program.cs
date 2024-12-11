using Autofac;

namespace Adapter_in_Dependency_Injection;

public interface ICommad
{
    void Execute();
}

public class SaveCommand : ICommad
{
    public void Execute()
    {
        Console.WriteLine("Saving a file");
    }
}

public class OpenCommand : ICommad
{
    public void Execute()
    {
        Console.WriteLine("Opening a file");
    }
}

public class Button
{
    private ICommad _command;
    private string _name;

    public Button(ICommad command, string name)
    {
        _command = command;
        _name = name;
    }

    public void Click()
    {
        _command.Execute();
    }

    public void PrintMe()
    {
        Console.WriteLine($"I am a button called {_name}");
    }
}

public class Editor
{
    private IEnumerable<Button> buttons;

    public Editor(IEnumerable<Button> buttons)
    {
        this.buttons = buttons;
    }

    public void ClickAll()
    {
        foreach (var button in buttons)
        {
            button.Click();
        }

    }

}

internal class Program
{
    static void Main(string[] args)
    {
        var b = new ContainerBuilder();
        b.RegisterType<SaveCommand>().As<ICommad>();
        b.RegisterType<OpenCommand>().As<ICommad>();
        //b.RegisterType<Button>();
        b.RegisterAdapter<ICommad, Button>(cmd => new Button(cmd));
        b.RegisterType<Editor>();

        using (var c = b.Build())
        {
            var editor = c.Resolve<Editor>();
            editor.ClickAll();
        }
    }
}
