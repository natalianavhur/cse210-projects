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
                List<string> csvParts = new List<string>();

                csvParts.Add(FormatCsv(entry.date));
                csvParts.Add(FormatCsv(entry.time));

                if (!string.IsNullOrWhiteSpace(entry.title))
                {
                    csvParts.Add(FormatCsv(entry.title));
                }
                if (!string.IsNullOrWhiteSpace(entry.prompt))
                {
                    csvParts.Add(FormatCsv(entry.prompt));
                }
                if (!string.IsNullOrWhiteSpace(entry.response))
                {
                    csvParts.Add(FormatCsv(entry.response));
                }
                if (!string.IsNullOrWhiteSpace(entry.reflection))
                {
                    csvParts.Add(FormatCsv(entry.reflection));
                }
                string csvLine = string.Join(",", csvParts);
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
                    var csvParts = line.Split(',');
                    Entry entry = new Entry();

                    if (csvParts.Length >= 2)
                    {
                        entry.date = csvParts[0].Trim('"');
                        entry.time = csvParts[1].Trim('"');
                    }
                    if (csvParts.Length >= 3)
                    {
                        entry.title = csvParts[2].Trim('"');
                    }
                    if (csvParts.Length >= 4)
                    {
                        entry.prompt = csvParts[3].Trim('"');
                    }
                    if (csvParts.Length >= 5)
                    {
                        entry.response = csvParts[4].Trim('"');
                    }
                    if (csvParts.Length >= 6)
                    {
                        entry.reflection = csvParts[5].Trim('"');
                    }

                    entries.Add(entry);
                }
            }
        }
        return entries;
    }

    private string FormatCsv(string entry)
    {
        if (entry.Contains("\"") || entry.Contains(",") || entry.Contains("\n"))
        {
            entry = entry.Replace("\"", "\"\"");
            entry = $"\"{entry}\"";
        }
        return entry;
    }
}
