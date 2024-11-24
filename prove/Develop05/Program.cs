using System;
using System.Collections.Generic;

class Program
{
    static GoalManager goalManager = new GoalManager();
    static void Main(string[] args)
    {
        Program program = new Program();
        program.DisplayMenu();
        program.HandleInput();
    }

    public void DisplayMenu()
    {
        Console.WriteLine("Welcome to the Eternal Quest Program!");
        Console.WriteLine("1. Create New Goal");
        Console.WriteLine("2. List Goals");
        Console.WriteLine("3. Save Goals");
        Console.WriteLine("4. Load Goals");
        Console.WriteLine("5. Record Event");
        Console.WriteLine("6. Quit");
    }

    public void HandleInput()
    {
        Console.Write("Select a choice from menu: ");
        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                goalManager.AddGoal();
                break;

            case "2":
                Console.WriteLine("List Goals");
                break;

            case "3":
                Console.WriteLine("Show Goals selected.");
                break;

            case "4":
                Console.WriteLine("Display Score selected.");
                break;

            case "5":
                Console.WriteLine("Save Goals selected.");
                break;

            case "6":
                Console.WriteLine("Exiting the program.");
                return;
            default:
                Console.WriteLine("Invalid option. Try again.");
                break;
        }

        DisplayMenu();
    }
}