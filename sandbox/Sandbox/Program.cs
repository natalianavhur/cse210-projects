// using System;
// using System.Diagnostics;
// using System.Runtime.CompilerServices;

// class Program
// {
//     static void Main(string[] args)
//     {
//         List<string> gList = new List<string>();
//         bool condition = true;

//         while (condition)
//         {
//             Console.Write("Add Item:");
//             string item = Console.ReadLine();
//             if ("no" == item)
//             {
//                 condition = false;
//             }
//             else
//             {
//                 gList.Add(item);
//             }

//         }
//         Console.WriteLine("Grocery List:");
//         foreach (string item in gList)
//         {
//             Console.WriteLine($"*{item}");
//         }
//     }
// }
public class Car
{
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }

    // Constructor 1: No parameters
    public Car() : this("Unknown", "Unknown", 0)
    {
    }

    // Constructor 2: Two parameters
    public Car(string make, string model) : this(make, model, 0)
    {
    }

    // Constructor 3: Three parameters
    public Car(string make, string model, int year)
    {
        Make = make;
        Model = model;
        Year = year;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Car car1 = new Car();
        Car car2 = new Car("Toyota", "Corolla");
        Car car3 = new Car("Honda", "Civic", 2020);

        Console.WriteLine(car1.Make, car1.Model, car1.Year);
        Console.WriteLine(car2.Make, car2.Model, car2.Year);
        Console.WriteLine(car3.Make, car3.Model, car3.Year);

    }
}