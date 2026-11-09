using Library.DataAccess.Entities;

namespace Library.Repositories.Interfaces;

public interface IBookRepository : IBaseRepository<Book>
{
    Task<List<Book>> GetByListIdsAsync(List<Guid> ids);
}