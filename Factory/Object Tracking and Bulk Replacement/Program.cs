namespace Object_Tracking_and_Bulk_Replacement;

using System;
using System.Collections.Generic;
using System.Text;

public interface ITheme
{
    string TextColor { get; }
    string BgrColor { get; }
}

class LightTheme : ITheme
{
    public string TextColor => "black";
    public string BgrColor => "white";
}

class DarkTheme : ITheme
{
    public string TextColor => "white";
    public string BgrColor => "dark gray";
}

public class TrackingThemeFactory
{
    private readonly List<WeakReference<ITheme>> themes = new();

    public ITheme CreateTheme(bool isDark)
    {
        ITheme theme
            = isDark ? new DarkTheme() : new LightTheme();
        themes.Add(new WeakReference<ITheme>(theme));
        return theme;
    }

    public string Info
    {
        get
        {
            var sb = new StringBuilder();
            foreach (var reference in themes)
            {
                if (reference.TryGetTarget(out var theme))
                {
                    bool dark = theme is DarkTheme;
                    sb.Append(dark ? "Dark" : "Light").AppendLine(" theme");
                }

            }

            return sb.ToString();
        }
    }
}

public class Program
{
    static void Main(string[] args)
    {
        var factory = new TrackingThemeFactory();
        var theme1 = factory.CreateTheme(true);
        var theme2 = factory.CreateTheme(false);

        Console.WriteLine(factory.Info);
    }
}
