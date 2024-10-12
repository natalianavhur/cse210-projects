public class Journal
{
    public List<Entry> entries = new List<Entry>();
    CsvUtility csvUtility = new CsvUtility();
    public void AddEntry()
    {
        Entry newEntry = new Entry();

        newEntry.date = newEntry.GetDate();
        newEntry.prompt = newEntry.GeneratePrompt();
        newEntry.DisplayPrompt(newEntry.prompt);
        newEntry.response = newEntry.GetResponse();

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