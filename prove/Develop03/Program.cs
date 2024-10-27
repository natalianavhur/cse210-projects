using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class Program
{
    private static List<Scripture> scriptures = new List<Scripture>();

    public static void Main(string[] args)
    {
        LoadJsonData();

        while (true)
        {
            DisplayMenu();
            Scripture selectedScripture = GetUserSelection();
            DisplayScripture(selectedScripture);
            DisappearWords(selectedScripture);
        }
    }

    /// <summary>
    /// Loads the scripture data from a JSON file.
    /// </summary>
    private static void LoadJsonData()
    {
        try
        {
            string json = File.ReadAllText("data.json");
            var books = JsonSerializer.Deserialize<Books>(json);

            // Ensure there are books available
            if (books?.books == null || !books.books.Any())
            {
                Console.WriteLine("No books found in the JSON file. Please check the file structure.");
                return;
            }

            // Load scriptures from the books
            foreach (var book in books.books)
            {
                foreach (var chapter in book.chapters)
                {
                    foreach (var verse in chapter.verses)
                    {
                        var scripture = new Scripture
                        {
                            Reference = new Reference
                            {
                                Book = book.book,
                                Chapter = chapter.chapter,
                                Verse = verse.verse
                            },
                            Words = verse.text.Split(' ')
                                .Select(w => new Word { Text = w, IsHidden = false })
                                .ToList(),
                            OriginalText = verse.text
                        };
                        scriptures.Add(scripture);
                        Console.WriteLine($"Loaded scripture: {scripture.Reference.Book} {scripture.Reference.Chapter}:{scripture.Reference.Verse}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading data: {ex.Message}");
        }
    }

    /// <summary>
    /// Displays a menu of available scriptures.
    /// </summary>
    private static void DisplayMenu()
    {
        Console.Clear();
        Console.WriteLine("Select a Scripture by entering its number:");
        for (int i = 0; i < scriptures.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {scriptures[i].Reference.Book} {scriptures[i].Reference.Chapter}:{scriptures[i].Reference.Verse}");
        }
        Console.WriteLine("Enter the number of your choice:");
    }

    /// <summary>
    /// Gets the user's selection of scripture from the menu.
    /// </summary>
    /// <returns>The selected scripture.</returns>
    private static Scripture GetUserSelection()
    {
        int selection;
        while (true)
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out selection) && selection > 0 && selection <= scriptures.Count)
            {
                return scriptures[selection - 1]; // Correctly fetch the selected scripture
            }
            Console.WriteLine("Invalid selection. Please enter a number corresponding to the scripture:");
        }
    }

    /// <summary>
    /// Displays the selected scripture and its text.
    /// </summary>
    /// <param name="scripture">The scripture to display.</param>
    private static void DisplayScripture(Scripture scripture)
    {
        Console.Clear();
        Console.WriteLine($"{scripture.Reference.Book} {scripture.Reference.Chapter}:{scripture.Reference.Verse}");
        Console.WriteLine(string.Join(" ", scripture.Words.Select(w => w.IsHidden ? "_____" : w.Text)));
        System.Threading.Thread.Sleep(500); // Pause before hiding words
    }

    /// <summary>
    /// Hides words in the selected scripture one by one until all are hidden.
    /// </summary>
    /// <param name="scripture">The scripture to process.</param>
    private static void DisappearWords(Scripture scripture)
    {
        var random = new Random();
        while (scripture.Words.Any(w => !w.IsHidden))
        {
            var wordToHide = scripture.Words
                .Where(w => !w.IsHidden)
                .OrderBy(_ => random.Next())
                .FirstOrDefault();

            if (wordToHide != null) 
            {
                wordToHide.IsHidden = true;
                DisplayScripture(scripture); // Update display
            }
        }
    }
}

public class Books
{
    public List<Book> books { get; set; }
}

public class Book
{
    public string book { get; set; }
    public List<Chapter> chapters { get; set; }
}

public class Chapter
{
    public int chapter { get; set; }
    public List<Verse> verses { get; set; }
}

public class Verse
{
    public string text { get; set; }
    public int verse { get; set; }
}
