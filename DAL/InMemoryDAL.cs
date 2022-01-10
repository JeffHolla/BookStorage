using DAL.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class InMemoryDAL : IDAO
    {
        private List<Book> Books { get; set; }
        private List<Author> Authors { get; set; }

        public InMemoryDAL()
        {
            Books = new List<Book>();
            Authors = new List<Author>();
        }

        // For creating DAL with data
        public InMemoryDAL(IEnumerable<Book> books, IEnumerable<Author> authors)
        {
            Books = books is null ? new List<Book>() : books.ToList();
            Authors = authors is null ? new List<Author>() : authors.ToList();
        }

        public void AddBook(Book book)
        {
            // Check for almost all params of book
            var found = Books.FirstOrDefault(x => x.Name == book.Name &&
                                                  x.YearOfWriting == book.YearOfWriting &&
                                                  x.Author == book.Author &&
                                                  x.IdentificationNumber == book.IdentificationNumber);
            if (found == null)
            {
                Books.Add(book);
            }
            else
            {
                throw new Exception("Such a book has already been added!");
            }
        }

        public void AddAuthor(Author author)
        {
            // Check for all params of author
            var found = Authors.FirstOrDefault(x => x.Name == author.Name &&
                                                    x.Surname == author.Surname &&
                                                    x.YearOfBirth == author.YearOfBirth &&
                                                    x.City == author.City);

            // If nothing is found, then add the Author, because there are no such authors 
            if (found == null)
            {
                Authors.Add(author);
            }
            else
            {
                throw new Exception("Such an author has already been added!");
            }
        }

        public void AddBookToTheAuthor(string name, string surname, Book book)
        {
            var author = Authors.FirstOrDefault(x => x.Name == name && x.Surname == surname);

            // If we found someone, then adding book
            if (author != null)
            {
                author.Books.Add(book);
            }
            else
            {
                throw new Exception("No such author was found!");
            }
        }

        public void DeleteAuthor(string name, string surname)
        {
            var author = Authors.FirstOrDefault(x => x.Name == name && x.Surname == surname);

            // If we found something, then delete the author
            if (author != null)
            {
                Authors.Remove(author);
            }
            else
            {
                throw new Exception("No such author was found!");
            }
        }

        public void DeleteBook(string name, int idNumber)
        {
            var book = Books.FirstOrDefault(x => x.Name == name && x.IdentificationNumber == idNumber);

            // If we found something, then delete the book
            if (book != null)
            {
                Books.Remove(book);
            }
            else
            {
                throw new Exception("No such book was found!");
            }
        }

        public IEnumerable<Book> GetSortedBooks(IEnumerable<Book> books)
        {
            return books.OrderBy(x => x.Author).ThenBy(x => x.Name).ThenBy(x => x.YearOfWriting);
        }

        public IEnumerable<Book> FindBooksByAuthorName(string authorName)
        {
            var books = Authors.FindAll(x => x.Name == authorName).SelectMany(x => x.Books);

            return GetSortedBooks(books);
        }

        public IEnumerable<Book> FindBooksByName(string bookName)
        {
            var books = Books.FindAll(x => x.Name == bookName);

            return GetSortedBooks(books);
        }

        public IEnumerable<Book> FindBooksByYearOfWriting(int writingYear)
        {
            var books = Books.FindAll(x => x.YearOfWriting == writingYear);

            return GetSortedBooks(books);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return Books;
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return Authors;
        }

        public void UploadFromFile(IEnumerable<Book> books, IEnumerable<Author> authors)
        {
            Books = books.ToList();
            Authors = authors.ToList();
        }
    }
}
