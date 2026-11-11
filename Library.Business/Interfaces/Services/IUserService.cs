using Library.Business.DTOs.Common;
using Library.Business.DTOs.Request.User;
using Library.Business.DTOs.Response.User;
using Library.Common.Helpers;

namespace Library.Business.Interfaces.Services
{
    public interface IUserService
    {
        Task<Result> CreateAsync(CreateUserRequest request);
        Task<Result<GetUserResponse>>GetByIdAsync(Guid id);
        Task<Result> UpdateAsync(Guid id, UpdateUserRequest request);
        Task<Result> DeleteAsync(Guid id);
        Task<Result<PagedResponse<ListUsersResponse>>>ListAsync(PagedRequest request);
    }
}