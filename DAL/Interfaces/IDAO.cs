using Entities;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IDAO
    {
        void AddAuthor(Author author);
        void AddBook(Book book);
        void AddBookToTheAuthor(string name, string surname, Book book);
        void DeleteAuthor(string name, string surname);
        void DeleteBook(string name, int idNumber);
        IEnumerable<Book> FindBooksByAuthorName(string authorName);
        IEnumerable<Book> FindBooksByName(string bookName);
        IEnumerable<Book> FindBooksByYearOfWriting(int writingYear);
        IEnumerable<Book> GetSortedBooks(IEnumerable<Book> books);
     
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Author> GetAllAuthors();

        void UploadFromFile(IEnumerable<Book> books, IEnumerable<Author> authors);
    }
}