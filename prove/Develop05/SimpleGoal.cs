using System;
using System.Text.Json;

class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points) : base(name, description, points)
    {
    }

    public override void RecordEvent()
    {
        Name = Console;
        Description = "mmm";
        base.Points = 20;
        base.IsCompleted = false;
    }

    public override string GetStatus()
    {
        Console.WriteLine("Stub: GetStatus called for SimpleGoal.");
        return $"SimpleGoal: {Name}, Points: {Points}, Completed: {IsCompleted}";
        // return "[ ]";
    }

    public override void Serialize()
    {

        // var goal = new SimpleGoal("mmm", "mmm", 20) { IsCompleted = false };

        string jsonString = JsonSerializer.Serialize(this);
        Console.WriteLine("serialization");
        Console.WriteLine(jsonString);
    }
}
