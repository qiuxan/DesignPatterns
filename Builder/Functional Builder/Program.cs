namespace Functional_Builder;

public class Person
{
    public string Name, Postion;
}

public sealed class PersonBuilder
{
    private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();

    public PersonBuilder Do(Action<Person> action) => AddAction(action);

    public PersonBuilder Called(string name) => AddAction(p => { p.Name = name;});

    public Person Build()
    => actions.Aggregate(new Person(), (p, f) => f(p));

    private PersonBuilder AddAction(Action<Person> action)
    {
        actions.Add(p =>
        {
            action(p);
            return p;
        });
        return this;
    }

}

public static class PersonBuilderExtensions
{
    public static PersonBuilder WorksAsA(this PersonBuilder builder, string position)
    => builder.Do(p => p.Postion = position);
}


internal class Program
{
    static void Main(string[] args)
    {
        var person = new PersonBuilder()
            .Called("Ian")
            .WorksAsA("Developer") // this is a fluent builder
            .Build();
    }
}
