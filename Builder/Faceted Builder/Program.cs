﻿namespace Faceted_Builder;
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

    public PersonJobBuilder Works => new PersonJobBuilder(person);
    public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

    public static implicit operator Person(PersonBuilder pb)
    {
        return pb.person;
    }

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

public class PersonAddressBuilder : PersonBuilder
{
    public PersonAddressBuilder(Person person)
    {
        this.person = person;
    }

    public PersonAddressBuilder At(string streetAddress)
    {
        person.StreetAddress = streetAddress;
        return this;
    }

    public PersonAddressBuilder WithPostcode(string postcode)
    {
        person.Postcode = postcode;
        return this;
    }

    public PersonAddressBuilder In(string city)
    {
        person.City = city;
        return this;
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        var pb = new PersonBuilder();
        Person person = 
            pb
            .Lives
            .At("123 London Road")
            .In("London")
            .WithPostcode("SW12BC")
            .Works
            .At("Fabrikam")
            .AsA("Engineer")
            .Earning(123000);
        Console.WriteLine(person);

    }
}
