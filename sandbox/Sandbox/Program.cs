using System;

public class Program
{
    public static void Main(string[] args)
    {
        var light1 = new SmartLight("Living Room Light");
        var heater1 = new SmartHeater("Bedroom Heater");
        var tv1 = new SmartTV("Living Room TV");

        var livingRoom = new Room("Living Room");
        livingRoom.AddDevice(light1);
        livingRoom.AddDevice(tv1);

        var bedroom = new Room("Bedroom");
        bedroom.AddDevice(heater1);

        var house = new House();
        house.AddRoom(livingRoom);
        house.AddRoom(bedroom);

        livingRoom.TurnOnDevice("Living Room Light");
        bedroom.TurnOnAllDevices();

        System.Threading.Thread.Sleep(2000);

        Console.WriteLine("All Items:");
        Console.WriteLine(house.ReportAllItems());

        Console.WriteLine("\nItems On:");
        Console.WriteLine(house.ReportItemsOn());

        Console.WriteLine("\nItem On Longest:");
        Console.WriteLine(house.ReportItemOnLongest());

        livingRoom.TurnOffAllDevices();

        Console.WriteLine("\nAll Items After Turning Off Living Room Devices:");
        Console.WriteLine(house.ReportAllItems());
    }
}



