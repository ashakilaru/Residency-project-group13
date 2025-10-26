# main.rb
# Entry point for the Book Catalog System CLI.
# Creates a CatalogManager instance and runs a menu loop to accept user commands.

require_relative 'catalog_manager'

# Initialize catalog manager which loads existing books from books.json
catalog = CatalogManager.new

# Main program loop: display menu, read user choice, and invoke catalog operations.
loop do
  puts "\n=== Book Catalog System ==="
  puts "1. Add a Book"
  puts "2. List All Books"
  puts "3. Search Book by Title, Author, or Genre"
  puts "4. Delete Book by ID"
  puts "0. Exit"
  print "Enter your choice: "
  choice = gets.chomp

  case choice
  when "1"
    # Prompt the user for book details, then add the book to the catalog.
    print "Enter book title: "
    title = gets.chomp
    print "Enter author name: "
    author = gets.chomp
    print "Enter genre: "
    genre = gets.chomp
    print "Enter publication year: "
    publication_year = gets.chomp
    catalog.add_book(title, author, genre, publication_year)
  when "2"
    # Show all books currently stored in the catalog.
    catalog.list_books
  when "3"
    # Prompt for a search keyword and display matching results.
    print "Enter keyword to search (title, author, or genre): "
    keyword = gets.chomp
    catalog.search_book(keyword)
  when "4"
    # Prompt for the book ID and attempt to delete the matching book.
    print "Enter book ID to delete: "
    id = gets.chomp
    catalog.delete_book(id)
  when "0"
    # Exit the application loop gracefully.
    puts "\n👋 Exiting Book Catalog System. Goodbye!"
    break
  else
    # Handle invalid menu choices.
    puts "\n⚠️ Invalid choice. Please try again."
  end
end
