using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Threading;
using CsvHelper.Configuration.Attributes;
using System.Reflection.Metadata.Ecma335;
using System.ComponentModel;
using System.Net;

class Program
{
    static void Main(string[] args)
    {
        bool continueProgram = true;

        while (continueProgram)
        {
            Console.Clear();
            Console.WriteLine("Main Menu");
            Console.WriteLine("   1. Start Activity");
            Console.WriteLine("   2. View Activity History");
            Console.WriteLine("   3. Exit");
            Console.Write("Select a choice from the menu: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Choose an activity to start:");
                    Console.WriteLine("1. Breathing");
                    Console.WriteLine("2. Reflecting");
                    Console.WriteLine("3. Listing");
                    Console.Write("Select a choice from the menu: ");
                    string activityChoice = Console.ReadLine();

                    Activities activity = null;

                    switch (activityChoice)
                    {
                        case "1":
                            Console.Clear();
                            activity = new Breathing("Breathing", "This activity will help you relax by walking your through breathing in and out slowly. Clear your mind and focus on your breathing.");
                            break;
                        case "2":
                            Console.Clear();
                            activity = new Reflecting("Reflecting", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.");
                            break;
                        case "3":
                            Console.Clear();
                            activity = new Listing("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");
                            break;
                        default:
                            Console.WriteLine("Invalid choice, returning to main menu.");
                            continue;
                    }

                    activity.StartMessage();
                    Console.WriteLine("Preparing...");
                    activity.AnimateTime(5);
                    activity.GetDuration();

                    if (activity is Breathing breathingActivity)
                    {
                        Console.Clear();
                        Console.WriteLine("Get Ready ...");
                        activity.AnimateTime(5);
                        breathingActivity.StartBreathing();
                    }
                    else if (activity is Reflecting reflectingActivity)
                    {
                        Console.Clear();
                        reflectingActivity.StartReflecting();
                    }
                    else if (activity is Listing listingActivity)
                    {
                        Console.Clear();
                        listingActivity.StartListing();
                    }

                    activity.EndMessage();
                    activity.LogActivity();
                    break;

                case "2":
                    Activities.DisplayHistory();
                    Console.WriteLine("\nPress any key to return to the main menu.");
                    Console.ReadKey();
                    break;

                case "3":
                    continueProgram = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}