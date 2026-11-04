using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DataAccess.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public List<Book> Books { get; private set; } = new();
        public Category()
        {
        }
        public Category(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
