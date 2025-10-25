using System;

// Represents a single book in the catalog
public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public int PublicationYear { get; set; }
    
    // Default constructor allows creating an empty book object
    public Book() { }

    // Parameterized constructor to initialize a book with all properties
    public Book(string title, string author, string genre, int year)
    {
        Title = title;
        Author = author;
        Genre = genre;
        PublicationYear = year;
    }

     // Returns a readable string representation of the book
    public override string ToString()
    {
        return $"{Title} | {Author} | {Genre} | {PublicationYear}";
    }
}
