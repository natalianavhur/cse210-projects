using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

class GoalManager
{
    private List<Goal> goals = new List<Goal>();
    private int score = 0;

    public void AddGoal()
    {
        Console.WriteLine("\nThe types of goals are:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Which type of goal would you like to create? ");

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
            goals.Add(newGoal);
            Console.WriteLine("Goal created successfully.");
        }
    }

    private SimpleGoal CreateSimpleGoal()
    {
        Console.Write("Enter the name of the goal: ");
        string name = Console.ReadLine();

        Console.Write("Enter a short description: ");
        string description = Console.ReadLine();

        Console.Write("Enter the points awarded upon completion: ");
        int points = int.Parse(Console.ReadLine());

        Console.Write("Enter the due date (yyyy-mm-dd): ");
        DateTime dueDate = DateTime.Parse(Console.ReadLine());

        return new SimpleGoal(name, description, points, dueDate);
    }

    private EternalGoal CreateEternalGoal()
    {
        Console.Write("Enter the name of the goal: ");
        string name = Console.ReadLine();

        Console.Write("Enter a short description: ");
        string description = Console.ReadLine();

        Console.Write("Enter the points awarded each time it is recorded: ");
        int points = int.Parse(Console.ReadLine());

        Console.Write("Enter the due date (yyyy-mm-dd): ");
        DateTime dueDate = DateTime.Parse(Console.ReadLine());

        return new EternalGoal(name, description, points, dueDate);
    }

    private ChecklistGoal CreateChecklistGoal()
    {
        Console.Write("Enter the name of the goal: ");
        string name = Console.ReadLine();

        Console.Write("Enter a short description: ");
        string description = Console.ReadLine();

        Console.Write("Enter the points awarded each time it is recorded: ");
        int points = int.Parse(Console.ReadLine());

        Console.Write("Enter the number of times to complete the goal: ");
        int targetCount = int.Parse(Console.ReadLine());

        Console.Write("Enter the bonus points upon completing the goal: ");
        int bonusPoints = int.Parse(Console.ReadLine());

        Console.Write("Enter the due date (yyyy-mm-dd): ");
        DateTime dueDate = DateTime.Parse(Console.ReadLine());

        return new ChecklistGoal(name, description, points, targetCount, bonusPoints, dueDate);
    }

    public void DisplayGoals()
    {
        Console.WriteLine("\nYour Goals:");
        foreach (var goal in goals)
        {
            Console.WriteLine(goal.GetStatus());
        }
        Console.WriteLine($"Current Score: {score}");
    }

    public void RecordEvent()
    {
        var completableGoals = goals.Where(g => g.CanComplete()).ToList();

        if (completableGoals.Count == 0)
        {
            Console.WriteLine("No goals available to complete."); return;
        }

        Console.WriteLine("\nSelect a goal to record:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].Name}");
        }

        int choice = int.Parse(Console.ReadLine()) - 1;

        if (choice >= 0 && choice < goals.Count)
        {
            score += goals[choice].RecordEvent();
            Console.WriteLine("Event recorded successfully.");
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
    }

    public void SaveGoals()
    {
        var options = new JsonSerializerOptions
        {
            Converters = { new GoalConverter() },
            WriteIndented = true
        };

        string jsonString = JsonSerializer.Serialize(goals, options);
        Console.Write("What is the name of the file: ");
        string filename = Console.ReadLine().Trim();
        File.WriteAllText(filename, jsonString);
        Console.WriteLine("Goals saved successfully.");
    }

    public void LoadGoals(string filename)
    {
        if (File.Exists(filename))
        {
            var options = new JsonSerializerOptions
            {
                Converters = { new GoalConverter() }
            };

            string jsonFromFile = File.ReadAllText(filename);
            goals = JsonSerializer.Deserialize<List<Goal>>(jsonFromFile, options);
            Console.WriteLine("Goals loaded successfully.");
        }
        else
        {
            Console.WriteLine($"File '{filename}' does not exist.");
        }
    }

    private void CalculateScore()
    {
        score = goals.Sum(goal => goal.Points * goal.CompletionCount);
    }

    public void DisplayReminders(int daysBeforeDue)
    {
        Console.WriteLine("\nReminders for Goals Due Soon:");
        foreach (var goal in goals.Where(g => g.IsDueSoon(daysBeforeDue)))
        {
            Console.WriteLine(goal.GetStatus());
        }
    }


    private class GoalConverter : JsonConverter<Goal>
    {
        public override Goal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (var jsonDoc = JsonDocument.ParseValue(ref reader))
            {
                var root = jsonDoc.RootElement;

                if (!root.TryGetProperty("Type", out var typeProperty))
                {
                    throw new JsonException("The 'Type' property is missing in the JSON data.");
                }

                var type = typeProperty.GetString();
                return type switch
                {
                    "SimpleGoal" => JsonSerializer.Deserialize<SimpleGoal>(root.GetRawText(), options),
                    "EternalGoal" => JsonSerializer.Deserialize<EternalGoal>(root.GetRawText(), options),
                    "ChecklistGoal" => JsonSerializer.Deserialize<ChecklistGoal>(root.GetRawText(), options),
                    _ => throw new NotSupportedException($"Goal type '{type}' is not supported."),
                };
            }
        }


        public override void Write(Utf8JsonWriter writer, Goal value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("Type", value.GetType().Name);

            var json = JsonSerializer.Serialize(value, value.GetType(), options);
            using (var jsonDoc = JsonDocument.Parse(json))
            {
                foreach (var property in jsonDoc.RootElement.EnumerateObject())
                {
                    property.WriteTo(writer);
                }
            }
            writer.WriteEndObject();
        }
    }
}