public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public int PublicationYear { get; set; }
    public int Quantity { get; set; }

    public Book(string title, string author, string genre, int year, int quantity = 1)
    {
        Title = title;
        Author = author;
        Genre = genre;
        PublicationYear = year;
        Quantity = quantity;
    }

    public override string ToString()
    {
        return $"{Title} | {Author} | {Genre} | {PublicationYear} | Stock: {Quantity}";
    }
}
