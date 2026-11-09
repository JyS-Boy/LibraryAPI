using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Business.DTOs.Response.Book
{
    public class GetBookResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int Stock { get; set; }
        public int PublicationYear { get; set; }
        public string Author { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
}
