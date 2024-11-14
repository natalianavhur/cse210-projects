using System;
using System.Collections.Generic;
using System.Linq;


public class Scripture
{
    private List<Reference> _references;
    private List<Word> _words;

    public Scripture(string filePath)
    {
        try
        {
            _references = Reference.LoadReferences(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading references from file: {ex.Message}");
            _references = new List<Reference>();
        }
    }

    public IEnumerable<string> GetDistinctVolumeTitles()
    {
        return _references.Select(r => r.VolumeTitle).Distinct();
    }

    public IEnumerable<string> GetBooksInVolume(string volumeTitle)
    {
        return _references.Where(r => r.VolumeTitle == volumeTitle)
                          .Select(r => r.BookTitle)
                          .Distinct();
    }

    public IEnumerable<string> GetChaptersInBook(string volumeTitle, string bookTitle)
    {
        return _references.Where(r => r.VolumeTitle == volumeTitle && r.BookTitle == bookTitle)
                          .Select(r => r.ChapterNumber)
                          .Distinct();
    }

    public List<Reference> GetScripturesInRange(string volumeTitle, string bookTitle, string chapterNumber, string verseRange)
    {
        var scriptures = new List<Reference>();

        if (verseRange.Contains("-"))
        {
            var range = verseRange.Split('-');
            int startVerse = int.Parse(range[0]);
            int endVerse = int.Parse(range[1]);

            scriptures = _references.Where(r => r.VolumeTitle == volumeTitle &&
                                                r.BookTitle == bookTitle &&
                                                r.ChapterNumber == chapterNumber &&
                                                int.Parse(r.VerseNumber) >= startVerse &&
                                                int.Parse(r.VerseNumber) <= endVerse)
                                    .ToList();
        }
        else
        {
            scriptures = _references.Where(r => r.VolumeTitle == volumeTitle &&
                                                r.BookTitle == bookTitle &&
                                                r.ChapterNumber == chapterNumber &&
                                                r.VerseNumber == verseRange)
                                    .ToList();
        }

        return scriptures;
    }

    public void SelectScriptureText(Reference reference)
    {
        _words = reference.ScriptureText.Split(' ')
                                         .Select(word => new Word(word))
                                         .ToList();
    }

    public void HideWordsInScripture(int wordsToHide = 2)
    {
        Random random = new Random();

        while (_words.Count(w => !w.IsHidden) > 0)
        {
            Console.Clear();
            Console.WriteLine("Current Scripture:");
            Console.WriteLine(string.Join(' ', _words));

            Console.WriteLine("\nPress Enter to hide some words...");
            Console.ReadLine();

            for (int i = 0; i < wordsToHide && _words.Any(w => !w.IsHidden); i++)
            {
                int index = random.Next(_words.Count);
                while (_words[index].IsHidden)
                {
                    index = random.Next(_words.Count);
                }
                _words[index].Hide();
            }

            Console.Clear();
            Console.WriteLine("Updated Scripture:");
            Console.WriteLine(string.Join(' ', _words));
        }

        Console.WriteLine("\nAll words in the verse are now hidden.");
    }
}