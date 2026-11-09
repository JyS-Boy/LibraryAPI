using Library.Business.DTOs.Response.Auth;
using Library.Common.Helpers;
using Library.DataAccess.Entities;

namespace Library.Business.Interfaces.Services
{
    public interface ITokenService
    {
        Result<TokenResponse> GenerateToken(User request);
    }
}