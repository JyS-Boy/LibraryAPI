using Library.DataAccess.Entities;
using System.Linq.Expressions;

namespace Library.Repositories.Interfaces;

public interface IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<TEntity> CreateAsync(TEntity entity);

    Task<TEntity?> GetByIdAsync(Guid id);

    Task<TEntity?> GetByPredicateAsync(
        Expression<Func<TEntity, bool>> predicate);

    Task UpdateAsync(TEntity entity);

    Task<(ICollection<TResult> Result, int TotalCount)>
        ListAsync<TResult, TKey>
        (
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, TKey>> orderBy,
            int pageNumber = 1,
            int pageSize = 10
        );

    Task DeleteAsync(Guid id);
}