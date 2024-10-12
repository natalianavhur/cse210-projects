using System;
using System.IO;
using System.Collections.Generic;


public class CsvUtility
{
    public void WriteCsv(List<Entry> entries, string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (Entry entry in entries)
            {
                string csvLine = $"{EscapeCsv(entry.date)}," +
                                 $"{EscapeCsv(entry.prompt)}," +
                                 $"{EscapeCsv(entry.response)}";
                outputFile.WriteLine(csvLine);
            }
        }
    }

    public List<Entry> ReadCsv(string filename)
    {
        List<Entry> entries = new List<Entry>();

        if (File.Exists(filename))
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(new[] { ',' }, 3);
                    if (parts.Length == 3)
                    {
                        Entry entry = new Entry
                        {
                            date = parts[0].Trim('"'),
                            prompt = parts[1].Trim('"'),
                            response = parts[2].Trim('"')
                        };
                        entries.Add(entry);
                    }
                    else
                    {
                        Console.WriteLine("Incorrect number of parts in line.");
                    }
                }
            }
        }
        return entries;
    }

    private string EscapeCsv(string value)
    {
        if (value.Contains("\"") || value.Contains(",") || value.Contains("\n"))
        {
            value = value.Replace("\"", "\"\"");
            value = $"\"{value}\"";
        }
        return value;
    }
}
