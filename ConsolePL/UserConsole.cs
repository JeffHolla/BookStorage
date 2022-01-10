using DependenciesResolver;
using Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace ConsolePL
{
    public class UserConsole
    {
        private BLL.SimpleBLL _BLL;

        // Path to local save file with data
        private const string PathToBookStorageFile = @"../../../BookStorageFile.json";

        public UserConsole()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.ForegroundColor = ConsoleColor.White;

            _BLL = DR.BLL;
        }

        public void Run()
        {
            // Load data from local file on load program
            OnProgramStart();

            // Main menu 
            ShowMenus();

            // Save data from local file on load program
            OnProgramExit();
        }

        private void ShowMenus()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("Выберите пункт меню:");
                Console.WriteLine(@"
1. Меню Добавить
2. Меню Удалить
3. Меню Отобразить/Найти
0. Выход из приложения");

                string command = Console.ReadLine();
                switch (command.Trim())
                {
                    case "1":
                        AddMenu();
                        break;

                    case "2":
                        DeleteMenu();
                        break;

                    case "3":
                        ShowMenu();
                        break;
                    case "0":
                        OnProgramExit();
                        return;
                }

                Console.ReadKey();
            }
        }

        #region AddRegion
        private void AddMenu()
        {
            string command;
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== AddMenu =====");
                Console.WriteLine("Выберите пункт меню:");
                Console.WriteLine(@"
1. Добавить Книгу
2. Добавить Автора
3. Добавить Книгу к существующему автору
0. Вернуться к выбору действий");

                command = Console.ReadLine();

                switch (command.Trim())
                {
                    case "1":
                        AddBook();
                        break;

                    case "2":
                        AddAuthor();
                        break;

                    case "3":
                        AddBookToTheAuthor();
                        break;

                    case "0":
                        return;
                }

                Console.WriteLine("Нажмите любую кнопку для продолжения");
                Console.ReadKey();
            }
        }

        // Method, that helps concat all props of Book and create new one
        private Book AddBook()
        {
            Console.WriteLine("Введите название:");
            string name = Console.ReadLine().Trim();
            if (!IsValidString(name))
            {
                return null;
            }

            Console.WriteLine("Введите имя автора:");
            string author = Console.ReadLine().Trim();
            if (!IsValidString(author))
            {
                return null;
            }

            int yearOfWriting;
            while (true)
            {
                Console.WriteLine("Введите год написания:");
                yearOfWriting = InputInt();

                if (yearOfWriting <= 2022)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Год написания не может быть больше, чем текущая дата!");
                    Console.WriteLine();
                }
            }

            int identNumber;
            while (true)
            {
                Console.WriteLine("Введите идентификационный номер:");
                identNumber = InputInt();

                if (identNumber >= 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("идентификационный номер не может быть меньше нуля!");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Введите описание:");
            string description = Console.ReadLine().Trim();
            if (!IsValidString(description))
            {
                return null;
            }

            Book book = new Book()
            {
                Author = author,
                Description = description,
                IdentificationNumber = identNumber,
                Name = name,
                YearOfWriting = yearOfWriting
            };

            _BLL.AddBook(book);
            return book;
        }

        // Method, that helps concat all props of Author and create new one
        private void AddAuthor()
        {
            Console.WriteLine("Введите имя:");
            string name = Console.ReadLine().Trim();
            if (!IsValidString(name))
            {
                return;
            }
            
            Console.WriteLine("Введите фамилию:");
            string surname = Console.ReadLine().Trim();
            if (!IsValidString(surname))
            {
                return;
            }

            int yearOfBirth;
            while (true)
            {
                Console.WriteLine("Введите год рождения:");
                yearOfBirth = InputInt();

                if (yearOfBirth <= 2022 && yearOfBirth >= 1900)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Год рождения не может быть больше, чем 2022 и не может быть меньше, чем 1900!");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Введите город:");
            string city = Console.ReadLine().Trim();
            if (!IsValidString(city))
            {
                return;
            }

            Author author = new Author()
            {
                Name = name,
                Surname = surname,
                Books = new List<Book>(),
                City = city,
                YearOfBirth = yearOfBirth
            };

            _BLL.AddAuthor(author);
        }

        private void AddBookToTheAuthor()
        {
            string command;
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== AddMenu =====");
                Console.WriteLine("Выберите пункт меню:");
                Console.WriteLine(@"
1. Добавить существующую книгу к автору
2. Добавить новую книгу к автору
0. Вернуться к выбору действий");

                command = Console.ReadLine();

                switch (command.Trim())
                {
                    // Show all authors and books. Then match them and adding book to the choosen author
                    case "1":
                        int counter = 0;
                        Console.Clear();
                        var author = ChooseAuthor();

                        Console.WriteLine();
                        Console.WriteLine("Выберите и ведите номер книги");
                        Dictionary<int, Book> posBooks = new Dictionary<int, Book>();

                        foreach (var bookEntity in _BLL.GetAllBooks())
                        {
                            Console.WriteLine($"{counter}: {bookEntity.Name} {bookEntity.IdentificationNumber}");

                            posBooks.Add(counter, bookEntity);
                            counter++;
                        }

                        int parsedBook = InputInt();
                        if (!posBooks.ContainsKey(parsedBook))
                        {
                            Console.WriteLine("Введено неверное число для списка!");
                            break;
                        }

                        if (IsToChangeBookAuthorFromBook(posBooks[parsedBook], author.Name))
                        {
                            posBooks[parsedBook].Author = author.Name;
                        }

                        _BLL.AddBookToTheAuthor(author.Name, author.Surname, posBooks[parsedBook]);
                        break;

                    // Show all authors, but create a new book. Then match them and adding book to the choosen author
                    case "2":
                        var newBook = AddBook();
                        if (newBook is null)
                        {
                            break;
                        }

                        var authorChoosed = ChooseAuthor();

                        if (IsToChangeBookAuthorFromBook(newBook, authorChoosed.Name))
                        {
                            newBook.Author = authorChoosed.Name;
                        }

                        _BLL.AddBookToTheAuthor(authorChoosed.Name, authorChoosed.Surname, newBook);

                        break;

                    case "0":
                        return;
                }

            }
        }

        // Check that string is valid. May be added in future with many other checks
        private bool IsValidString(string justAString)
        {
            if (justAString == string.Empty)
            {
                Console.WriteLine("Строка не может быть пустой!");
                return false;
            }

            return true;
        }

        // Method-helper. Help to show like list all authors
        private Author ChooseAuthor()
        {
            Console.WriteLine("Выберите и ведите номер автора");
            int counter = 0;
            Dictionary<int, Author> posAuthors = new Dictionary<int, Author>();

            foreach (var author in _BLL.GetAllAuthors())
            {
                Console.WriteLine($"{counter}: {author.Name} {author.Surname}");

                posAuthors.Add(counter, author);
                counter++;
            }

            int parsedAuthor = InputInt();
            if (!posAuthors.ContainsKey(parsedAuthor))
            {
                Console.WriteLine("Введено неверное число для списка!");
                return null;
            }

            return posAuthors[parsedAuthor];
        }

        // Check that Author in Book is equivalent to the author name
        private bool IsToChangeBookAuthorFromBook(Book book, string authorName)
        {
            if (book.Author != authorName)
            {
                Console.Clear();

                Console.WriteLine("Имя выбранного автора и имя автора в книге не совпадают!");
                Console.WriteLine("Хотите ли вы сделать имя автора в книге такое же, как имя выбранного автора?");

                string command;
                while (true)
                {
                    Console.WriteLine(@"
1. Да
2. Нет");
                    command = Console.ReadLine();
                    switch (command)
                    {
                        case "1":
                            return true;
                        case "2":
                            return false;

                        default:
                            Console.WriteLine("Введите правильное число - 1 или 2");
                            break;
                    }
                    Console.WriteLine();
                }
            }

            return false;
        }

        // Method-Helper. Helps to correct parse int
        private int InputInt()
        {
            while (true)
            {
                Console.WriteLine("Введите число:");

                string userString = Console.ReadLine();
                if (int.TryParse(userString, out int number))
                {
                    return number;
                }
                else
                {
                    Console.WriteLine("Число не распознано! Введите ещё раз");
                    Console.WriteLine();
                }
            }
        }
        #endregion

        #region DeleteRegion
        private void DeleteMenu()
        {
            string command;
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== DeleteMenu =====");
                Console.WriteLine("Выберите пункт меню:");
                Console.WriteLine(@"
1. Удалить Книгу
2. Удалить Автора
0. Вернуться к выбору действий");

                command = Console.ReadLine();

                switch (command.Trim())
                {
                    case "1":
                        DeleteBook();
                        break;

                    case "2":
                        DeleteAuthor();
                        break;

                    case "0":
                        return;
                }

                Console.WriteLine("Нажмите любую кнопку для продолжения");
                Console.ReadKey();
            }
        }

        private void DeleteAuthor()
        {
            Console.WriteLine("Выберите и ведите номер автора");
            int counter = 0;
            Dictionary<int, Author> posAuthors = new Dictionary<int, Author>();

            foreach (var author in _BLL.GetAllAuthors())
            {
                Console.WriteLine($"{counter}: {author.Name} {author.Surname}");

                posAuthors.Add(counter, author);
                counter++;
            }

            int parsedAuthor = InputInt();
            if (!posAuthors.ContainsKey(parsedAuthor))
            {
                Console.WriteLine("Введено неверное число для списка!");
                return;
            }

            _BLL.DeleteAuthor(posAuthors[parsedAuthor].Name, posAuthors[parsedAuthor].Surname);
        }

        private void DeleteBook()
        {
            int counter = 0;
            Console.Clear();
            Console.WriteLine("Выберите и ведите номер книги");
            Dictionary<int, Book> posBooks = new Dictionary<int, Book>();

            foreach (var bookEntity in _BLL.GetAllBooks())
            {
                Console.WriteLine($"{counter}: {bookEntity.Name} {bookEntity.IdentificationNumber}");

                posBooks.Add(counter, bookEntity);
                counter++;
            }

            int parsedBook = InputInt();
            if (!posBooks.ContainsKey(parsedBook))
            {
                Console.WriteLine("Введено неверное число для списка!");
                return;
            }

            _BLL.DeleteBook(posBooks[parsedBook].Name, posBooks[parsedBook].IdentificationNumber);
        }

        #endregion

        #region ShowRegion
        private void ShowMenu()
        {
            string command;
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== ShowSearchMenu =====");
                Console.WriteLine("Выберите пункт меню:");
                Console.WriteLine(@"
1. Вывести все книги
2. Вывести всех авторов
3. Найти книгу по имени автора
4. Найти книгу по названию
5. Найти книгу по году написания
0. Вернуться к выбору действий");

                command = Console.ReadLine();

                // In all cases just getting data and showing it
                switch (command.Trim())
                {
                    case "1":
                        foreach (var book in _BLL.GetSortedBooks(_BLL.GetAllBooks()))
                        {
                            Console.WriteLine($"Name - {book.Name} | Author - {book.Author} | ID - {book.IdentificationNumber}");
                        }

                        break;

                    case "2":
                        foreach (var author in _BLL.GetAllAuthors())
                        {
                            Console.WriteLine($"{author.Name} - {author.Surname} - {author.YearOfBirth}");
                            if (author.Books.Any())
                            {
                                foreach (var book in author.Books)
                                {
                                    Console.WriteLine($"-- Name - {book.Name} | YearOfWriting - {book.YearOfWriting} | ID - {book.IdentificationNumber}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("-- No books --");
                            }
                        }

                        break;

                    case "3":
                        Console.WriteLine("Введите имя автора для поиска");
                        string authorName = Console.ReadLine().Trim();
                        foreach (var book in _BLL.FindBooksByAuthorName(authorName))
                        {
                            Console.WriteLine($"-- Name - {book.Name} | Author - {book.Author} | YearOfWriting - {book.YearOfWriting} | ID - {book.IdentificationNumber}");
                        }

                        break;

                    case "4":
                        Console.WriteLine("Введите название книги для поиска");
                        string bookName = Console.ReadLine().Trim();
                        foreach (var book in _BLL.FindBooksByName(bookName))
                        {
                            Console.WriteLine($"-- Name - {book.Name} | Author - {book.Author} | YearOfWriting - {book.YearOfWriting} | ID - {book.IdentificationNumber}");
                        }

                        break;

                    case "5":
                        Console.WriteLine("Введите имя автора для поиска");
                        int yearOfWriting = InputInt();
                        foreach (var book in _BLL.FindBooksByYearOfWriting(yearOfWriting))
                        {
                            Console.WriteLine($"-- Name - {book.Name} | Author - {book.Author} | YearOfWriting - {book.YearOfWriting} | ID - {book.IdentificationNumber}");
                        }

                        break;

                    case "0":
                        Console.WriteLine("Нажмите любую кнопку для продолжения");
                        return;
                }

                Console.WriteLine("Нажмите любую кнопку для продолжения");
                Console.ReadKey();
            }
        }

        #endregion

        private void OnProgramStart()
        {
            // Read data from file on start program in special container,
            // that contains 2 props - for easialy serialization and deserialization
            var jsonString = File.ReadAllText(PathToBookStorageFile);
            var jsonContainerData = JsonSerializer.Deserialize<JsonContainer>(jsonString);

            _BLL.UploadFromFile(jsonContainerData.Books, jsonContainerData.Authors);
        }

        private void OnProgramExit()
        {
            // Write data to file on exit program. Create instance of special container,
            // that contains 2 props - for easialy serialization and deserialization
            JsonContainer jsonContainer = new JsonContainer()
            {
                Books = _BLL.GetAllBooks(),
                Authors = _BLL.GetAllAuthors()
            };

            var containerSerialized = JsonSerializer.Serialize(jsonContainer);

            File.WriteAllText(PathToBookStorageFile, containerSerialized);
        }
    }
}