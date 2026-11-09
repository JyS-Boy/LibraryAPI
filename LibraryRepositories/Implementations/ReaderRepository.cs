using Library.DataAccess;
using Library.DataAccess.Entities;
using Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories.Implementations;

public class ReaderRepository : BaseRepository<Reader>, IReaderRepository
{
    public ReaderRepository(LibraryDbContext context) : base(context)
    {
    }
}
