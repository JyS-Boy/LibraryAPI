
using Library.DataAccess;
using Library.DataAccess.Entities;
using Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories.Implementations;

public class BookRepository : BaseRepository<Book>, IBookRepository
{
    public BookRepository(LibraryDbContext context) : base(context)
    {
    }

    public async Task<List<Book>> GetByListIdsAsync(List<Guid> ids)
    {
        return await _context.Set<Book>()
            .Where(b => 
            ids.Contains(b.Id)
            && b.IsActive)
            .ToListAsync();
    }
}
