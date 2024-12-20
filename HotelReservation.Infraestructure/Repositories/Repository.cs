using System.Linq.Expressions;

using Common;
using HotelReservation.Application.Interfaces;
using HotelReservation.Infraestructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Infraestructure.Repositories;
public class Repository<T, TId> : IRepository<T, TId> where T : EntityBase<TId>
{
    protected readonly ApplicationDbContext _dbContext;

    public Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<T?> GetByIdAsync(TId id, bool isTraking = false, string[]? navigationProperties = null)
    {
        var query = _dbContext.Set<T>().AsQueryable();

        if (!isTraking)
        {
            query = query.AsNoTracking();
        }

        if (navigationProperties != null && navigationProperties.Length > 0)
        {
            query = IncludeMultiple(query, navigationProperties);
        }

        return await query.SingleOrDefaultAsync(x => x.Id!.Equals(id));
    }

    public virtual async Task<IEnumerable<T>> ListAsync(bool isTraking = false)
    {
        return !isTraking ? await _dbContext.Set<T>().AsNoTracking().ToListAsync() 
            : await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate, bool isTraking = false, string[]? navigationProperties = null)
    {
        var query = _dbContext.Set<T>().AsQueryable();

        if (!isTraking)
        {
            query = query.AsNoTracking();
        }

        if (navigationProperties != null && navigationProperties.Length > 0)
        {
            query = IncludeMultiple(query, navigationProperties);
        }

        return await query.Where(predicate).ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
    }

    public void DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public void EditAsync(T entity)
    {
        _dbContext.Set<T>().Update(entity);
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbContext.Set<T>().AsNoTracking().AnyAsync(predicate);
    }

    public IQueryable<T> QueryAsync(bool isTraking = false)
    {
        return !isTraking ? _dbContext.Set<T>().AsNoTracking().AsQueryable() :
            _dbContext.Set<T>().AsQueryable();
    }

    public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate, bool isTraking = false, string[]? navigationProperties = null)
    {
        var query = _dbContext.Set<T>().AsQueryable();

        if (!isTraking)
        {
            query = query.AsNoTracking();
        }

        if (navigationProperties != null && navigationProperties.Length > 0)
        {
            query = IncludeMultiple(query, navigationProperties);
        }

        return await query.SingleOrDefaultAsync(predicate);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private static IQueryable<T> IncludeMultiple(IQueryable<T> query, params string[] navigationProperties)
    {
        if (navigationProperties != null)
        {
            query = navigationProperties.Aggregate(query,
              (current, include) => current.Include(include));
        }

        return query;
    }
}
