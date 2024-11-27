﻿namespace Object_Tracking_and_Bulk_Replacement;

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


public class ReplacableThemeFactory
{
    private readonly List<WeakReference<Ref<ITheme>>> themes = new();

    private ITheme CreateThemeImpl(bool Dark)
    {
        return Dark ? new DarkTheme() : new LightTheme();
    }
    public Ref<ITheme> CreateTheme(bool dark)
    {
        var r = new Ref<ITheme>(CreateThemeImpl(dark));
        themes.Add(new(r));
        return r;

    }

    public void ReplaceTheme( bool dark)
    {
        foreach (var wr in themes)
        {
            if (wr.TryGetTarget(out var r))
            {
                r.Value = CreateThemeImpl(dark);
            }
        }
    }

}


//when we want to revert to the previous themes, we could use a Ref class

public class Ref<T>
{
    public T Value { get; set; }

    public Ref(T value)
    {
        Value = value;
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

        var replacableFactory = new ReplacableThemeFactory();
        var theme3 = replacableFactory.CreateTheme(true);
        Console.WriteLine(theme3.Value.BgrColor);
        replacableFactory.ReplaceTheme(false);
        Console.WriteLine(theme3.Value.BgrColor);


    }
}
