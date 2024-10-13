using System;
using System.ComponentModel;
using System.Net;

class Program
{
    static void Main(string[] args)
    {
        DisplayMenu();
    }
    static void DisplayMenu()
    {
        int option = 1;
        Entry entry = new Entry();
        Journal journal = new Journal();

        while (option != 5)
        {
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Quit");
            Console.Write("What would you like to do?\n>");
            option = int.Parse(Console.ReadLine());
            if (option == 1)
            {
                journal.AddEntry();
            }
            else if (option == 2)
            {
                journal.DisplayJournalEntries();
            }
            else if (option == 3)
            {
                journal.LoadFromFile();
            }
            else if (option == 4)
            {
                journal.SaveToFile();
            }
            else if (option == 5)
            {
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("The choice provided is not valid. Please type a number between (1,5) ");
            }
        }



    }
}



