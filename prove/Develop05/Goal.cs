using System;

public abstract class Goal
{
    private string _name;
    private string _description;
    private int _points;
    private int _completionCount;
    private DateTime _dueDate;

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public int Points
    {
        get { return _points; }
        set { _points = value; }
    }

    public int CompletionCount
    {
        get { return _completionCount; }
        set { _completionCount = value; }
    }

    public DateTime DueDate
    {
        get { return _dueDate; }
        set { _dueDate = value; }
    }

    public Goal(string name, string description, int points, DateTime dueDate)
    {
        this._name = name;
        this._description = description;
        this._points = points;
        _completionCount = 0;
        this._dueDate = dueDate;
    }

    public abstract int RecordEvent();
    public abstract bool CanComplete();
    public abstract string GetStatus();

    public bool IsDueSoon(int daysBeforeDue)
    {
        return (_dueDate - DateTime.Now).TotalDays <= daysBeforeDue;
    }
}