using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;

public class CatalogManager
{
    public List<Book> Books { get; private set; }

    public CatalogManager()
    {
        Books = new List<Book>();
        LoadBooks();
    }

    public void AddBook(Book book)
    {
        Books.Add(book);
        Console.WriteLine("Book added successfully!");
        SaveBooks();
    }

    public void RemoveBook(string title)
    {
        var bookToRemove = Books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        if (bookToRemove != null)
        {
            Books.Remove(bookToRemove);
            Console.WriteLine("Book removed successfully!");
            SaveBooks();
        }
        else
        {
            Console.WriteLine("Book not found!");
        }
    }

    public List<Book> SearchByTitle(string title)
    {
        return Books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public List<Book> SearchByAuthor(string author)
    {
        return Books.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public List<Book> SearchByGenre(string genre)
    {
        return Books.Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public void DisplayBooks()
    {
        if (Books.Count == 0)
        {
            Console.WriteLine("No books in catalog.");
            return;
        }

        foreach (var book in Books)
        {
            Console.WriteLine(book.ToString());
        }
    }

    public void ReportByGenre()
    {
        if (Books.Count == 0)
        {
            Console.WriteLine("No books to report.");
            return;
        }

        var report = Books.GroupBy(b => b.Genre)
                          .Select(g => new { Genre = g.Key, Count = g.Count() });

        Console.WriteLine("\nBooks by Genre:");
        foreach (var r in report)
        {
            Console.WriteLine($"{r.Genre}: {r.Count} books");
        }
    }

    private void SaveBooks()
    {
        try
        {
            File.WriteAllText("books.json", JsonSerializer.Serialize(Books, new JsonSerializerOptions { WriteIndented = true }));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving books: {ex.Message}");
        }
    }

    private void LoadBooks()
    {
        try
        {
            if (File.Exists("books.json"))
            {
                Books = JsonSerializer.Deserialize<List<Book>>(File.ReadAllText("books.json"));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading books: {ex.Message}");
            Books = new List<Book>();
        }
    }
}
