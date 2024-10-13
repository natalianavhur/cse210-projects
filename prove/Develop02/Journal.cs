public class Journal
{
    public List<Entry> entries = new List<Entry>();
    CsvUtility csvUtility = new CsvUtility();
    public void AddEntry()
    {
        Entry newEntry = new Entry();

        newEntry.date = newEntry.GetDate();
        newEntry.time = newEntry.GetTime();

        entries.Add(newEntry);
        entries.Add(newEntry);

        bool quit = false;
        while (!quit)
        {
            newEntry.DisplayMenuEntries();
            int option = int.Parse(Console.ReadLine());
            switch (option)
            {
                case 1:
                    newEntry.title = newEntry.GetTitle();
                    break;
                case 2:
                    newEntry.prompt = newEntry.GeneratePrompt();
                    newEntry.DisplayPrompt(newEntry.prompt);
                    newEntry.response = newEntry.GetResponse();
                    break;
                case 3:
                    newEntry.reflection = newEntry.GetReflection();
                    break;
                case 4:
                    quit = true;
                    break;
                default:
                    Console.WriteLine("The choice types is invalid. Please choose an option from (1-4)");
                    break;
            }
        }
        entries.Add(newEntry);
    }
    public void DisplayJournalEntries()
    {
        foreach (var entry in entries)
        {
            entry.DisplayEntry();
        }

    }
    public void SaveToFile()
    {
        Console.WriteLine("What is the filename?");
        string filename = Console.ReadLine();
        csvUtility.WriteCsv(entries, filename);
    }
    public void LoadFromFile()
    {
        Console.WriteLine("What is the filename?");
        string filename = Console.ReadLine();
        entries = csvUtility.ReadCsv(filename);
        foreach (var entry in entries)
        {
            entry.DisplayEntry();
        }

    }
}