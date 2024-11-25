using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

class Program
{
    static GoalManager goalManager = new GoalManager();

    static void Main(string[] args)
    {
        while (true)
        {
            DisplayMenu();
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    goalManager.AddGoal();
                    break;
                case "2":
                    goalManager.DisplayGoals();
                    break;
                case "3":
                    goalManager.SaveGoals();
                    break;
                case "4":
                    Console.Write("What is the name of the file: ");
                    string filename = Console.ReadLine().Trim();
                    goalManager.LoadGoals(filename);
                    break;
                case "5":
                    goalManager.RecordEvent();
                    break;
                case "6":
                    goalManager.DisplayReminders(7);
                    break;
                case "7":
                    Console.WriteLine("Quit program.");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please type a number from 1-7.");
                    break;
            }
        }
    }

    static void DisplayMenu()
    {
        Console.WriteLine("\nWelcome to the Eternal Quest Program!");
        Console.WriteLine("1. Create New Goal");
        Console.WriteLine("2. List Goals");
        Console.WriteLine("3. Save Goals");
        Console.WriteLine("4. Load Goals");
        Console.WriteLine("5. Record Event");
        Console.WriteLine("6. Display Remainders");
        Console.WriteLine("7. Quit");
        Console.Write("Select a choice from the menu: ");
    }
}