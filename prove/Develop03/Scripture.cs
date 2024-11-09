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
        return _references.Select(r => r._volumeTitle).Distinct();
    }

    public IEnumerable<string> GetBooksInVolume(string volumeTitle)
    {
        return _references.Where(r => r._volumeTitle == volumeTitle).Select(r => r._bookTitle).Distinct();
    }

    public IEnumerable<string> GetChaptersInBook(string volumeTitle, string bookTitle)
    {
        return _references.Where(r => r._volumeTitle == volumeTitle && r._bookTitle == bookTitle)
                          .Select(r => r._chapterNumber).Distinct();
    }

    public List<Reference> GetScripturesInRange(string volumeTitle, string bookTitle, string chapterNumber, string verseRange)
    {
        var scriptures = new List<Reference>();
        if (verseRange.Contains("-"))
        {
            var range = verseRange.Split('-');
            int startVerse = int.Parse(range[0]);
            int endVerse = int.Parse(range[1]);
            scriptures = _references.Where(r => r._volumeTitle == volumeTitle && r._bookTitle == bookTitle &&
                                                r._chapterNumber == chapterNumber &&
                                                int.Parse(r._verseNumber) >= startVerse && int.Parse(r._verseNumber) <= endVerse)
                                     .ToList();
        }
        else
        {
            scriptures = _references.Where(r => r._volumeTitle == volumeTitle && r._bookTitle == bookTitle &&
                                                r._chapterNumber == chapterNumber && r._verseNumber == verseRange)
                                     .ToList();
        }
        return scriptures;
    }
    public void SelectScriptureText(Reference reference)
    {
        _words = reference._scriptureText.Split(' ').Select(word => new Word(word)).ToList();
    }

    public void HideWordsInScripture(int wordsToHide = 2)
    {
        Random random = new Random();

        while (_words.Count(w => !w._isHidden) > 0)
        {
            Console.WriteLine("\nPress Enter to hide some words...");
            Console.ReadLine();

            for (int i = 0; i < wordsToHide && _words.Any(w => !w._isHidden); i++)
            {
                int index = random.Next(_words.Count);
                while (_words[index]._isHidden)
                {
                    index = random.Next(_words.Count);
                }
                _words[index].Hide();
            }

            Console.WriteLine(string.Join(' ', _words));
        }
        Console.WriteLine("All words in the verse are now hidden.");
    }
}
