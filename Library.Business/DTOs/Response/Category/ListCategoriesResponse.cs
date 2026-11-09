namespace Library.Business.DTOs.Response.Category
{
    public class ListCategoriesResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}