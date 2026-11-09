using Library.Business.DTOs.Common;
using Library.Business.DTOs.Request.Reader;
using Library.Business.DTOs.Response.Reader;
using Library.Business.Interfaces.Services;
using Library.Common.Helpers;
using Library.DataAccess.Entities;
using Library.Repositories.Interfaces;
using Mapster;

namespace Library.Business.Implementations.Services
{
    public class ReaderService : IReaderService
    {
        private readonly IReaderRepository _repository;

        public ReaderService(IReaderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> RegisterAsync(RegisterReaderRequest request)
        {
            var emailExists =
                await _repository.GetByPredicateAsync(
                    r => r.Email == request.Email);

            if (emailExists != null)
            {
                return Result.Failure(
                    $"The email {request.Email} is already being used.");
            }

            var reader = request.Adapt<Reader>();

            await _repository.CreateAsync(reader);

            return Result.Success(
                "Reader registered successfully.");
        }

        public async Task<Result<GetReaderResponse>>GetByIdAsync(Guid id)
        {
            var result =
                await _repository.GetByIdAsync(id);

            if (result is null)
            {
                return Result
                    .Failure<GetReaderResponse>(
                        $"Reader with id {id} not found.");
            }

            return Result.Success(
                result.Adapt<GetReaderResponse>());
        }

        public async Task<Result> UpdateAsync(Guid id, UpdateReaderRequest request)
        {
            var reader =
                await _repository.GetByIdAsync(id);

            if (reader is null)
            {
                return Result.Failure(
                    $"Reader with id {id} not found.");
            }

            var emailExists =
                await _repository.GetByPredicateAsync(
                    r => r.Email == request.Email &&
                         r.Id != id);

            if (emailExists != null)
            {
                return Result.Failure(
                    $"The email {request.Email} is already being used.");
            }

            request.Adapt(reader);

            await _repository.UpdateAsync(reader);

            return Result.Success();
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var reader =
                await _repository.GetByIdAsync(id);

            if (reader is null)
            {
                return Result.Failure(
                    $"Reader with id {id} not found.");
            }

            await _repository.DeleteAsync(id);

            return Result.Success();
        }

        public async Task<Result<PagedResponse<ListReadersResponse>>> ListAsync(PagedRequest request)
        {
            var response = new PagedResponse<ListReadersResponse>();

            var result =
                await _repository.ListAsync(
                    predicate: r =>
                        r.IsActive &&
                        (
                            string.IsNullOrEmpty(request.Filter)
                            ||
                            r.FullName.Contains(request.Filter)
                            ||
                            r.Email.Contains(request.Filter)
                        ),
                    selector: r =>
                        r.Adapt<ListReadersResponse>(),
                    orderBy: r => r.FullName,
                    pageNumber: request.PageNumber,
                    pageSize: request.PageSize
                );

            response.Collection = result.Result.ToList();

            response.TotalCount = result.TotalCount;

            response.TotalRowsPerPage = result.Result.Count;

            response.TotalPages = (int)Math.Ceiling((double)result.TotalCount/request.PageSize);

            return Result.Success(response);
        }
    }
}
