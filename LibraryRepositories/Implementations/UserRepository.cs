using Library.DataAccess;
using Library.DataAccess.Entities;
using Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories.Implementations;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(LibraryDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        return await _context.Set<User>()
            .FirstOrDefaultAsync(x =>
                x.UserName == userName
                && x.IsActive);
    }
}
