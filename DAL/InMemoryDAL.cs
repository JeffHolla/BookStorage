using DAL.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class InMemoryDAL : IDAO
    {
        public List<Book> Books { get; set; }
        public List<Author> Authors { get; set; }

        public InMemoryDAL()
        {
            Books = new List<Book>();
            Authors = new List<Author>();
        }

        public InMemoryDAL(IEnumerable<Book> books, IEnumerable<Author> authors)
        {
            Books = books is null ? new List<Book>() : books.ToList();
            Authors = authors is null ? new List<Author>() : authors.ToList();
        }

        public void AddBook(Book book)
        {
            Books.Add(book);
        }

        public void AddAuthor(Author author)
        {
            Authors.Add(author);
        }

        public void AddBookToTheAuthor(string name, string surname, Book book)
        {
            var author = Authors.FirstOrDefault(x => x.Name == name && x.Surname == surname);

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
