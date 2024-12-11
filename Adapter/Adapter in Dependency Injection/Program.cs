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

    public Button(ICommad command)
    {
        _command = command;
    }

    public void Click()
    {
        _command.Execute();
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
        b.RegisterType<Button>();
        b.RegisterType<Editor>();

        using (var c = b.Build())
        {
            var editor = c.Resolve<Editor>();
            editor.ClickAll();
        }
    }
}
