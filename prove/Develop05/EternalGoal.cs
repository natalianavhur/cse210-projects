
class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points)
    {
    }

    public override void RecordEvent()
    {
        Console.WriteLine("Stub: RecordEvent called for EternalGoal.");
    }

    public override string GetStatus()
    {
        Console.WriteLine("Stub: GetStatus called for EternalGoal.");
        return "[ ]";
    }
}