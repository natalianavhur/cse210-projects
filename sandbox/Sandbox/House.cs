using System.Collections.Generic;

public class House
{
    private List<Room> _rooms;

    public House()
    {
        _rooms = new List<Room>();
    }

    public void AddRoom(Room room)
    {
        _rooms.Add(room);
    }

    public Room GetRoom(string roomName)
    {
        return _rooms.FirstOrDefault(r => r.Name == roomName);
    }

    public string ReportAllItems()
    {
        return string.Join("\n\n", _rooms.Select(r => $"{r.Name}:\n{r.ReportAllItems()}"));
    }

    public string ReportItemsOn()
    {
        return string.Join("\n\n", _rooms.Select(r => $"{r.Name}:\n{r.ReportItemsOn()}"));
    }

    public string ReportItemOnLongest()
    {
        var allDevices = _rooms.SelectMany(r => r.GetAllDevices());
        var longestOnDevice = allDevices.OrderByDescending(d => d.GetOnDuration()).FirstOrDefault();
        return longestOnDevice?.ToString() ?? "No devices are on.";
    }
}
