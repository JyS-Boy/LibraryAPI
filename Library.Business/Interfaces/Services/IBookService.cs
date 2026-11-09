using Library.Business.DTOs.Common;
using Library.Business.DTOs.Request.Book;
using Library.Business.DTOs.Response.Book;
using Library.Common.Helpers;


namespace Library.Business.Interfaces.Services
{
    public interface IBookService
    {
        Task<Result> CreateAsync(CreateBookRequest request);
        Task<Result<GetBookResponse>> GetByIdAsync(Guid id);
        Task<Result> UpdateAsync(Guid id,UpdateBookRequest request);
        Task<Result> DeleteAsync(Guid id);
        Task<Result<PagedResponse<ListBooksResponse>>> ListAsync(PagedRequest request);
    }
}
