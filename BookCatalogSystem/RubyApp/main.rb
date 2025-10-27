require_relative 'catalog_manager'

catalog = CatalogManager.new

loop do
  puts "\n=== Book Catalog System ==="
  puts "1. Add a Book"
  puts "2. List All Books"
  puts "3. Search Book by Title, Author, or Genre"
  puts "4. Remove Book by Title"
  puts "5. Report by Genre"
  puts "6. Report by Author"
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
    year = gets.chomp
    print "Enter quantity (default 1): "
    qty_input = gets.chomp
    quantity = qty_input.empty? ? 1 : qty_input.to_i
    catalog.add_book(title, author, genre, year, quantity)
  when "2"
    catalog.list_books
  when "3"
    print "Enter keyword to search (title, author, or genre): "
    keyword = gets.chomp
    catalog.search_book(keyword)
  when "4"
    print "Enter book title to remove: "
    title = gets.chomp
    catalog.remove_book_by_title(title)
  when "5"
    catalog.report_by_genre
  when "6"
    catalog.report_by_author  
  when "0"
    puts "\n Exiting Book Catalog System. Goodbye!"
    break
  else
    puts "\n Invalid choice. Please try again."
  end
end
