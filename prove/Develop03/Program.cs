using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        string filePath = "scriptures.csv";
        var scripture = new Scripture(filePath);
        var scriptureLibrary = new ScriptureLibrary(scripture);

        while (true)
        {
            scriptureLibrary.DisplayVolumeOptions();
            string volumeChoice = Console.ReadLine();
            if (volumeChoice.ToLower() == "quit") break;

            scriptureLibrary.DisplayBookOptions(volumeChoice);
            string bookChoice = Console.ReadLine();

            scriptureLibrary.DisplayChapterOptions(volumeChoice, bookChoice);
            string chapterChoice = Console.ReadLine();

            Console.WriteLine("Choose the verse number or range (1 or 1-7):");
            string verseChoice = Console.ReadLine();

            var scriptures = scriptureLibrary.GetScripturesInRange(volumeChoice, bookChoice, chapterChoice, verseChoice);
            scriptureLibrary.DisplayScriptureTexts(scriptures);

            if (scriptures.Any())
            {
                scripture.SelectScriptureText(scriptures.First());
                scripture.HideWordsInScripture();
            }
        }

        Console.WriteLine("The program ends!");
    }

}