
public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points, DateTime dueDate)
        : base(name, description, points, dueDate) { }

    public override int RecordEvent()
    {
        CompletionCount++;
        return Points;
    }

    public override bool CanComplete()
    {
        return true;
    }

    public override string GetStatus()
    {
        return $"[ ] Eternal Goal: {Name} - {Description} ({Points} points each time, Completed {CompletionCount} times, Due: {DueDate.ToShortDateString()})";
    }
}