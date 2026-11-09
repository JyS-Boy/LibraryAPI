using Library.Business.DTOs.Common;
using Library.Business.DTOs.Request.Author;
using Library.Business.DTOs.Response.Author;
using Library.Common.Helpers;

namespace Library.Business.Interfaces.Services;

public interface IAuthorService
{
    Task<Result> CreateAsync(CreateAuthorRequest request);

    Task<Result<GetAuthorResponse>> GetByIdAsync(Guid id);

    Task<Result> UpdateAsync(
        Guid id,
        UpdateAuthorRequest request);

    Task<Result> DeleteAsync(Guid id);

    Task<Result<PagedResponse<ListAuthorsResponse>>>
        ListAsync(PagedRequest request);
}