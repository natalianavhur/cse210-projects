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

class Listing : Activities
{
    protected List<string> prompts = new List<string>(){
        "What are your favorite hobbies and interests?",
        "What are your long-term goals and aspirations?",
        "What are your biggest fears and insecurities?",
        "What are you most grateful for in your life?",
        "What is your favorite memory from childhood?",
        "What is your favorite thing about yourself?",
        "What is something you’ve always wanted to try but haven’t yet?",
        "What is your biggest regret?",
        "What is your favorite quote or saying?",
        "What is your favorite book or movie?",
        "What is your favorite way to spend a free day?",
        "What is your favorite season?",
        "What is your favorite place to be?",
        "What is your favorite thing to eat?",
        "What is your favorite thing to do with friends and family?",
        "What is your favorite thing about your job or school?",
        "What is your favorite thing about living in your community?",
        "What is your favorite thing about yourself physically?",
        "What is your favorite thing about yourself mentally?",
        "What is your favorite thing about yourself spiritually?"
};
    protected List<string> responses = new();
    protected Random random = new Random();
    protected Stopwatch stopwatch = new();

    public Listing(string name, string description)
       : base(name, description)
    {
    }
    public string GetRandomPrompt()
    {
        int promptIndex = random.Next(0, prompts.Count);
        return prompts[promptIndex];

    }
    public void DisplayResponses()
    {
        Console.WriteLine("Here are your responses:");
        foreach (var response in responses)
        {
            Console.WriteLine($" - {response}");
        }
    }
    public void StartListing()
    {
        Console.WriteLine("List as many responses you can to the following prompt:");
        string prompt = GetRandomPrompt();
        Console.WriteLine($"--- {prompt} ---");

        Console.WriteLine("You may begin in: ");
        base.AnimateTime(3);

        stopwatch.Start();

        while (stopwatch.Elapsed.TotalSeconds < _duration)
        {
            Console.Write(">");
            string response = Console.ReadLine();

            if (stopwatch.Elapsed.TotalSeconds >= _duration) break;

            if (!string.IsNullOrWhiteSpace(response))
            {
                responses.Add(response);
            }
        }
        stopwatch.Stop();

        Console.WriteLine($"\nYou entered {responses.Count} items.");
        Thread.Sleep(5000);
    }
}