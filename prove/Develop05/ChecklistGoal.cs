using System.Text.Json;
using System.Text.Json.Serialization;

public class ChecklistGoal : Goal
{
    private int _currentCount = 0;

    private int _targetCount;
    private int _bonusPoints;

    [JsonConstructor]
    public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints, DateTime dueDate)
        : base(name, description, points, dueDate)
    {
        this._targetCount = targetCount;
        this._bonusPoints = bonusPoints;
    }

    public ChecklistGoal() : base("", "", 0, DateTime.MinValue) { }

    public override int RecordEvent()
    {
        if (_currentCount < _targetCount)
        {
            _currentCount++;
            CompletionCount++;
            if (_currentCount == _targetCount)
            {
                return Points + _bonusPoints;
            }
            return Points;
        }
        Console.WriteLine("Goal already completed.");
        return 0;
    }

    public override bool CanComplete()
    {
        return _currentCount < _targetCount;
    }

    public override string GetStatus()
    {
        return $"{(_currentCount >= _targetCount ? "[X]" : "[ ]")} {Name} - ({Description}) ({_currentCount}/{_targetCount} completed, {Points} points each, {_bonusPoints} bonus, Due: {DueDate.ToShortDateString()})";
    }
}
