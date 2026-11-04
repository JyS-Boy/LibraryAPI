using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DataAccess.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string Country { get; private set; } = string.Empty;
        public List<Book> Books { get; private set; } = new();
        public Author()
        {
        }
        public Author(string name, string country)
        {
            Name = name;
            Country = country;
        }
    }
}
