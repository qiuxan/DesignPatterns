using static System.Console;

namespace ICloneable_is_Bad;
// ICloneable is ill-specified

public interface IPrototype<T>
{
    T DeepCopy();
}

public class Address : IPrototype<Address>
{
    public readonly string StreetName;
    public int HouseNumber;

    public Address(string streetName, int houseNumber)
    {
        StreetName = streetName;
        HouseNumber = houseNumber;
    }

    public Address(Address other)
    {
        StreetName = other.StreetName;
        HouseNumber = other.HouseNumber;

    }

    public Address DeepCopy()
    {
        return new Address(StreetName, HouseNumber);
    }

    public override string ToString()
    {
        return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
    }

}

public class Person : IPrototype<Person>
{
    public readonly string[] Names;
    public readonly Address Address;

    public Person(string[] names, Address address)
    {
        Names = names;
        Address = address;
    }

    public Person(Person other)
    { // copy constructor
        Names = other.Names;
        Address = new Address(other.Address);

    }

    public Person DeepCopy()
    {
        return new Person(Names, Address.DeepCopy());
    }

    public override string ToString()
    {
        return $"{nameof(Names)}: {string.Join(",", Names)}, {nameof(Address)}: {Address}";
    }

}
internal class Program
{
    static void Main(string[] args)
    {
        var john = new Person(new[] { "John", "Smith" }, new Address("London Road", 123));

        var jane = john.DeepCopy();

        jane.Address.HouseNumber = 321; 
        jane.Names[0] = "Jane";

        WriteLine(john);
        WriteLine(jane);
    }
}
