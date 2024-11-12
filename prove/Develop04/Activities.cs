class Activities
{
    protected string _name;
    protected string _description;
    protected int _duration;
    protected DateTime _time;

    public Activities(string name, string description)
    {
        _name = name;
        _description = description;
        _time = DateTime.Now;
    }

    public void StartMessage()
    {
        Console.WriteLine($"Welcome to the {_name} Activity.");
        Console.WriteLine($"{_description}");
    }

    public void EndMessage()
    {
        Console.WriteLine("Well Done");
        Console.WriteLine($"You have completed {_duration} seconds of the {_name} Activity");
        Thread.Sleep(5000);
    }
    public void GetDuration()
    {
        Console.Write("Enter the duration for the activity in seconds: ");
        _duration = int.Parse(Console.ReadLine());
    }
    public void AnimateTime(int seconds)
    {
        char[] animationFrames = { '|', '/', '-', '\\' };
        int frameIndex = 0;
        DateTime endTime = DateTime.Now.AddSeconds(seconds);

        while (DateTime.Now < endTime)
        {
            Console.Write($"\r{animationFrames[frameIndex]}");
            frameIndex = (frameIndex + 1) % animationFrames.Length;
            Thread.Sleep(250);
        }
        Console.WriteLine("\r ");

    }
    public void LogActivity()
    {
        string filePath = "activity_log.txt";
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine($"Activity: {_name}");
            writer.WriteLine($"Description: {_description}");
            writer.WriteLine($"Duration: {_duration} seconds");
            writer.WriteLine($"Time: {_time}");
            writer.WriteLine(new string('-', 50));
        }
    }
    public static void DisplayHistory()
    {
        string filePath = "activity_log.txt";
        if (File.Exists(filePath))
        {
            Console.WriteLine("Activity History:");
            Console.WriteLine(new string('-', 50));
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
        else
        {
            Console.WriteLine("No activity history found.");
        }
    }

}