using System;
using System.Collections.Generic;
using System.Linq;

public class ScriptureDisplay
{
    public static void DisplayVolumeOptions(IEnumerable<string> volumes)
    {
        Console.WriteLine("Choose one of the following volume titles or type 'quit' to exit:");
        foreach (var volume in volumes)
        {
            Console.WriteLine(volume);
        }
    }

    public static void DisplayBookOptions(IEnumerable<string> books)
    {
        Console.WriteLine("Choose one of the following book titles:");
        foreach (var book in books)
        {
            Console.WriteLine(book);
        }
    }

    public static void DisplayChapterOptions(IEnumerable<string> chapters)
    {
        Console.WriteLine("Choose one of the following chapter numbers:");
        foreach (var chapter in chapters)
        {
            Console.WriteLine(chapter);
        }
    }

    public static void DisplayScriptureTexts(IEnumerable<Reference> scriptures)
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

    public static void HideWordsInScripture(Reference reference, int wordsToHide = 2)
    {
        string scriptureText = reference.ScriptureText;
        List<string> words = scriptureText.Split(' ').ToList();
        Random random = new Random();

        while (words.Exists(word => !word.Equals("___")))
        {
            Console.WriteLine("\nPress Enter to hide some words...");
            Console.ReadLine();
            words = Word.HideWords(string.Join(' ', words), wordsToHide);
            Console.WriteLine(string.Join(' ', words));
        }
        Console.WriteLine("All words in the verse are now hidden.");
    }
}
