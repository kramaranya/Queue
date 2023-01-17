using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
    static void Main()
    {
        var colors = new List<string> { "green", "brown", "blue", "red" };
        var query = colors.Where(c => c.Length == 3);
        Console.WriteLine(query.Count());
        colors.Remove("red");
        Console.WriteLine(query.Count());
    }
}

class Unit
{
    public int Id { get; set; }
    public string Agility { get; set; }
    public int Age { get; set; }
}