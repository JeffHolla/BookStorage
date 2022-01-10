using BLL.Interfaces;
using DAL.Interfaces;
using Entities;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class SimpleBLL : IBLL
    {
        public IDAO _DAO;

        public SimpleBLL(IDAO dao)
        {
            _DAO = dao;
        }

        public void AddAuthor(Author author)
        {
            _DAO.AddAuthor(author);
        }

        public void AddBook(Book book)
        {
            _DAO.AddBook(book);
        }

        public void AddBookToTheAuthor(string name, string surname, Book book)
        {
            _DAO.AddBookToTheAuthor(name, surname, book);
        }

        public void DeleteAuthor(string name, string surname)
        {
            _DAO.DeleteAuthor(name, surname);
        }

        public void DeleteBook(string name, int idNumber)
        {
            _DAO.DeleteBook(name, idNumber);
        }

        public IEnumerable<Book> FindBooksByAuthorName(string authorName)
        {
            return _DAO.FindBooksByAuthorName(authorName);
        }

        public IEnumerable<Book> FindBooksByName(string bookName)
        {
            return _DAO.FindBooksByName(bookName);
        }

        public IEnumerable<Book> FindBooksByYearOfWriting(int writingYear)
        {
            return _DAO.FindBooksByYearOfWriting(writingYear);
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _DAO.GetAllAuthors();
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _DAO.GetAllBooks();
        }

        public IEnumerable<Book> GetSortedBooks(IEnumerable<Book> books)
        {
            return _DAO.GetSortedBooks(books);
        }

        public void UploadFromFile(IEnumerable<Book> books, IEnumerable<Author> authors)
        {
            _DAO.UploadFromFile(books, authors);
        }
    }
}
