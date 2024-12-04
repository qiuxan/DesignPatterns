﻿namespace Prototype_Inheritance;

public interface IDeepCopyable<T>
    where T : new()
{
    void CopyTo(T target);

    public T DeepCopy()
    {
        T t = new T();
        CopyTo(t);
        return t;
    }
}

public class Address : IDeepCopyable<Address>
{
    public string StreetName;
    public int HouseNumber;

    public Address()
    {
        
    }
    public Address(string streetName, int houseNumber)
    {
        StreetName = streetName;
        HouseNumber = houseNumber;
    }

    public void CopyTo(Address target)
    {
        target.StreetName = StreetName;
        target.HouseNumber = HouseNumber;
    }



    public override string ToString()
    {
        return $"{nameof(StreetName)}: {StreetName}, " +
               $"{nameof(HouseNumber)}: {HouseNumber}";
    }
}

public class Person : IDeepCopyable<Person>
{
    public string[] Names;
    public Address Address;

    public Person()
    {

    }
    public Person(string[] names, Address address)
    {
        Names = names;
        Address = address;
    }

    public void CopyTo(Person target)
    {
        target.Names = (string[])Names.Clone();
        target.Address = Address.DeepCopy();//broken! without the extension method
    }



    public override string ToString()
    {
        return $"{nameof(Names)}: {string.Join(",", Names)}, " +
               $"{nameof(Address)}: {Address}";
    }
}

public class Employee : Person, IDeepCopyable<Employee>
{
    public int Salary;

    public Employee()
    {

    }
    public Employee(string[] names, Address address, int salary) : base(names, address)
    {
        Salary = salary;
    }

    public void CopyTo(Employee target)
    {
        base.CopyTo(target);
        target.Salary = Salary;
    }


    public override string ToString()
    {
        return $"{base.ToString()}, {nameof(Salary)}: {Salary}";
    }
}

public static class ExtensionMethods
{
    public static T DeepCopy<T>(this IDeepCopyable<T> item)
        where T : IDeepCopyable<T>, new()
    {
        return item.DeepCopy();
    }

    public static T DeepCopy<T>(this T pserson)
        where T : Person, new()
    {
        return ((IDeepCopyable<T>)pserson).DeepCopy();
    }
}

public class Program
{
    static void Main(string[] args)
    {
        var john = new Employee();
        john.Names = new[] { "John", "Doe" };
        john.Address = new Address 
        {
            StreetName = "London Road",
            HouseNumber = 123
        };
        john.Salary = 100000;

        var copy = john.DeepCopy();



        copy.Names[1] = "Smith";

        copy.Address.HouseNumber++;

        copy.Salary = 50000;

        Console.WriteLine(john);
        Console.WriteLine(copy);
    }
}
