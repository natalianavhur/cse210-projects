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
class Reflecting : Activities
{
    protected List<string> prompts = new List<string>(){
        "Think of a time when you overcame a significant obstacle.",
        "Think of a time when you learned something important about yourself.",
        "Think of a time when you had to make a difficult decision.",
        "Think of a time when you were really proud of yourself.",
        "Think of a time when you felt really disappointed in yourself.",
        "Think of a time when you had to apologize to someone.",
        "Think of a time when someone apologized to you.",
        "Think of a time when you felt really grateful for something.",
        "Think of a time when you felt really angry.",
        "Think of a time when you felt really scared.",
        "Think of a time when you felt really happy.",
        "Think of a time when you felt really sad.",
        "Think of a time when you felt really loved.",
        "Think of a time when you felt really lonely.",
        "Think of a time when you felt really misunderstood.",
        "Think of a time when you felt really understood.",
        "Think of a time when you felt really inspired.",
        "Think of a time when you felt really bored.",
        "Think of a time when you felt really excited.",
        "Think of a time when you felt really calm.",
    };
    protected List<string> questions = new List<string>(){
        "What was the most challenging aspect of this experience?",
        "What was the most rewarding aspect of this experience?",
        "What surprised you most about this experience?",
        "What did you learn about others through this experience?",
        "How did this experience change your perspective on [specific topic]?",
        "What would you do differently if you could do it again?",
        "What advice would you give to someone who wants to have a similar experience?",
        "How did this experience make you feel about your own abilities?",
        "What did you learn about the process of [specific skill or task]?",
        "How did this experience impact your relationships with others?",
        "What did you learn about the importance of [specific value or quality]?",
        "How did this experience help you grow as a person?",
        "What did you learn about the power of [specific action or behavior]?",
        "How did this experience change your understanding of [specific concept]?",
        "What did you learn about the importance of perseverance?",
        "How did this experience help you develop your problem-solving skills?",
        "What did you learn about the importance of seeking help when needed?",
        "How did this experience help you build your confidence?",
        "What did you learn about the importance of taking risks?",
        "How did this experience help you develop your creativity?",
        "What did you learn about the importance of setting goals?",
        "How did this experience help you develop your time management skills?",
        "What did you learn about the importance of self-care?",
        "How did this experience help you develop your communication skills?",
        "What did you learn about the importance of empathy?",
        "How did this experience help you develop your leadership skills?",
        "What did you learn about the importance of teamwork?",
        "How did this experience help you develop your critical thinking skills?",
        "What did you learn about the importance of lifelong learning?",
        "How can you apply the lessons learned from this experience to future challenges?"
    };
    protected Random random = new Random();
    protected Stopwatch stopwatch = new Stopwatch();


    public Reflecting(string name, string description)
        : base(name, description)
    {
    }
    public string GetRandomPrompt()
    {
        int promptIndex = random.Next(0, prompts.Count);
        return prompts[promptIndex];
    }

    public string GetRandomQuestion(List<string> usedQuestions)
    {
        string question;
        do
        {
            int index = random.Next(questions.Count);
            question = questions[index];
        }
        while (usedQuestions.Contains(question));
        return question;
    }

    public void StartReflecting()
    {
        Console.WriteLine("Get Ready ...");
        base.AnimateTime(5);
        Console.WriteLine($"--- {GetRandomPrompt()} ---");
        Console.WriteLine("When you have something in mind, press enter to continue.");
        Console.ReadLine();

        Console.WriteLine("Now ponder on each of the following questions as they related to this experience.");
        Console.WriteLine("You may begin in: ");
        base.AnimateTime(5);

        Console.Clear();
        stopwatch.Start();
        List<string> usedQuestions = new List<string>();

        while (stopwatch.Elapsed.TotalSeconds < _duration)
        {
            string question = GetRandomQuestion(usedQuestions);
            usedQuestions.Add(question);

            Console.WriteLine(question);
            base.AnimateTime(3);
        }
        stopwatch.Stop();
        Thread.Sleep(5000);
    }

}