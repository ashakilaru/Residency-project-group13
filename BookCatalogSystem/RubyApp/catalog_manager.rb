# catalog_manager.rb
require_relative 'book'
require 'json'

puts "Loaded CatalogManager from updated file!"

class CatalogManager
  def initialize
    @books = []
    @next_id = 1
    load_books
  end

  # Add a book or increase stock if the same book exists
  def add_book(title, author, genre, publication_year, quantity = 1)
    existing_book = @books.find do |b|
      b.title.downcase == title.downcase &&
      b.author.downcase == author.downcase &&
      b.genre.downcase == genre.downcase &&
      b.publication_year.to_s == publication_year.to_s
    end

    if existing_book
      existing_book.quantity += quantity.to_i
      puts "\n📚 Book already exists. Increased stock to #{existing_book.quantity}."
    else
      book = Book.new(@next_id, title, author, genre, publication_year, quantity)
      @books << book
      @next_id += 1
      puts "\n✅ New book added successfully!"
    end

    save_books
  end

  # List all books
  def list_books
    if @books.empty?
      puts "\n📭 No books found."
    else
      puts "\n📚 Book Catalog:"
      @books.each { |b| puts b }
    end
  end

  # Search by title, author, or genre
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
      results.each { |b| puts b }
    end
  end

  # Remove a book by title (or reduce quantity)
  def remove_book_by_title(title)
    matching_books = @books.select { |b| b.title.downcase == title.downcase }

    if matching_books.empty?
      puts "\n❌ No book found with that title."
      return
    end

    if matching_books.size > 1
      puts "\nMultiple books found with that title:"
      matching_books.each_with_index do |b, i|
        puts "#{i + 1}. #{b}"
      end
      print "\nEnter the number of the book to remove: "
      choice = gets.chomp.to_i
      selected_book = matching_books[choice - 1]
      remove_book_quantity(selected_book)
    else
      remove_book_quantity(matching_books.first)
    end
  end

  # --- Reporting Methods ---

  # Report books grouped by genre
  def report_by_genre
    if @books.empty?
      puts "\n📭 No books to report."
      return
    end

    report = @books.group_by(&:genre)
    puts "\n📊 Books by Genre:"
    report.each do |genre, books|
      puts "#{genre}: #{books.size} book(s)"
    end
  end

  # Report books grouped by author
  def report_by_author
    if @books.empty?
      puts "\n📭 No books to report."
      return
    end

    report = @books.group_by(&:author)
    puts "\n📊 Books by Author:"
    report.each do |author, books|
      puts "#{author}: #{books.size} book(s)"
    end
  end

  private

  # Handle quantity removal
  def remove_book_quantity(book)
    if book.quantity > 1
      print "This book has #{book.quantity} copies. How many do you want to remove? "
      qty_to_remove = gets.chomp.to_i
      if qty_to_remove >= book.quantity
        @books.delete(book)
        puts "\n🗑️ All copies removed from catalog!"
      elsif qty_to_remove > 0
        book.quantity -= qty_to_remove
        puts "\n📉 #{qty_to_remove} copies removed. Remaining stock: #{book.quantity}"
      else
        puts "\n⚠️ Invalid input. No book removed."
      end
    else
      @books.delete(book)
      puts "\n🗑️ Book removed from catalog!"
    end
    save_books
  end

  # Save books to JSON
  def save_books
    data = @books.map do |b|
      {
        id: b.id,
        title: b.title,
        author: b.author,
        genre: b.genre,
        publication_year: b.publication_year,
        quantity: b.quantity
      }
    end
    File.write('books.json', JSON.pretty_generate(data))
  end

  # Load books from JSON
  def load_books
    return unless File.exist?('books.json')

    data = JSON.parse(File.read('books.json'))
    data.each do |b|
      @books << Book.new(b["id"], b["title"], b["author"], b["genre"], b["publication_year"], b["quantity"])
      @next_id = [@next_id, b["id"].to_i + 1].max
    end
  end
end
