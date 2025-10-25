# catalog_manager.rb
require_relative 'book'
require 'json'

class CatalogManager
  def initialize
    @books = []
    @next_id = 1
    load_books
  end

  def add_book(title, author, genre, publication_year)
    book = Book.new(@next_id, title, author, genre, publication_year)
    @books << book
    @next_id += 1
    save_books
    puts "\n✅ Book added successfully!"
  end

  def list_books
    if @books.empty?
      puts "\n📭 No books found."
    else
      puts "\n📚 Book Catalog:"
      @books.each { |book| puts book }
    end
  end

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

  def save_books
    data = @books.map { |b| {id: b.id, title: b.title, author: b.author, genre: b.genre, publication_year: b.publication_year} }
    File.write('books.json', JSON.pretty_generate(data))
  end

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
