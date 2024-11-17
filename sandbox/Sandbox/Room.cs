using System.Collections.Generic;
using System.Linq;

public class Room
{
    public string Name { get; private set; }
    private List<SmartDevice> _devices;

    public Room(string name)
    {
        Name = name;
        _devices = new List<SmartDevice>();
    }

    public void AddDevice(SmartDevice device)
    {
        _devices.Add(device);
    }

    public void TurnOnAllLights()
    {
        foreach (var device in _devices.OfType<SmartLight>())
        {
            device.TurnOn();
        }
    }

    public void TurnOffAllLights()
    {
        foreach (var device in _devices.OfType<SmartLight>())
        {
            device.TurnOff();
        }
    }

    public void TurnOnDevice(string deviceName)
    {
        var device = _devices.FirstOrDefault(d => d.Name == deviceName);
        device?.TurnOn();
    }

    public void TurnOffDevice(string deviceName)
    {
        var device = _devices.FirstOrDefault(d => d.Name == deviceName);
        device?.TurnOff();
    }

    public void TurnOnAllDevices()
    {
        foreach (var device in _devices)
        {
            device.TurnOn();
        }
    }

    public void TurnOffAllDevices()
    {
        foreach (var device in _devices)
        {
            device.TurnOff();
        }
    }

    public string ReportAllItems()
    {
        return string.Join("\n", _devices.Select(d => d.ToString()));
    }

    public string ReportItemsOn()
    {
        return string.Join("\n", _devices.Where(d => d.IsOn()).Select(d => d.ToString()));
    }

    public string ReportItemOnLongest()
    {
        var longestOnDevice = _devices.OrderByDescending(d => d.GetOnDuration()).FirstOrDefault();
        return longestOnDevice?.ToString() ?? "No devices are on.";
    }

    public List<SmartDevice> GetAllDevices()
    {
        return _devices;
    }
}
