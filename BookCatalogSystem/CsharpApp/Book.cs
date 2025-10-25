using System;

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public int PublicationYear { get; set; }

    public Book() { }

    public Book(string title, string author, string genre, int year)
    {
        Title = title;
        Author = author;
        Genre = genre;
        PublicationYear = year;
    }

    public override string ToString()
    {
        return $"{Title} | {Author} | {Genre} | {PublicationYear}";
    }
}
