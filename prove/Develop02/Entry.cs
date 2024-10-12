using System;
using System.Collections.Generic;


public class Entry
{
    public string date;
    public string prompt;
    public string response;
    //

    // TITLE, TIME, TAG, REFLECTION variables
    // public string title;
    //public string time;
    //public string tag;
    //public string reflection;

    public void DisplayEntry()
    {
        Console.WriteLine($"Date: {date} - Prompt: {prompt}");
        Console.WriteLine($"{response}\n");
    }
    public string GeneratePrompt()
    {
        List<string> prompts = new List<string>
        {
            "What happened today?",
            "What was the best thing that happened today?",
            "What was the worst thing that happened today?",
            "What was the most interesting thing I saw or heard today?",
            "What was the most challenging thing I faced today?",
            "What am I grateful for today?",
            "What did I learn today",
            "What was the most fun thing I did today?",
            "What was the most surprising thing that happened today?",
            "What did I do today that I am proud of?",
        };

        Random random = new Random();
        int index = random.Next(prompts.Count);

        string random_prompt = prompts[index];
        return random_prompt;
    }
    public void DisplayPrompt(string randomPrompt)
    {
        Console.WriteLine(randomPrompt);
    }
    public string GetResponse()
    {
        string userResponse = Console.ReadLine();
        return userResponse;
    }
    public string GetDate()
    {
        DateTime date = DateTime.Today;
        string formatDate = date.ToString("dd/MM/yyyy");
        return formatDate;
    }

}