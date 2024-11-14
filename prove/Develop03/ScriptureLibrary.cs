using System;
using System.Collections.Generic;
using System.Linq;


public class ScriptureLibrary
{
    private Scripture _scripture;

    public ScriptureLibrary(Scripture scripture)
    {
        _scripture = scripture;
    }

    public void DisplayVolumeOptions()
    {
        var volumes = _scripture.GetDistinctVolumeTitles();
        Console.WriteLine("Choose one of the following volume titles or type 'quit' to exit:");
        foreach (var volume in volumes)
        {
            Console.WriteLine(volume);
        }
    }

    public void DisplayBookOptions(string volumeTitle)
    {
        var books = _scripture.GetBooksInVolume(volumeTitle);
        Console.WriteLine("Choose one of the following book titles:");
        foreach (var book in books)
        {
            Console.WriteLine(book);
        }
    }

    public void DisplayChapterOptions(string volumeTitle, string bookTitle)
    {
        var chapters = _scripture.GetChaptersInBook(volumeTitle, bookTitle);
        Console.WriteLine("Choose one of the following chapter numbers:");
        foreach (var chapter in chapters)
        {
            Console.WriteLine(chapter);
        }
    }

    public void DisplayScriptureTexts(List<Reference> scriptures)
    {
        if (scriptures.Any())
        {
            Console.WriteLine("Scripture Texts:");
            foreach (var scripture in scriptures)
            {
                Console.WriteLine(scripture.ScriptureText);
            }
        }
        else
        {
            Console.WriteLine("Scripture not found.");
        }
    }

    public List<Reference> GetScripturesInRange(string volumeTitle, string bookTitle, string chapterNumber, string verseRange)
    {
        return _scripture.GetScripturesInRange(volumeTitle, bookTitle, chapterNumber, verseRange);
    }
}

