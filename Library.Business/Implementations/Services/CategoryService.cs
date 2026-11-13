using Library.Business.DTOs.Common;
using Library.Business.DTOs.Request.Category;
using Library.Business.DTOs.Response.Category;
using Library.Business.Interfaces.Services;
using Library.Common.Helpers;
using Library.DataAccess.Entities;
using Library.Repositories.Interfaces;
using Mapster;

namespace Library.Business.Implementations.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> CreateAsync(CreateCategoryRequest request)
        {
            var category = request.Adapt<Category>();

            await _repository.CreateAsync(category);

            return Result.Success();
        }

        public async Task<Result<GetCategoryResponse>>GetByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);

            if (result is null)
                return Result.Failure<GetCategoryResponse>($"Categoria con id {id} no encontrado");

            return Result.Success(result.Adapt<GetCategoryResponse>());
        }

        public async Task<Result> UpdateAsync(Guid id,UpdateCategoryRequest request)
        {
            var result = await _repository.GetByIdAsync(id);

            if (result is null)
                return Result.Failure($"Categoria con id {id} no encontrado");

            request.Adapt(result);

            await _repository.UpdateAsync(result);

            return Result.Success();
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);

            if (result is null)
                return Result.Failure($"Category with id {id} not found.");

            await _repository.DeleteAsync(id);

            return Result.Success();
        }

        public async Task<Result<PagedResponse<ListCategoriesResponse>>> ListAsync(PagedRequest request)
        {
            var response = new PagedResponse<ListCategoriesResponse>();

            var result = await _repository.ListAsync(
                    predicate: c => c.IsActive &&
                        (string.IsNullOrEmpty(request.Filter)
                            || c.Name.Contains(request.Filter)
                            || c.Description.Contains(request.Filter)
                        ),
                    selector: c => c.Adapt<ListCategoriesResponse>(),
                    orderBy: c => c.Name,
                    pageNumber: request.PageNumber,
                    pageSize: request.PageSize
                );

            response.Collection = result.Result.ToList();

            response.TotalCount = result.TotalCount;

            response.TotalRowsPerPage = result.Result.Count;

            response.TotalPages = (int)Math.Ceiling((double)result.TotalCount / request.PageSize);

            return Result.Success(response);
        }
    }
}
