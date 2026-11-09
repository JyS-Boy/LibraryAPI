using Library.Business.DTOs.Common;
using Library.Business.DTOs.Request.Author;
using Library.Business.DTOs.Response.Author;
using Library.Business.Interfaces.Services;
using Library.Common.Helpers;
using Library.DataAccess.Entities;
using Library.Repositories.Interfaces;
using Mapster;

namespace Library.Business.Implementations.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;

        public AuthorService(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> CreateAsync(CreateAuthorRequest request)
        {
            var author = request.Adapt<Author>();

            await _repository.CreateAsync(author);

            return Result.Success();
        }

        public async Task<Result<GetAuthorResponse>>
            GetByIdAsync(Guid id)
        {
            var result = await _repository
                .GetByIdAsync(id);

            if (result is null)
            {
                return Result
                    .Failure<GetAuthorResponse>(
                        $"Author with id {id} not found.");
            }

            return Result
                .Success(
                    result.Adapt<GetAuthorResponse>());
        }

        public async Task<Result> UpdateAsync(
            Guid id,
            UpdateAuthorRequest request)
        {
            var result = await _repository
                .GetByIdAsync(id);

            if (result is null)
            {
                return Result.Failure(
                    $"Author with id {id} not found.");
            }

            request.Adapt(result);

            await _repository.UpdateAsync(result);

            return Result.Success();
        }

        public async Task<Result> DeleteAsync(
            Guid id)
        {
            var result = await _repository
                .GetByIdAsync(id);

            if (result is null)
            {
                return Result.Failure(
                    $"Author with id {id} not found.");
            }

            await _repository.DeleteAsync(id);

            return Result.Success();
        }

        public async Task<
            Result<PagedResponse<ListAuthorsResponse>>
        > ListAsync(
            PagedRequest request)
        {
            var response =
                new PagedResponse<ListAuthorsResponse>();

            var result =
                await _repository.ListAsync(
                    predicate: a =>
                        a.IsActive &&
                        (
                            string.IsNullOrEmpty(
                                request.Filter)
                            ||
                            a.Name.Contains(
                                request.Filter)
                            ||
                            a.Country.Contains(
                                request.Filter)
                        ),
                    selector: a =>
                        a.Adapt<ListAuthorsResponse>(),
                    orderBy: a => a.Name,
                    pageNumber:
                        request.PageNumber,
                    pageSize:
                        request.PageSize
                );

            response.Collection =
                result.Result.ToList();

            response.TotalCount =
                result.TotalCount;

            response.TotalRowsPerPage =
                result.Result.Count;

            response.TotalPages =
                (int)Math.Ceiling(
                    (double)result.TotalCount /
                    request.PageSize);

            return Result.Success(response);
        }
    }
}