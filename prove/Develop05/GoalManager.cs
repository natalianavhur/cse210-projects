using System;
class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    public void AddGoal()
    {
        Console.WriteLine("The types of Goal are:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.WriteLine("Which type of goal would you like to create?");

        string choice = Console.ReadLine();
        Goal newGoal = null;

        switch (choice)
        {
            case "1":
                newGoal = CreateSimpleGoal();
                break;
            case "2":
                newGoal = CreateEternalGoal();
                break;
            case "3":
                newGoal = CreateChecklistGoal();
                break;
            default:
                Console.WriteLine("Invalid choice. Returning to menu.");
                return;
        }

        if (newGoal != null)
        {
            _goals.Add(newGoal);
            SerializeGoal(newGoal);
            Console.WriteLine("Goal created and saved successfully.");
        }

    }

    private SimpleGoal CreateSimpleGoal()
    {
        Console.WriteLine("What is the name of your goal? ");
        string name = Console.ReadLine();

        Console.WriteLine("Enter the description of the goal:");
        string description = Console.ReadLine();

        Console.WriteLine("Enter the points for this goal:");
        int points = int.Parse(Console.ReadLine());

        return new SimpleGoal(name, description, points);
    }

    private EternalGoal CreateEternalGoal()
    {
        Console.WriteLine("What is the name of your goal? ");
        string name = Console.ReadLine();

        Console.WriteLine("What is a short description ");
        string description = Console.ReadLine();

        Console.WriteLine("Enter the points for this eternal goal:");
        int points = int.Parse(Console.ReadLine());

        return new EternalGoal(name, description, points);
    }

    private ChecklistGoal CreateChecklistGoal()
    {
        Console.WriteLine("What is the name of your goal? ");
        string name = Console.ReadLine();

        Console.WriteLine("Enter the description of the checklist goal:");
        string description = Console.ReadLine();

        Console.WriteLine("Enter the points for this checklist goal:");
        int points = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter the total number of times to complete the goal:");
        int totalTimes = int.Parse(Console.ReadLine());

        return new ChecklistGoal(name, description, points, totalTimes);
    }

    private void SerializeGoal(Goal goal)
    {
        string jsonString = JsonSerializer.Serialize(goal);
        Console.WriteLine("Serialized Goal:");
        Console.WriteLine(jsonString);

        // Here you can save the JSON string to a file or database
        System.IO.File.AppendAllText("goals.json", jsonString + Environment.NewLine);
    }

    public void DisplayGoals()
    {
        Console.WriteLine("Stub: DisplayGoals called.");
    }

    public int GetScore()
    {
        Console.WriteLine("Stub: GetScore called.");
        return _score;
    }

    public void SaveGoals()
    {
        Console.WriteLine("Stub: SaveGoals called.");
    }

    public void LoadGoals(string filename)
    {
        Console.WriteLine($"Stub: LoadGoals called with filename '{filename}'.");
    }
}