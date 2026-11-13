using Library.Business.DTOs.Common;
using Library.Business.DTOs.Request.Category;
using Library.Business.DTOs.Response.Category;
using Library.Common.Helpers;

namespace Library.Business.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<Result> CreateAsync(CreateCategoryRequest request);
        Task<Result<GetCategoryResponse>> GetByIdAsync(Guid id);
        Task<Result> UpdateAsync(Guid id,UpdateCategoryRequest request);
        Task<Result> DeleteAsync(Guid id);
        Task<Result<PagedResponse<ListCategoriesResponse>>>ListAsync(PagedRequest request);
    }
}