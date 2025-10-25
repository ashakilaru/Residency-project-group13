# main.rb
require_relative 'catalog_manager'

catalog = CatalogManager.new

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
    catalog.list_books
  when "3"
    print "Enter keyword to search (title, author, or genre): "
    keyword = gets.chomp
    catalog.search_book(keyword)
  when "4"
    print "Enter book ID to delete: "
    id = gets.chomp
    catalog.delete_book(id)
  when "0"
    puts "\n👋 Exiting Book Catalog System. Goodbye!"
    break
  else
    puts "\n⚠️ Invalid choice. Please try again."
  end
end
