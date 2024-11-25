public class SimpleGoal : Goal
{
    private bool _isCompleted = false;

    public SimpleGoal(string name, string description, int points, DateTime dueDate)
        : base(name, description, points, dueDate) { }

    public override int RecordEvent()
    {
        if (!_isCompleted)
        {
            _isCompleted = true;
            CompletionCount++;
            return Points;
        }
        Console.WriteLine("Goal already completed.");
        return 0;
    }

    public override bool CanComplete()
    {
        return !_isCompleted;
    }

    public override string GetStatus()
    {
        return $"{(_isCompleted ? "[X]" : "[ ]")} Simple Goal: {Name} - {Description} ({Points} points, Completed {CompletionCount} times, Due: {DueDate.ToShortDateString()})";
    }
}