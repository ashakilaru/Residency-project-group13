# book.rb
class Book
  attr_accessor :id, :title, :author, :genre, :publication_year

  def initialize(id, title, author, genre, publication_year)
    @id = id
    @title = title
    @author = author
    @genre = genre
    @publication_year = publication_year
  end

  def to_s
    "ID: #{@id} | Title: #{@title} | Author: #{@author} | Genre: #{@genre} | Publication: #{@publication_year}"
  end
end
