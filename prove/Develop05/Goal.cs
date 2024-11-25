using System;

public abstract class Goal
{
    private string name;
    private string description;
    private int points;
    private int completionCount;
    private DateTime dueDate;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public int Points
    {
        get { return points; }
        set { points = value; }
    }

    public int CompletionCount
    {
        get { return completionCount; }
        set { completionCount = value; }
    }

    public DateTime DueDate
    {
        get { return dueDate; }
        set { dueDate = value; }
    }

    public Goal(string name, string description, int points, DateTime dueDate)
    {
        this.name = name;
        this.description = description;
        this.points = points;
        completionCount = 0;
        this.dueDate = dueDate;
    }

    public abstract int RecordEvent();
    public abstract bool CanComplete();
    public abstract string GetStatus();

    public bool IsDueSoon(int daysBeforeDue)
    {
        return (dueDate - DateTime.Now).TotalDays <= daysBeforeDue;
    }
}