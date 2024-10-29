using System;
using System.Collections.Generic;

public class Word
{
    public static List<string> HideWords(string text, int wordsToHide)
    {
        List<string> words = new List<string>(text.Split(' '));
        Random random = new Random();

        for (int i = 0; i < wordsToHide; i++)
        {
            int index = random.Next(words.Count);
            while (words[index] == "___")
            {
                index = random.Next(words.Count);
            }
            words[index] = "___";
        }

        return words;
    }
}

