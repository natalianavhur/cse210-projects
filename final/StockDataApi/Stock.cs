using System;

public class Stock
{
    private string _symbol;
    private string _name;
    private DateTime _timestamp;
    private double _open;
    private double _high;
    private double _low;
    private double _close;
    private int _volume;

    public Stock(string symbol, string name)
    {
        _symbol = symbol;
        _name = name;
    }
    public string Symbol
    {
        get => _symbol;
        private set => _symbol = value;
    }

    public string Name
    {
        get => _name;
        private set => _name = value;
    }

    public DateTime Timestamp
    {
        get => _timestamp;
        set => _timestamp = value;
    }

    public double Open
    {
        get => _open;
        set
        {
            if (value >= 0)
                _open = value;
            else
                throw new ArgumentException("Open price cannot be negative.");
        }
    }

    public double High
    {
        get => _high;
        set
        {
            if (value >= 0)
                _high = value;
            else
                throw new ArgumentException("High price cannot be negative.");
        }
    }

    public double Low
    {
        get => _low;
        set
        {
            if (value >= 0)
                _low = value;
            else
                throw new ArgumentException("Low price cannot be negative.");
        }
    }

    public double Close
    {
        get => _close;
        set
        {
            if (value >= 0)
                _close = value;
            else
                throw new ArgumentException("Close price cannot be negative.");
        }
    }

    public int Volume
    {
        get => _volume;
        set
        {
            if (value >= 0)
                _volume = value;
            else
                throw new ArgumentException("Volume cannot be negative.");
        }
    }
}
