using System.Text.Json;
using System.Text.Json.Serialization;

public class ChecklistGoal : Goal
{
    private int currentCount = 0;

    private int targetCount;
    private int bonusPoints;

    [JsonConstructor]
    public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints, DateTime dueDate)
        : base(name, description, points, dueDate)
    {
        this.targetCount = targetCount;
        this.bonusPoints = bonusPoints;
    }

    public ChecklistGoal() : base("", "", 0, DateTime.MinValue) { }

    public override int RecordEvent()
    {
        if (currentCount < targetCount)
        {
            currentCount++;
            CompletionCount++;
            if (currentCount == targetCount)
            {
                return Points + bonusPoints;
            }
            return Points;
        }
        Console.WriteLine("Goal already completed.");
        return 0;
    }

    public override bool CanComplete()
    {
        return currentCount < targetCount;
    }

    public override string GetStatus()
    {
        return $"{(currentCount >= targetCount ? "[X]" : "[ ]")} Checklist Goal: {Name} - {Description} ({currentCount}/{targetCount} completed, {Points} points each, {bonusPoints} bonus, Completed {CompletionCount} times, Due: {DueDate.ToShortDateString()})";
    }
}
