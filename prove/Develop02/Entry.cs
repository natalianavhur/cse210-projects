using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
public class Entry
{
    public string date;
    public string time;
    public List<(string prompt, string response)> promptResponses = new List<(string, string)>();
    public string title;

    public string reflection;

    public void DisplayEntry()
    {
        Console.WriteLine($"Date: {date} - Time: {time}");

        if (!string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine($"Title: {title}");
        }
        foreach (var (prompt, response) in promptResponses)
        {
            Console.WriteLine($"Prompt: {prompt}");
            Console.WriteLine($"Response: {response}");

        }
        if (!string.IsNullOrWhiteSpace(reflection))
        {
            Console.WriteLine($"Reflection: {reflection}");
        }

        Console.WriteLine();
    }

    public void DisplayMenuEntries()
    {
        Console.WriteLine("Select the attributes you want to add to the journal entry:");
        Console.WriteLine("1. Title");
        Console.WriteLine("2. Prompt");
        Console.WriteLine("3. Reflection");
        Console.WriteLine("4. Quit");
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
            "What is the current problem or challenge I am facing?",
            "What are my goals and objectives related to this problem or challenge?",
            "What are some potential solutions to this problem or challenge?",
            "What are some creative and unconventional solutions I can consider?",
            "What are some pros and cons of each potential solution?",
            "How can I collaborate with others to find a solution?",
            "What are some resources I can utilize to help solve this problem or challenge?",
            "How can I apply my skills, knowledge, and experience to this problem or challenge?",
            "What are some potential roadblocks or challenges to implementing a solution, and how can I overcome them?",
            "How can I prioritize and organize my thoughts and ideas to effectively solve this problem or challenge?",
            "What do I love most about my body, and why?",
            "What beliefs or messages about my body do I need to let go of in order to cultivate more self - love and acceptance?",
            "What activities or practices help me feel connected to and in tune with my body?",
            "How can I be more compassionate towards my body, especially when I’m feeling self - critical or negative?",
            "What role does social media or the media in general play in shaping my body image, and how can I cultivate a more positive relationship with these sources of influence?",
            "What would it feel like to let go of the need to compare my body to others, and instead focus on my own unique strengths and beauty?",
            "What are some ways I can prioritize my physical health and well - being, without falling into the trap of diet culture or body shaming?",
            "How can I shift my focus from appearance - based goals(e.g.weight loss, achieving a certain body shape) to more holistic measures of health and wellness(e.g.energy levels, mood, strength, etc.)?",
            "What does it mean to truly embody self-love and body positivity, and how can I take small steps towards this every day?",
            "How can I cultivate a sense of appreciation and love for my body, even if it doesn’t conform to societal ideals?",
            "What are some ways I can celebrate and care for my body, regardless of its shape or size ?",
            "What does creativity mean to me ?",
            "How do I get to use my creativity on a daily basis?",
            "What is one thing that I have always wanted to create, and what steps can I take to make it a reality?",
            "What is one place or environment that inspires my creativity, and how can I create more opportunities to be in that space?",
            "What are my passions and interests, and how can I incorporate them into my work or personal life?",
            "What is one small creative project that I can do today, and how can I make it unique to my personal style?",
            "What is one fear or obstacle that is holding me back creatively, and what can I do to overcome it ?",
            "What is one thing that I can learn or experiment with in order to expand my creative skills and knowledge?",
            "What is one challenge or prompt that I can give myself to push myself creatively?",
            "What is one way I can creatively express gratitude, love, or appreciation for someone in my life ?",
            "How can I challenge myself to think outside of the box and embrace new and creative ideas ?",
            "How can I surround myself with people and environments that foster creativity and inspiration ?",
            "What are some ways I can take time for myself and recharge my batteries to cultivate creativity and inspiration ?",
            "What are some hobbies or activities I can pursue to tap into my creativity and imagination ?",
            "How can I incorporate more play and fun into my life to foster creativity and inspiration ?",
            "What are some ways I can break out of my comfort zone and try new things to stimulate creativity and inspiration?",
            "How can I be more open - minded and receptive to new ideas and perspectives?",
            "What are some ways I can use technology and innovation to enhance my creativity and inspiration?",
            "How can I seek out new experiences and adventures to expand my horizons and inspire my creativity?",
            "How can I create a supportive and nurturing environment for my mind, body, and soul to encourage creativity and inspiration ?"
        };

        Random random = new Random();
        int index = random.Next(prompts.Count);

        string random_prompt = prompts[index];
        return random_prompt;
    }
    public string GetResponse()
    {
        string userResponse = Console.ReadLine();
        return userResponse;
    }
    public void DisplayPrompt(string randomPrompt)
    {
        Console.WriteLine(randomPrompt);
        Console.WriteLine();
    }
    public string GetDate()
    {
        DateTime date = DateTime.Today;
        string formatDate = date.ToString("dd/MM/yyyy");
        return formatDate;
    }
    public string GetTitle()
    {
        Console.WriteLine("Enter a title for this journal entry: ");
        string title = Console.ReadLine();
        return title;
    }
    public string GetTime()
    {
        DateTime time = DateTime.Now;
        string time_str = time.ToString("dd/MM/yyyy");
        return time_str;
    }
    public string GetReflection()
    {
        Console.WriteLine("Enter a reflection for tis journal entry: ");
        string reflection = Console.ReadLine();
        return reflection;
    }


}