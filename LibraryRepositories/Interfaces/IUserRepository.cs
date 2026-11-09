using Library.DataAccess.Entities;

namespace Library.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByUserNameAsync(string userName);
    }
}
