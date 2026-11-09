using Library.DataAccess;
using Library.DataAccess.Entities;
using Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.Repositories.Implementations;

public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly LibraryDbContext _context;

    public BaseRepository(LibraryDbContext context)
    {
        _context = context;
    }
    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity?> GetByPredicateAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedBy = Environment.UserName;
        await _context.SaveChangesAsync();
    }

    public async Task<(ICollection<TResult> Result, int TotalCount)>
        ListAsync<TResult, TKey>
        (
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, TKey>> orderBy,
            int pageNumber = 1,
            int pageSize = 10
        )
    {
        var result = await _context.Set<TEntity>()
            .AsNoTracking()
            .Where(predicate)
            .OrderBy(orderBy)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(selector)
            .ToListAsync();
        var  total = await _context.Set<TEntity>()
            .Where(predicate)
            .CountAsync();
        return (result, total);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _context.Set<TEntity>()
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(x => 
            x.SetProperty(p => p.IsActive, false));
    }
}
