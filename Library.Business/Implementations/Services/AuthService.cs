using Library.Business.DTOs.Request.Auth;
using Library.Business.DTOs.Response.Auth;
using Library.Business.Interfaces.Services;
using Library.Common.Helpers;
using Library.Repositories.Interfaces;

namespace Library.Business.Implementations.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<Result<TokenResponse>> LoginAsync(LoginRequest request)
        {
            // Validación de credenciales
            var user = await _userRepository.GetByPredicateAsync(p => p.UserName == request.UserName);
            if (user is null)
                return Result.Failure<TokenResponse>("Usuario no encontrado.");

            var ValidPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!ValidPassword)
                return Result.Failure<TokenResponse>("Contraseña inválida.");
            
            //Generación del token JWT
            var token = _tokenService.GenerateToken(user);

            if (token.IsFailure)
                return Result.Failure<TokenResponse>(token.Message!);
          
            return Result.Success(token.Value!);
        }
    }
}
