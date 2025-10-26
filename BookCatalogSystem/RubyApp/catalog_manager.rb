# catalog_manager.rb
# Manages the in-memory catalog of Book objects and handles persistence to books.json.

require_relative 'book'
require 'json'

class CatalogManager
  # Initialize catalog state, next ID counter, and load persisted books if present.
  def initialize
    @books = []       # Array to hold Book instances
    @next_id = 1      # Incremental ID assigned to new books
    load_books        # Populate @books from books.json if it exists
  end

  # Create a new Book, add it to the catalog, persist changes, and confirm to the user.
  # Parameters:
  # - title: String
  # - author: String
  # - genre: String
  # - publication_year: Integer or String
  def add_book(title, author, genre, publication_year)
    book = Book.new(@next_id, title, author, genre, publication_year)
    @books << book
    @next_id += 1
    save_books
    puts "\n✅ Book added successfully!"
  end

  # List all books in the catalog or show a message if the catalog is empty.
  def list_books
    if @books.empty?
      puts "\n📭 No books found."
    else
      puts "\n📚 Book Catalog:"
      @books.each { |book| puts book }   # Uses Book#to_s for formatted output
    end
  end

  # Search for books where title, author, or genre includes the keyword (case-insensitive).
  # Displays matching results or a not-found message.
  # Parameter:
  # - keyword: String
  def search_book(keyword)
    results = @books.select do |b|
      b.title.downcase.include?(keyword.downcase) ||
      b.author.downcase.include?(keyword.downcase) ||
      b.genre.downcase.include?(keyword.downcase)
    end

    if results.empty?
      puts "\n❌ No books found matching '#{keyword}'."
    else
      puts "\n🔍 Search Results:"
      results.each { |book| puts book }
    end
  end

  # Delete a book by its ID. Converts provided id to integer and removes matching Book.
  # Persists changes and informs the user of success or failure.
  # Parameter:
  # - id: Integer or String representing the book id
  def delete_book(id)
    book = @books.find { |b| b.id == id.to_i }
    if book
      @books.delete(book)
      save_books
      puts "\n🗑️ Book deleted successfully."
    else
      puts "\n⚠️ Book not found."
    end
  end

  private

  # Persist the current @books array to books.json as an array of hashes.
  # Uses pretty_generate for readable JSON formatting.
  def save_books
    data = @books.map do |b|
      {
        id: b.id,
        title: b.title,
        author: b.author,
        genre: b.genre,
        publication_year: b.publication_year
      }
    end
    File.write('books.json', JSON.pretty_generate(data))
  end

  # Load books from books.json if the file exists.
  # Reconstructs Book instances and updates @next_id to avoid ID collisions.
  def load_books
    if File.exist?('books.json')
      data = JSON.parse(File.read('books.json'))
      data.each do |b|
        @books << Book.new(b["id"], b["title"], b["author"], b["genre"], b["publication_year"])
        @next_id = b["id"].to_i + 1
      end
    end
  end
end

