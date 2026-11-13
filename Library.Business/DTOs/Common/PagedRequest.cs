namespace Library.Business.DTOs.Common
{
    public class PagedRequest
    {
        public string? Filter { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PagedRequest()
        {
            PageNumber = 1;
            PageSize = 10;
        }
    }
}
