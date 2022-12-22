using System.Linq.Expressions;
using CommonRepository.Models;

namespace CommonRepository.Abstractions;

public interface IBaseRepository<TEntity> where TEntity : BaseRepositoryEntity
{
    Task<List<TEntity>> GetAllAsync();
    Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy);
    Task<TEntity?> GetByIdAsync(int id);
    Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task<int> GetCountAsync();
    Task<List<TEntity>> GetFilteredBatchOfData(int pageSize, int page, string? filterString = null);
}