using Library.Business.DTOs.Common;
using Library.Business.DTOs.Request.User;
using Library.Business.DTOs.Response.User;
using Library.Business.Interfaces.Services;
using Library.Common.Helpers;
using Library.DataAccess.Entities;
using Library.Repositories.Interfaces;
using Mapster;


namespace Library.Business.Implementations.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> CreateAsync(CreateUserRequest request)
        {
            var exists = await _repository.GetByUserNameAsync(request.UserName);

            if (exists is not null)
            {
                return Result.Failure(
                    $"The username '{request.UserName}' already exists.");
            }

            var hash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User(
                request.UserName,
                hash,
                request.Role);

            await _repository.CreateAsync(user);

            return Result.Success();
        }

        public async Task<Result<GetUserResponse>> GetByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);

            if (result is null)
                return Result.Failure<GetUserResponse>($"User with id {id} not found.");

            return Result.Success(result.Adapt<GetUserResponse>());
        }

        public async Task<Result> UpdateAsync(Guid id, UpdateUserRequest request)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user is null)
                return Result.Failure($"User with id {id} not found.");

            var exists = await _repository.GetByUserNameAsync(request.UserName);

            if (exists is not null && exists.Id != id)
                return Result.Failure($"The username '{request.UserName}' already exists.");

            request.Adapt(user);

            await _repository.UpdateAsync(user);

            return Result.Success();
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user is null)
                return Result.Failure($"User with id {id} not found.");

            await _repository.DeleteAsync(id);

            return Result.Success();
        }

        public async Task<Result<PagedResponse<ListUsersResponse>>> ListAsync(PagedRequest request)
        {
            var response = new PagedResponse<ListUsersResponse>();

            var result = await _repository.ListAsync(predicate: u => u.IsActive &&
                        (string.IsNullOrEmpty(request.Filter) || u.UserName.Contains(request.Filter)),
                    selector: u => u.Adapt<ListUsersResponse>(),
                    orderBy: u => u.UserName,
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
