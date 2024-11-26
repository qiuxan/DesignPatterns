﻿namespace Fluent_Builder_Inheritance;

public class Person
{
    public string Name;
    public string Position;

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
    }
}

public class PersonInfoBuilder
{
    protected Person person = new();

    public PersonInfoBuilder called(string name)
    {
        person.Name = name;
        return this;
    }
}

public class PersonJobBuilder : PersonInfoBuilder
{
    public PersonJobBuilder worksAsA(string position)
    {
        person.Position = position;
        return this;
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        var builder = new PersonJobBuilder();
        builder.called("Dmitri")
            .worksAsA("Quant") // not work because called returns PersonBuilder not PersonJobBuilder
            ;
    }
}
