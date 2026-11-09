
namespace Library.Business.DTOs.Request.Book
{
    public class UpdateBookRequest
    {
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int Stock { get; set; }
        public int PublicationYear { get; set; }
        public Guid AuthorId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
