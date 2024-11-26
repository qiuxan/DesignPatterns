namespace Faceted_Builder
public class Person
{
    // Address
    public string StreetAddress, Postcode, City;
    // Employment
    public string CompanyName, Position;
    public int AnnualIncome;

    public override string ToString()
    {
        return
            $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(Postcode)}: {Postcode}, {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
    }
}

public class PersonBuilder // facade
{
    //reference
    protected Person person = new Person();

}

public class PersonJobBuilder : PersonBuilder
{
    public PersonJobBuilder(Person person)
    {   
        this.person = person;
    }

    public PersonJobBuilder At(string companyName)
    {
        person.CompanyName = companyName;
        return this;
    }

    
    public PersonJobBuilder AsA(string position)
    {
        person.Position = position;
        return this;
    }

    public PersonJobBuilder Earning(int amount)
    {
        person.AnnualIncome = amount;
        return this;
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        var pb = new PersonBuilder();

    }
}
