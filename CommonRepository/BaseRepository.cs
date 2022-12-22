using System.Linq.Expressions;
using CommonRepository.Abstractions;
using CommonRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace CommonRepository;

public abstract class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity> where TEntity: BaseRepositoryEntity where TContext : DbContext
{
    protected readonly TContext Context;
    
    protected BaseRepository(TContext dbContext)
    {
        Context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    
    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate,
                                                               Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
    {
        var query = Context.Set<TEntity>()
                           .AsNoTracking()
                           .Where(predicate);
        if (orderBy != null) return await orderBy(query).ToListAsync();
        return await query.ToListAsync();
    }

    public virtual async Task<TEntity?> GetByIdAsync(int id)
    {
        return await Context.Set<TEntity>()
                            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        entity.CreatedDate = DateTime.Now;
        entity.LastModifiedDate = DateTime.Now;
        Context.Set<TEntity>().Add(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        entity.LastModifiedDate = DateTime.Now;
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task<int> GetCountAsync()
    {
        return await Context.Set<TEntity>().CountAsync();
    }

    public virtual async Task<List<TEntity>> GetFilteredBatchOfData(int pageSize, int page, string? filterString = null)
    {
        return await FilterByString(Context.Set<TEntity>(), filterString).OrderByDescending(e => e.LastModifiedDate)
                                                                         .Skip((page - 1) * pageSize)
                                                                         .Take(pageSize)
                                                                         .ToListAsync();
        
    }

    public virtual async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Context.Set<TEntity>()
                            .FirstOrDefaultAsync(predicate);
    }

    protected abstract IQueryable<TEntity> FilterByString(IQueryable<TEntity> query, string? filterString);
}