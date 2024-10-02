using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

class Program
{
    static void Main(string[] args)
    {
        List<string> gList = new List<string>();
        bool condition = true;

        while (condition)
        {
            Console.Write("Add Item:");
            string item = Console.ReadLine();
            if ("no" == item)
            {
                condition = false;
            }
            else
            {
                gList.Add(item);
            }

        }
        Console.WriteLine("Grocery List:");
        foreach (string item in gList)
        {
            Console.WriteLine($"*{item}");
        }
    }
}