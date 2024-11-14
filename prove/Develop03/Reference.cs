using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration.Attributes;


public class Reference
{
    [Name("volume_title")]
    private string _volumeTitle;
    public string VolumeTitle
    {
        get => _volumeTitle;
        set => _volumeTitle = value;
    }

    [Name("book_title")]
    private string _bookTitle;
    public string BookTitle
    {
        get => _bookTitle;
        set => _bookTitle = value;
    }

    [Name("chapter_number")]
    private string _chapterNumber;
    public string ChapterNumber
    {
        get => _chapterNumber;
        set => _chapterNumber = value;
    }

    [Name("verse_number")]
    private string _verseNumber;
    public string VerseNumber
    {
        get => _verseNumber;
        set => _verseNumber = value;
    }

    [Name("scripture_text")]
    private string _scriptureText;
    public string ScriptureText
    {
        get => _scriptureText;
        set => _scriptureText = value;
    }

    public static List<Reference> LoadReferences(string filePath)
    {
        try
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return new List<Reference>(csv.GetRecords<Reference>());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading references: {e.Message}");
            return new List<Reference>();
        }
    }
}





