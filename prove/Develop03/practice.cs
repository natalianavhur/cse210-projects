// class Program
// {
//     static void Main(string[] args)
//     {
//         bool continueProgram = true;

//         while (continueProgram)
//         {
//             Console.Clear();
//             Console.WriteLine("Main Menu");
//             Console.WriteLine("1. Start Activity");
//             Console.WriteLine("2. View Activity History");
//             Console.WriteLine("3. Exit");
//             Console.Write("Please choose an option: ");

//             string choice = Console.ReadLine();

//             switch (choice)
//             {
//                 case "1":
//                     // Choose which activity to start
//                     Console.Clear();
//                     Console.WriteLine("Choose an activity to start:");
//                     Console.WriteLine("1. Breathing");
//                     Console.WriteLine("2. Reflecting");
//                     Console.WriteLine("3. Listing");
//                     Console.Write("Please choose an option: ");
//                     string activityChoice = Console.ReadLine();

//                     Activities activity = null;

//                     // Initialize the chosen activity
//                     switch (activityChoice)
//                     {
//                         case "1":
//                             activity = new Breathing("Breathing", "A breathing activity for relaxation");
//                             break;
//                         case "2":
//                             activity = new Reflecting("Reflecting", "A reflection activity to relax and reflect");
//                             break;
//                         case "3":
//                             activity = new Listing("Listing", "A listing activity to organize thoughts");
//                             break;
//                         default:
//                             Console.WriteLine("Invalid choice, returning to main menu.");
//                             continue;
//                     }

//                     // Start the chosen activity
//                     activity.StartMessage();
//                     Console.WriteLine("Preparing...");
//                     activity.AnimateTime(5); // Short preparation animation

//                     // Get the duration for the activity
//                     activity.GetDuration();

//                     // Perform activity-specific actions based on the type of activity
//                     if (activity is Breathing breathingActivity)
//                     {
//                         Console.Clear();
//                         Console.WriteLine("Get Ready");
//                         activity.AnimateTime(5); // Get ready animation for breathing
//                         breathingActivity.StartBreathing();
//                     }
//                     else if (activity is Reflecting reflectingActivity)
//                     {
//                         reflectingActivity.StartReflecting();
//                     }
//                     else if (activity is Listing listingActivity)
//                     {
//                         listingActivity.StartListing();
//                     }

//                     // End the activity and log it
//                     activity.EndMessage();
//                     activity.LogActivity();
//                     break;

//                 case "2":
//                     // Display the history of activities
//                     Activities.DisplayHistory();
//                     Console.WriteLine("\nPress any key to return to the main menu.");
//                     Console.ReadKey();
//                     break;

//                 case "3":
//                     continueProgram = false;
//                     break;

//                 default:
//                     Console.WriteLine("Invalid choice. Please try again.");
//                     break;
//             }
//         }
//     }
// }



// using System;
// using System.Collections.Generic;

// public class Program
// {
//     public static void Main(string[] args)
//     {
//         string filePath = "scriptures.csv";
//         var scripture = new Scripture(filePath);
//         var scriptureLibrary = new ScriptureLibrary(scripture);

//         while (true)
//         {
//             scriptureLibrary.DisplayVolumeOptions();
//             string volumeChoice = Console.ReadLine();
//             if (volumeChoice.ToLower() == "quit") break;

//             scriptureLibrary.DisplayBookOptions(volumeChoice);
//             string bookChoice = Console.ReadLine();

//             scriptureLibrary.DisplayChapterOptions(volumeChoice, bookChoice);
//             string chapterChoice = Console.ReadLine();

//             Console.WriteLine("Choose the verse number or range (1 or 1-7):");
//             string verseChoice = Console.ReadLine();

//             var scriptures = scriptureLibrary.GetScripturesInRange(volumeChoice, bookChoice, chapterChoice, verseChoice);
//             scriptureLibrary.DisplayScriptureTexts(scriptures);

//             if (scriptures.Any())
//             {
//                 scripture.SelectScriptureText(scriptures.First());
//                 scripture.HideWordsInScripture();
//             }
//         }

//         Console.WriteLine("The program ends!");
//     }

// }

// using System;
// using System.Collections.Generic;
// using System.IO;
// using CsvHelper;
// using System.Globalization;
// using CsvHelper.Configuration.Attributes;
// public class Reference
// {
//     [Name("volume_title")]
//     private string _volumeTitle;
//     public string VolumeTitle
//     {
//         get => _volumeTitle;
//         set => _volumeTitle = value;
//     }

//     [Name("book_title")]
//     private string _bookTitle;
//     public string BookTitle
//     {
//         get => _bookTitle;
//         set => _bookTitle = value;
//     }

//     [Name("chapter_number")]
//     private string _chapterNumber;
//     public string ChapterNumber
//     {
//         get => _chapterNumber;
//         set => _chapterNumber = value;
//     }

//     [Name("verse_number")]
//     private string _verseNumber;
//     public string VerseNumber
//     {
//         get => _verseNumber;
//         set => _verseNumber = value;
//     }

//     [Name("scripture_text")]
//     private string _scriptureText;
//     public string ScriptureText
//     {
//         get => _scriptureText;
//         set => _scriptureText = value;
//     }

//     public static List<Reference> LoadReferences(string filePath)
//     {
//         try
//         {
//             using (var reader = new StreamReader(filePath))
//             using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
//             {
//                 return new List<Reference>(csv.GetRecords<Reference>());
//             }
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine($"Error loading references: {e.Message}");
//             return new List<Reference>();
//         }
//     }
// }





// using System;
// using System.Collections.Generic;

// public class Word
// {
//     private string _text;
//     public string Text
//     {
//         get => _text;
//         private set => _text = value;
//     }

//     private bool _isHidden;
//     public bool IsHidden
//     {
//         get => _isHidden;
//         private set => _isHidden = value;
//     }

//     public Word(string text)
//     {
//         Text = text;
//         IsHidden = false;
//     }

//     public void Hide()
//     {
//         IsHidden = true;
//     }

//     public override string ToString()
//     {
//         return IsHidden ? "___" : Text;
//     }
// }



// using System;
// using System.Collections.Generic;
// using System.Linq;

// public class Scripture
// {
//     private List<Reference> _references;
//     private List<Word> _words;

//     public Scripture(string filePath)
//     {
//         try
//         {
//             _references = Reference.LoadReferences(filePath);
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine($"Error loading references from file: {ex.Message}");
//             _references = new List<Reference>();
//         }
//     }

//     public IEnumerable<string> GetDistinctVolumeTitles()
//     {
//         return _references.Select(r => r.VolumeTitle).Distinct();
//     }

//     public IEnumerable<string> GetBooksInVolume(string volumeTitle)
//     {
//         return _references.Where(r => r.VolumeTitle == volumeTitle)
//                           .Select(r => r.BookTitle)
//                           .Distinct();
//     }

//     public IEnumerable<string> GetChaptersInBook(string volumeTitle, string bookTitle)
//     {
//         return _references.Where(r => r.VolumeTitle == volumeTitle && r.BookTitle == bookTitle)
//                           .Select(r => r.ChapterNumber)
//                           .Distinct();
//     }

//     public List<Reference> GetScripturesInRange(string volumeTitle, string bookTitle, string chapterNumber, string verseRange)
//     {
//         var scriptures = new List<Reference>();

//         if (verseRange.Contains("-"))
//         {
//             var range = verseRange.Split('-');
//             int startVerse = int.Parse(range[0]);
//             int endVerse = int.Parse(range[1]);

//             scriptures = _references.Where(r => r.VolumeTitle == volumeTitle &&
//                                                 r.BookTitle == bookTitle &&
//                                                 r.ChapterNumber == chapterNumber &&
//                                                 int.Parse(r.VerseNumber) >= startVerse &&
//                                                 int.Parse(r.VerseNumber) <= endVerse)
//                                     .ToList();
//         }
//         else
//         {
//             scriptures = _references.Where(r => r.VolumeTitle == volumeTitle &&
//                                                 r.BookTitle == bookTitle &&
//                                                 r.ChapterNumber == chapterNumber &&
//                                                 r.VerseNumber == verseRange)
//                                     .ToList();
//         }

//         return scriptures;
//     }

//     public void SelectScriptureText(Reference reference)
//     {
//         _words = reference.ScriptureText.Split(' ')
//                                          .Select(word => new Word(word))
//                                          .ToList();
//     }

//     public void HideWordsInScripture(int wordsToHide = 2)
//     {
//         Random random = new Random();

//         while (_words.Count(w => !w.IsHidden) > 0)
//         {
//             Console.WriteLine("\nPress Enter to hide some words...");
//             Console.ReadLine();

//             for (int i = 0; i < wordsToHide && _words.Any(w => !w.IsHidden); i++)
//             {
//                 int index = random.Next(_words.Count);
//                 while (_words[index].IsHidden)
//                 {
//                     index = random.Next(_words.Count);
//                 }
//                 _words[index].Hide();
//             }

//             Console.WriteLine(string.Join(' ', _words));
//         }

//         Console.WriteLine("All words in the verse are now hidden.");
//     }
// }

// using System;
// using System.Collections.Generic;
// using System.Linq;

// public class ScriptureLibrary
// {
//     private Scripture _scripture;

//     public ScriptureLibrary(Scripture scripture)
//     {
//         _scripture = scripture;
//     }

//     public void DisplayVolumeOptions()
//     {
//         var volumes = _scripture.GetDistinctVolumeTitles();
//         Console.WriteLine("Choose one of the following volume titles or type 'quit' to exit:");
//         foreach (var volume in volumes)
//         {
//             Console.WriteLine(volume);
//         }
//     }

//     public void DisplayBookOptions(string volumeTitle)
//     {
//         var books = _scripture.GetBooksInVolume(volumeTitle);
//         Console.WriteLine("Choose one of the following book titles:");
//         foreach (var book in books)
//         {
//             Console.WriteLine(book);
//         }
//     }

//     public void DisplayChapterOptions(string volumeTitle, string bookTitle)
//     {
//         var chapters = _scripture.GetChaptersInBook(volumeTitle, bookTitle);
//         Console.WriteLine("Choose one of the following chapter numbers:");
//         foreach (var chapter in chapters)
//         {
//             Console.WriteLine(chapter);
//         }
//     }

//     public void DisplayScriptureTexts(List<Reference> scriptures)
//     {
//         if (scriptures.Any())
//         {
//             Console.WriteLine("Scripture Texts:");
//             foreach (var scripture in scriptures)
//             {
//                 Console.WriteLine(scripture.ScriptureText);
//             }
//         }
//         else
//         {
//             Console.WriteLine("Scripture not found.");
//         }
//     }

//     public List<Reference> GetScripturesInRange(string volumeTitle, string bookTitle, string chapterNumber, string verseRange)
//     {
//         return _scripture.GetScripturesInRange(volumeTitle, bookTitle, chapterNumber, verseRange);
//     }
// }

