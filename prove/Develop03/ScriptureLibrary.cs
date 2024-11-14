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
        var volumes = _scripture.GetDistinctVolumeTitles().ToList();
        Console.WriteLine("Choose one of the following volume titles or type 'quit' to exit:");
        DisplayInGrid(volumes, false);
    }

    public void DisplayBookOptions(string volumeTitle)
    {
        var books = _scripture.GetBooksInVolume(volumeTitle).ToList();
        Console.WriteLine("Choose one of the following book titles:");
        DisplayInGrid(books, false);
    }

    public void DisplayChapterOptions(string volumeTitle, string bookTitle)
    {
        var chapters = _scripture.GetChaptersInBook(volumeTitle, bookTitle).ToList();
        Console.WriteLine("Choose one of the following chapter numbers:");
        DisplayInGrid(chapters, true);
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

    public void DisplayInGrid(IEnumerable<string> items, bool isChapter)
    {
        var itemList = items.ToList();

        int itemsPerRow = isChapter ? 5 : 3;
        int padding = isChapter ? 3 : 7;

        int rowCount = (int)Math.Ceiling((double)itemList.Count / itemsPerRow);

        int maxLength = itemList.Max(item => item.Length);

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < itemsPerRow; j++)
            {
                int itemIndex = i * itemsPerRow + j;
                if (itemIndex < itemList.Count)
                {
                    string item = itemList[itemIndex].PadRight(maxLength + padding);
                    Console.Write(item);
                }
                else
                {
                    Console.Write(new string(' ', maxLength + padding));
                }
            }
            Console.WriteLine();
        }
    }
    public List<Reference> GetScripturesInRange(string volumeTitle, string bookTitle, string chapterNumber, string verseRange)
    {
        return _scripture.GetScripturesInRange(volumeTitle, bookTitle, chapterNumber, verseRange);
    }
}

