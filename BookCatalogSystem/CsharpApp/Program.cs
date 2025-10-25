using System;

class Program
{
    static void Main()
    {
        CatalogManager catalog = new CatalogManager();
        bool exit = false;

        while (!exit)
        {
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

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Title: "); string title = Console.ReadLine();
                    Console.Write("Author: "); string author = Console.ReadLine();
                    Console.Write("Genre: "); string genre = Console.ReadLine();
                    Console.Write("Publication Year: ");
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
