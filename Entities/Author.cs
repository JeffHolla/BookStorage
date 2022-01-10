using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Author
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public int YearOfBirth { get; set; }
        public string City { get; set; }
        public IList<Book> Books { get; set; }
    }
}
