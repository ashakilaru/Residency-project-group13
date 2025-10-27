# book.rb
# Represents a single book record in the catalog.

class Book
  attr_accessor :id, :title, :author, :genre, :publication_year, :quantity

  # Initialize a new Book instance
  def initialize(id, title, author, genre, publication_year, quantity = 1)
    @id = id
    @title = title
    @author = author
    @genre = genre
    @publication_year = publication_year
    @quantity = [quantity.to_i, 1].max  # Ensure at least 1 copy
  end

  # Human-readable string representation
  def to_s
    "ID: #{@id} | Title: #{@title} | Author: #{@author} | Genre: #{@genre} | Publication: #{@publication_year} | Stock: #{@quantity}"
  end
end
