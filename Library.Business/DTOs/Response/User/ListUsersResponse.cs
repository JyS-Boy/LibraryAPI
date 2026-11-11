namespace Library.Business.DTOs.Response.User
{
    public class ListUsersResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}