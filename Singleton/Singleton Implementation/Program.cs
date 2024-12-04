using MoreLinq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Singleton_Implementation;


public interface IDatabase
{
    int GetPopulation(string name);
}


public class SingletonDatabase : IDatabase
{
    private Dictionary<string, int> capitals;

    private SingletonDatabase()
    {
        WriteLine("Initializing database");

        capitals = File.ReadAllLines("./capital.txt")
            .Batch(2)
            .ToDictionary(
                list => list.ElementAt(0).Trim(),
                list => int.Parse(list.ElementAt(1))
            );
    }
    public int GetPopulation(string name)
    {
        return capitals[name];  
    }

    private static Lazy<SingletonDatabase> instance = 
        new Lazy<SingletonDatabase>(()=>new SingletonDatabase());
    public static SingletonDatabase Instance => instance.Value;
}

public class Program
{
    static void Main(string[] args)
    {
        var db = SingletonDatabase.Instance;
        var city = "Tokyo";
        WriteLine($"{city} has population {db.GetPopulation(city)}");
    }
}
