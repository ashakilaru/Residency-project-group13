using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;

// Manages the collection of books and handles all catalog operations
public class CatalogManager
{
    private List<Book> books = new List<Book>();

    // Read-only access to the books list
    public List<Book> Books => books;

    public CatalogManager()
    {
        LoadBooks();
    }

    // Adds a new book to the catalog or increases stock if it already exists
    public void AddBook(Book newBook)
    {
        var existingBook = books.FirstOrDefault(b =>
            b.Title.Equals(newBook.Title, StringComparison.OrdinalIgnoreCase) &&
            b.Author.Equals(newBook.Author, StringComparison.OrdinalIgnoreCase) &&
            b.Genre.Equals(newBook.Genre, StringComparison.OrdinalIgnoreCase) &&
            b.PublicationYear == newBook.PublicationYear);

        if (existingBook != null)
        {
            existingBook.Quantity += newBook.Quantity;
            Console.WriteLine($"Book already exists. Increased stock to {existingBook.Quantity}.");
        }
        else
        {
            books.Add(newBook);
            Console.WriteLine("New book added successfully!");
        }

        SaveBooks();
    }

    // Removes a book by prompting the user
    public void RemoveBook()
    {
        Console.Write("Title to remove: ");
        string? title = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Invalid title. No book removed.");
            return;
        }

        var matchingBooks = books.Where(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase)).ToList();

        if (matchingBooks.Count == 0)
        {
            Console.WriteLine("No book found with that title.");
            return;
        }

        Book selectedBook;

        if (matchingBooks.Count > 1)
        {
            Console.WriteLine("\nMultiple books found with that title:");
            for (int i = 0; i < matchingBooks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {matchingBooks[i].Title} | {matchingBooks[i].Author} | {matchingBooks[i].Genre} | {matchingBooks[i].PublicationYear} | Stock: {matchingBooks[i].Quantity}");
            }

            Console.Write("\nEnter the number of the book you want to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > matchingBooks.Count)
            {
                Console.WriteLine("Invalid choice. No book removed.");
                return;
            }

            selectedBook = matchingBooks[choice - 1];
        }
        else
        {
            selectedBook = matchingBooks[0];
        }

        RemoveBookQuantity(selectedBook);
        SaveBooks();
    }

    // Removes quantity or entire book
    private void RemoveBookQuantity(Book book)
    {
        if (book.Quantity > 1)
        {
            Console.Write($"This book has {book.Quantity} copies. How many do you want to remove? ");
            if (int.TryParse(Console.ReadLine(), out int qtyToRemove) && qtyToRemove > 0)
            {
                if (qtyToRemove >= book.Quantity)
                {
                    books.Remove(book);
                    Console.WriteLine("All copies removed from catalog!");
                }
                else
                {
                    book.Quantity -= qtyToRemove;
                    Console.WriteLine($"{qtyToRemove} copies removed. Remaining stock: {book.Quantity}");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. No book removed.");
            }
        }
        else
        {
            books.Remove(book);
            Console.WriteLine("Book removed from catalog!");
        }
    }

    // Search books
    public List<Book> SearchByTitle(string title)
        => books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();

    public List<Book> SearchByAuthor(string author)
        => books.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();

    public List<Book> SearchByGenre(string genre)
        => books.Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)).ToList();

    // Display all books
    public void DisplayBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No books in catalog.");
            return;
        }

        foreach (var book in books)
        {
            Console.WriteLine($"{book.Title} | {book.Author} | {book.Genre} | {book.PublicationYear} | Stock: {book.Quantity}");
        }
    }

    // Report
    public void ReportByGenre()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No books to report.");
            return;
        }

        var report = books.GroupBy(b => b.Genre)
                          .Select(g => new { Genre = g.Key, Count = g.Count() });

        Console.WriteLine("\nBooks by Genre:");
        foreach (var r in report)
        {
            Console.WriteLine($"{r.Genre}: {r.Count} books");
        }
    }

    public void ReportByAuthor()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No books to report.");
            return;
        }

        var report = books.GroupBy(b => b.Author)
                          .Select(g => new { Author = g.Key, Count = g.Count() });

        Console.WriteLine("\nBooks by Author:");
        foreach (var r in report)
        {
            Console.WriteLine($"{r.Author}: {r.Count} book(s)");
        }
    }

    // Save/load JSON
    private void SaveBooks()
    {
        try
        {
            File.WriteAllText("books.json", JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true }));
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
                books = JsonSerializer.Deserialize<List<Book>>(File.ReadAllText("books.json")) ?? new List<Book>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading books: {ex.Message}");
            books = new List<Book>();
        }
    }
}
