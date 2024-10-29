using System.Collections.Generic;
using System.IO;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration.Attributes;

public class Reference
{
    [Name("volume_title")]
    public string _volumeTitle {get; set;}

    [Name("book_title")]
    public string _bookTitle {get; set;}

    [Name("chapter_number")]
    public string _chapterNumber {get; set;}

    [Name("verse_number")]
    public string _verseNumber {get; set;}

    [Name("scripture_text")]
    public string ScriptureText {get; set;}

    public static List<Reference> LoadReferences(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            return new List<Reference>(csv.GetRecords<Reference>());
        }
    }
}

