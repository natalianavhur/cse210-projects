

using System;

class Program
{
    static void Main(string[] args)
    {
        string firstName = "Sean";
        firstName += " lastname";

        Console.WriteLine($"Your name is {firstName}");
        List<string> names = new List<string>();
        names.Add(firstName);
        names.Add("John");
        Console.WriteLine("Count");
        Console.WriteLine(names.Count);

        List<int> numbers = new List<int>();
        numbers.Add(42);

        Console.WriteLine("For Loop:");
        for (int i = 0; i < names.Count; i++)
        {
            Console.WriteLine(names[i]);
        }
        Console.WriteLine("Foreach Loop:");
        foreach(string n in names){
            Console.WriteLine(n);
        }





    }
}