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
            // In order not to show the exception to the user
            try
            {
                _DAO.AddAuthor(author);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AddBook(Book book)
        {
            // In order not to show the exception to the user
            try
            {
                _DAO.AddBook(book);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AddBookToTheAuthor(string name, string surname, Book book)
        {
            // In order not to show the exception to the user
            try
            {
                _DAO.AddBookToTheAuthor(name, surname, book);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteAuthor(string name, string surname)
        {
            // In order not to show the exception to the user
            try
            {
                _DAO.DeleteAuthor(name, surname);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteBook(string name, int idNumber)
        {
            // In order not to show the exception to the user
            try
            {
                _DAO.DeleteBook(name, idNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
