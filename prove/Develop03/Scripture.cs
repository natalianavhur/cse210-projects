using System.Collections.Generic;
using System.Linq;

public class Scripture
{
    private List<Reference> _references;

    public Scripture(string filePath)
    {
        _references = Reference.LoadReferences(filePath);
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
}
