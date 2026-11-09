using Library.Business.DTOs.Request.Auth;
using Library.Business.DTOs.Response.Auth;
using Library.Common.Helpers;

namespace Library.Business.Interfaces.Services
{
    public interface IAuthService
    {
        Task<Result<TokenResponse>>LoginAsync(LoginRequest request);
    }
}