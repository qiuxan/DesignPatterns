using System.Text;

namespace Builder;

internal class Program
{
    static void Main(string[] args)
    {
        var hello = "Hello";

        var sb = new StringBuilder();
        sb.Append("<p>");
        sb.Append(hello);
        sb.Append("</p>");

        Console.WriteLine(sb);

        var words = new[] { "Hello", "world" };
        Console.WriteLine(words);
        // <ul><li>hello</li><li>world</li></ul>
        sb.Clear();
        sb.Append("<ul>");
        foreach (var word in words)
        {
            sb.AppendFormat("<li>{0}</li>", word);

        }
        sb.Append("</ul>");

        Console.WriteLine(sb);

    }
}
