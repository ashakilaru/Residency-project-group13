using System;

class Program
{
    static void Main()
    {
        CatalogManager catalog = new CatalogManager();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n=== Book Catalog System ===");
            Console.WriteLine("1. Add a Book");
            Console.WriteLine("2. List All Books");
            Console.WriteLine("3. Search Book by Title");
            Console.WriteLine("4. Search Book by Author");
            Console.WriteLine("5. Search Book by Genre");
            Console.WriteLine("6. Delete Book");
            Console.WriteLine("7. Report by Genre");
            Console.WriteLine("8. Report by Author");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");

            string? input = Console.ReadLine();
            Console.WriteLine();

            switch (input)
            {
                case "1":
                    // Add a book
                    Console.Write("Title: ");
                    string title = Console.ReadLine() ?? "";
                    Console.Write("Author: ");
                    string author = Console.ReadLine() ?? "";
                    Console.Write("Genre: ");
                    string genre = Console.ReadLine() ?? "";
                    Console.Write("Publication Year: ");
                    int year = int.TryParse(Console.ReadLine(), out int y) ? y : 0;
                    Console.Write("Quantity: ");
                    int qty = int.TryParse(Console.ReadLine(), out int q) ? q : 1;

                    catalog.AddBook(new Book(title, author, genre, year, qty));
                    break;

                case "2":
                    // List all books
                    catalog.DisplayBooks();
                    break;

                case "3":
                    // Search by title
                    Console.Write("Enter title to search: ");
                    string searchTitle = Console.ReadLine() ?? "";
                    var titleResults = catalog.SearchByTitle(searchTitle);
                    if (titleResults.Count == 0)
                        Console.WriteLine("No books found with that title.");
                    else
                        foreach (var b in titleResults)
                            Console.WriteLine($"{b.Title} | {b.Author} | {b.Genre} | {b.PublicationYear} | Stock: {b.Quantity}");
                    break;

                case "4":
                    // Search by author
                    Console.Write("Enter author to search: ");
                    string searchAuthor = Console.ReadLine() ?? "";
                    var authorResults = catalog.SearchByAuthor(searchAuthor);
                    if (authorResults.Count == 0)
                        Console.WriteLine("No books found for that author.");
                    else
                        foreach (var b in authorResults)
                            Console.WriteLine($"{b.Title} | {b.Author} | {b.Genre} | {b.PublicationYear} | Stock: {b.Quantity}");
                    break;

                case "5":
                    // Search by genre
                    Console.Write("Enter genre to search: ");
                    string searchGenre = Console.ReadLine() ?? "";
                    var genreResults = catalog.SearchByGenre(searchGenre);
                    if (genreResults.Count == 0)
                        Console.WriteLine("No books found in that genre.");
                    else
                        foreach (var b in genreResults)
                            Console.WriteLine($"{b.Title} | {b.Author} | {b.Genre} | {b.PublicationYear} | Stock: {b.Quantity}");
                    break;

                case "6":
                    // Delete book
                    catalog.RemoveBook();
                    break;

                case "7":
                    // Report by genre
                    catalog.ReportByGenre();
                    break;

                case "8":
                    // Report by author
                    catalog.ReportByAuthor();
                    break;

                case "0":
                    running = false;
                    Console.WriteLine("Exiting...");
                    break;

                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }
}
