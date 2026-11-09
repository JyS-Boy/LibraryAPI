using Library.DataAccess;
using Library.DataAccess.Entities;
using Library.Repositories.Interfaces;

namespace Library.Repositories.Implementations;

public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
{
    public AuthorRepository(LibraryDbContext context) : base(context)
    {
    }
}