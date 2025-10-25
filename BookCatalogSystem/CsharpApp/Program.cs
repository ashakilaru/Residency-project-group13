using System;

class Program
{
    static void Main()
    {
         // Initialize the catalog manager, which handles all book operations
        CatalogManager catalog = new CatalogManager();
        bool exit = false;

         // Main loop: repeatedly display the menu until the user chooses to exit
        while (!exit)
        {
            // Display menu options to the user
            Console.WriteLine("\n--- Book Catalog System ---");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Remove Book");
            Console.WriteLine("3. Search Book by Title");
            Console.WriteLine("4. Search Book by Author");
            Console.WriteLine("5. Search Book by Genre");
            Console.WriteLine("6. Display All Books");
            Console.WriteLine("7. Report by Genre");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");

             // Read user input
            string choice = Console.ReadLine();

             // Handle menu selection using a switch statement
            switch (choice)
            {
                case "1":
                     // Prompt user for book details
                    Console.Write("Title: "); string title = Console.ReadLine();
                    Console.Write("Author: "); string author = Console.ReadLine();
                    Console.Write("Genre: "); string genre = Console.ReadLine();
                    Console.Write("Publication Year: ");

                    // Validate the year input; add book if valid
                    if (int.TryParse(Console.ReadLine(), out int year))
                    {
                        catalog.AddBook(new Book(title, author, genre, year));
                    }
                    else
                    {
                        Console.WriteLine("Invalid year. Book not added.");
                    }
                    break;

                case "2":
                    Console.Write("Title to remove: "); string removeTitle = Console.ReadLine();
                    catalog.RemoveBook(removeTitle);
                    break;

                case "3":
                    Console.Write("Search title: "); string searchTitle = Console.ReadLine();
                    DisplaySearchResults(catalog.SearchByTitle(searchTitle));
                    break;

                case "4":
                    Console.Write("Search author: "); string searchAuthor = Console.ReadLine();
                    DisplaySearchResults(catalog.SearchByAuthor(searchAuthor));
                    break;

                case "5":
                    Console.Write("Search genre: "); string searchGenre = Console.ReadLine();
                    DisplaySearchResults(catalog.SearchByGenre(searchGenre));
                    break;

                case "6":
                    catalog.DisplayBooks();
                    break;

                case "7":
                      // Generate a report showing the number of books per genre
                    catalog.ReportByGenre();
                    break;

                case "0":
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

     // Helper method to display search results in a readable format
    static void DisplaySearchResults(System.Collections.Generic.List<Book> results)
    {
        if (results.Count == 0)
        {
            Console.WriteLine("No books found.");
        }
        else
        {
            foreach (var book in results)
            {
                Console.WriteLine(book.ToString());
            }
        }
    }
}
