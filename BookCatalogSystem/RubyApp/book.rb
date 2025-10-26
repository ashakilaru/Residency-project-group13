# book.rb
# Represents a single book record in the catalog.
# Each Book instance stores identifying and descriptive attributes
# and provides a readable string representation for display.
class Book
  # Create getter and setter methods for these attributes:
  # - id: unique identifier for the book (Integer or String)
  # - title: book title (String)
  # - author: book author (String)
  # - genre: book genre/category (String)
  # - publication_year: year of publication (Integer or String)
  attr_accessor :id, :title, :author, :genre, :publication_year

  # Initialize a new Book instance.
  # Parameters:
  # - id: unique identifier assigned when the book is created
  # - title: title of the book
  # - author: author of the book
  # - genre: genre or category of the book
  # - publication_year: year the book was published
  def initialize(id, title, author, genre, publication_year)
    @id = id
    @title = title
    @author = author
    @genre = genre
    @publication_year = publication_year
  end

  # Return a human-readable string for displaying the book.
  # This method is useful for printing book details in the CLI.
  def to_s
    "ID: #{@id} | Title: #{@title} | Author: #{@author} | Genre: #{@genre} | Publication: #{@publication_year}"
  end
end
