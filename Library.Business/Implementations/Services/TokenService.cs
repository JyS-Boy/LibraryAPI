using Library.Business.DTOs.Response.Auth;
using Library.Business.Interfaces.Services;
using Library.Common.Helpers;
using Library.DataAccess.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Library.Business.Implementations.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Result<TokenResponse> GenerateToken (User request)
        {
            //Header
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryMinutes"]));

            //Payload
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.UserName),
                new Claim(ClaimTypes.Role, request.Role),
                new Claim(ClaimTypes.NameIdentifier, request.Id.ToString())
            };

            //Signature
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            return Result.Success(new TokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserName = request.UserName,
                Role = request.Role,
                Expiration = expiration
            });
        }
    }
}
