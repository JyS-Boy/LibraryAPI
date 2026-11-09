using Library.Business.DTOs.Common;
using Library.Business.DTOs.Request.Book;
using Library.Business.DTOs.Response.Book;
using Library.Business.Interfaces.Services;
using Library.Common.Helpers;
using Library.DataAccess.Entities;
using Library.Repositories.Interfaces;
using Mapster;

namespace Library.Business.Implementations.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository, ICategoryRepository categoryRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Result> CreateAsync(CreateBookRequest request)
        {
            var author =
                await _authorRepository
                    .GetByIdAsync(request.AuthorId);

            if (author is null)
            {
                return Result.Failure(
                    $"Author with id {request.AuthorId} not found.");
            }

            var category =
                await _categoryRepository
                    .GetByIdAsync(request.CategoryId);

            if (category is null)
            {
                return Result.Failure(
                    $"Category with id {request.CategoryId} not found.");
            }

            var entity = new Book(
                request.Title,
                request.ISBN,
                request.Stock,
                request.PublicationYear,
                author,
                category);

            await _bookRepository.CreateAsync(entity);

            return Result.Success();
        }

        public async Task<Result<GetBookResponse>>GetByIdAsync(Guid id)
        {
            var result = await _bookRepository
                .GetByIdAsync(id);

            if (result is null)
            {
                return Result
                    .Failure<GetBookResponse>(
                        $"Book with id {id} not found.");
            }

            return Result.Success(
                result.Adapt<GetBookResponse>());
        }

        public async Task<Result> UpdateAsync(Guid id,UpdateBookRequest request)
        {
            var result = await _bookRepository
                .GetByIdAsync(id);

            if (result is null)
            {
                return Result.Failure(
                    $"Book with id {id} not found.");
            }

            request.Adapt(result);

            await _bookRepository.UpdateAsync(result);

            return Result.Success();
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var result = await _bookRepository
                .GetByIdAsync(id);

            if (result is null)
            {
                return Result.Failure(
                    $"Book with id {id} not found.");
            }

            await _bookRepository.DeleteAsync(id);

            return Result.Success();
        }

        public async Task<
    Result<PagedResponse<ListBooksResponse>>> ListAsync(PagedRequest request)
        {
            var response =
                new PagedResponse<ListBooksResponse>();

            var result =
                await _bookRepository.ListAsync(
                    predicate: b =>
                        b.IsActive &&
                        (
                            string.IsNullOrEmpty(request.Filter)
                            ||
                            b.Title.Contains(request.Filter)
                            ||
                            b.ISBN.Contains(request.Filter)
                        ),
                    selector: b =>
                        b.Adapt<ListBooksResponse>(),
                    orderBy: b => b.Title,
                    pageNumber: request.PageNumber,
                    pageSize: request.PageSize
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
