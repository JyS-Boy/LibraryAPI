namespace Library.Business.DTOs.Response.Auth
{
    public class TokenResponse
    {
        public string Token { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}