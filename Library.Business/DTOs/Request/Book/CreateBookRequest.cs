using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Business.DTOs.Request.Book
{
    public class CreateBookRequest
    {
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int Stock { get; set; }
        public int PublicationYear { get; set; }
        public Guid AuthorId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
