class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _currentCount;
    private int _bonusPoints;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints)
        : base(name, description, points)
    {
        _targetCount = targetCount;
        _bonusPoints = bonusPoints;
        _currentCount = 0;
    }

    public override void RecordEvent()
    {
        Console.WriteLine("Stub: RecordEvent called for ChecklistGoal.");
    }

    public override string GetStatus()
    {
        Console.WriteLine("Stub: GetStatus called for ChecklistGoal.");
        return $"{_currentCount}/{_targetCount}";
    }

    public override void Serialize()
    {
        // base.Serialize();
    }
}
