using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    // Using for saving entities more easily
    public class JsonContainer
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Author> Authors { get; set; }
    }
}
