using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        // Change the file path according to where your file is located.
        string filePath = @"C:\Users\nnavh\OneDrive\Desktop\cse210\cse210-projects\prove\Develop03\scriptures.csv";
        var scriptureReader = new Scripture(filePath);
        var volumes = scriptureReader.GetDistinctVolumeTitles();

        while (true)
        {
            ScriptureDisplay.DisplayVolumeOptions(volumes);
            string volumeChoice = Console.ReadLine();
            if (volumeChoice.ToLower() == "quit") break;

            var booksInVolume = scriptureReader.GetBooksInVolume(volumeChoice);
            ScriptureDisplay.DisplayBookOptions(booksInVolume);
            string bookChoice = Console.ReadLine();

            var chaptersInBook = scriptureReader.GetChaptersInBook(volumeChoice, bookChoice);
            ScriptureDisplay.DisplayChapterOptions(chaptersInBook);
            string chapterChoice = Console.ReadLine();

            Console.WriteLine("Choose the verse number or range (1 or 1-7):");
            string verseChoice = Console.ReadLine();

            var scriptures = scriptureReader.GetScripturesInRange(volumeChoice, bookChoice, chapterChoice, verseChoice);
            ScriptureDisplay.DisplayScriptureTexts(scriptures);

            if (scriptures.Any())
            {
                ScriptureDisplay.HideWordsInScripture(scriptures.First());
            }
        }
        Console.WriteLine("The program ends!");
    }
}
