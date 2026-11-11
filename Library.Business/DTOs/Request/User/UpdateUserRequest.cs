namespace Library.Business.DTOs.Request.User
{
    public class UpdateUserRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}