using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Threading;


class Program
{
    static void Main()
    {

        int choice = int.Parse(Console.ReadLine());
        if (choice == 1)
        {

        }
        else if (choice == 2)
        {

        }
        else if (choice == 3)
        {

        }
        else if (choice == 4)
        {
            Console.WriteLine("Quitting Program.");

        }
        else
        {

        }

    }

    public void DisplayMenu()
    {
        Console.WriteLine("Menu Options:");
        Console.WriteLine("1. Start breathing activity");
        Console.WriteLine("2. Start reflecting activity");
        Console.WriteLine("3. Start listing activities");
        Console.WriteLine("4. Quit");
        Console.Write("Select a choice from the menu:");
    }

}

class Activities
{
    protected string _name;
    protected string _description;
    protected int _duration;

    protected int _time;

    public Activities(string name, string description, int duration, int time)
    {
        _name = name;
        _description = description;
        _duration = duration;
        _time = time;
    }

    public void StartMessage()
    {
        Console.WriteLine($" Welcome to the {_name}.");
        Console.WriteLine($"{_description}");
    }

    public void EndMessage()
    {

    }
    public void AnimateTime(int seconds)
    {

    }

}

class Breathing : Activities
{
    private int _inhaleDuration;
    private int _exhaleDuration;
    private string _animationType;

    DateTime startTime = DateTime.Now;
    Stopwatch stopwatch = Stopwatch.StartNew();

    public Breathing(string name, string description, int duration, int time) : base(name, description, duration, time)
    {
        _name = name;
        _duration = duration;
        _description = description;
        _time = time;
        // _inhaleDuration = duration / 2;
        // _exhaleDuration = duration / 2;
        // _animationType = 
    }
    public void DisplayMessage()
    {
        while (stopwatch.Elapsed.TotalSeconds < _duration)
        {
            int remainingTime = _duration - (int)stopwatch.Elapsed.TotalSeconds;
            _inhaleDuration = 5;
            _exhaleDuration = 5;
            if (remainingTime == 5)
            {
                Console.WriteLine($"Inhale:");
            }
            else if (remainingTime % 10 == 0)
            {
                Console.WriteLine($"Exhale:");
            }

        }
        stopwatch.Stop();
        Console.WriteLine("Time is up. The bretahing task has finished.");

    }


}

class Listing : Activities
{
    private List<string> prompts = new List<string>();
    private Random random = new Random();

    public Listing(string name, string description, int duration, int time) : base(name, description, duration, time)
    {
        _name = name;
        _duration = duration;
        _description = description;
        _time = time;
    }
    public string GetRandomPrompt()
    {
        int promptIndex = random.Next(0, prompts.Count);
        return prompts[promptIndex];

    }
    public void DisplayRandomPrompt()
    {


    }

}

class Reflecting : Activities
{
    private List<string> prompts = new List<string>();
    private List<string> questions = new List<string>();
    private Random random = new Random();

    public Reflecting(string name, string description, int duration, int time) : base(name, description, duration, time)
    {
        _name = name;
        _duration = duration;
        _description = description;
        _time = time;
    }
    public string GetPrompt()
    {
        int promptIndex = random.Next(0, prompts.Count);
        return prompts[promptIndex];

    }
    public void DisplayPrompt()
    {

    }

    public string GetQuestion()
    {
        int questionIndex = random.Next(0, questions.Count);
        return questions[questionIndex];
    }

    public void DisplayQuestion()
    {

    }

}