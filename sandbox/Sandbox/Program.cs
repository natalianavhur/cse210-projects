using System;

public class Program
{
    public static void Main(string[] args)
    {
        // Create smart devices
        var light1 = new SmartLight("Living Room Light");
        var heater1 = new SmartHeater("Bedroom Heater");
        var tv1 = new SmartTV("Living Room TV");

        // Create rooms and add devices
        var livingRoom = new Room("Living Room");
        livingRoom.AddDevice(light1);
        livingRoom.AddDevice(tv1);

        var bedroom = new Room("Bedroom");
        bedroom.AddDevice(heater1);

        // Create house and add rooms
        var house = new House();
        house.AddRoom(livingRoom);
        house.AddRoom(bedroom);

        // Turn on some devices
        livingRoom.TurnOnDevice("Living Room Light");
        bedroom.TurnOnAllDevices();

        // Wait for a while
        System.Threading.Thread.Sleep(2000);

        // Report status of all items
        Console.WriteLine("All Items:");
        Console.WriteLine(house.ReportAllItems());

        // Report items that are on
        Console.WriteLine("\nItems On:");
        Console.WriteLine(house.ReportItemsOn());

        // Report item that has been on the longest
        Console.WriteLine("\nItem On Longest:");
        Console.WriteLine(house.ReportItemOnLongest());

        // Turn off all devices in the living room
        livingRoom.TurnOffAllDevices();

        // Report status again
        Console.WriteLine("\nAll Items After Turning Off Living Room Devices:");
        Console.WriteLine(house.ReportAllItems());
    }
}
