using System.Diagnostics;
using static System.Console;

namespace Single_Responsibility_Principle;

internal class Program
{

    public class Journal
    {
        private readonly List<string> entries = new List<string>();

        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
            return count; // memento pattern!
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }

    }

    /**
        if we want to save the journal to a file, a better approach would be to create a separate class that is responsible for saving the journal to a file,
        Instead of adding a method to the Journal class that saves the journal to a file.
        this is so called Single Responsibility Principle.
     */

    public class Persistence
    {
        public void SaveToFile(Journal journal, string filename, bool overwrite = false)
        {
            if (overwrite || !File.Exists(filename))
            {
                File.WriteAllText(filename, journal.ToString());
            }

        }
    }

    static void Main(string[] args)
    {
        var j = new Journal();
        j.AddEntry("I cried today.");
        j.AddEntry("I ate a bug.");
        WriteLine(j);// a method from system.console

        var p = new Persistence();
        var filename = @"D:\DesignPatterns\Single Responsibility Principle\journal.txt";
        p.SaveToFile(j, filename, true);
        Process.Start(filename);

    }
}
