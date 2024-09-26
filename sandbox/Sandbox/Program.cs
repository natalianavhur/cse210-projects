using System;
using System.Runtime.CompilerServices;

class Program
{
    static void Main(string[] args)
    {
        List<string> gList = new List<string>();
        bool condition = true;

        while (condition)
        {
            Console.WriteLine("Add Item:");
            string item = Console.ReadLine();
            if ("no" == item)
            {
                condition = false;
            }
            else
            {
                gList.Append(item);
            }
            Console.WriteLine("Grocery List:");
            foreach(string i in gList){
                Console.WriteLine($"* {i}");
            }

        }

    }
}