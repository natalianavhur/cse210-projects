using System;
using System.Text.Json;

public abstract class Goal
{
    protected string _name;
    protected string _description;
    protected int _points;
    protected bool _isCompleted;

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public string Description
    {
        get => _description;
        set => _description = value;
    }

    public int Points
    {
        get => _points;
        set => _points = value;
    }

    public bool IsCompleted
    {
        get => _isCompleted;
        set => _isCompleted = value;
    }

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public abstract void RecordEvent();
    public abstract string GetStatus();

    public virtual void Serialize()
    {

    }

    public void Display()
    {
        Console.WriteLine($"Stub: Display called for goal {_name}.");
    }
}