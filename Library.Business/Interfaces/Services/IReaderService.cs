using Library.Business.DTOs.Common;
using Library.Business.DTOs.Request.Reader;
using Library.Business.DTOs.Response.Reader;
using Library.Common.Helpers;

namespace Library.Business.Interfaces.Services
{
    public interface IReaderService
    {
        Task<Result> RegisterAsync(
            RegisterReaderRequest request);

        Task<Result<GetReaderResponse>>
            GetByIdAsync(Guid id);

        Task<Result> UpdateAsync(
            Guid id,
            UpdateReaderRequest request);

        Task<Result> DeleteAsync(Guid id);

        Task<Result<PagedResponse<ListReadersResponse>>>
            ListAsync(PagedRequest request);
    }
}