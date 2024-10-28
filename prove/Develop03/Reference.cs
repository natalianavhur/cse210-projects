using CsvHelper.Configuration.Attributes;

public class Reference
{
    [Name("volume_title")]
    public string _volumeTitle { get; set; }

    [Name("book_title")]
    public string _bookTitle { get; set; }

    [Name("chapter_number")]
    public string _chapterNumber { get; set; }

    [Name("verse_number")]
    public string _verseNumber { get; set; }

    [Name("scripture_text")]
    public string ScriptureText { get; set; }
}
