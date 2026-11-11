using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DataAccess.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; private set; } = string.Empty;
        public string ISBN { get; private set; } = string.Empty;
        public int Stock { get; private set; }
        public int PublicationYear { get; private set; }
        public Guid AuthorId { get; private set; }
        public Author Author { get; private set; } = new();
        public Guid CategoryId { get; private set; }
        public Category? Category { get; private set; } = new();
        public List<LoanDetail> LoanDetails { get; private set; } = new();
        public Book()
        {
        }
        public Book(string title, string isbn, int stock, int publicationYear, Author author, Category category)
        {
            Title = title;
            ISBN = isbn;
            Stock = stock;
            PublicationYear = publicationYear;
            Author = author;
            AuthorId = author.Id;
            Category = category;
            CategoryId = category.Id;
        }
        public void DecreaseStock(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Cantidad debe ser mayor que cero.", nameof(quantity));
            }
            if (Stock < quantity)
            {
                throw new InvalidOperationException("Stock insuficiente para disminuir.");
            }
            Stock -= quantity;
        }
        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Cantidad debe ser mayor que cero.", nameof(quantity));
            }
            Stock += quantity;
        }
    }
}
