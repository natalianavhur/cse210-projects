using System;
using System.Diagnostics;

public abstract class SmartDevice
{
    private bool _isOn;
    private DateTime _lastTurnedOnTime;
    protected Stopwatch _stopwatch;

    public string Name { get; protected set; }

    public SmartDevice(string name)
    {
        Name = name;
        _stopwatch = new Stopwatch();
    }

    public void TurnOn()
    {
        if (!_isOn)
        {
            _isOn = true;
            _lastTurnedOnTime = DateTime.Now;
            _stopwatch.Start();
        }
    }

    public void TurnOff()
    {
        if (_isOn)
        {
            _isOn = false;
            _stopwatch.Stop();
        }
    }

    public bool IsOn()
    {
        return _isOn;
    }

    public TimeSpan GetOnDuration()
    {
        return _stopwatch.Elapsed;
    }

    public override string ToString()
    {
        return $"{Name} is {(IsOn() ? "On" : "Off")}, On Duration: {GetOnDuration()}";
    }



}