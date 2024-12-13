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

    public virtual async Task<T> GetByIdAsync(TId id, bool isTraking = false)
    {
        return isTraking ? await _dbContext.Set<T>().AsNoTracking().SingleOrDefaultAsync(x => x.Id!.Equals(id)) :
            await _dbContext.Set<T>().SingleOrDefaultAsync(x => x.Id!.Equals(id));
    }

    public virtual async Task<IEnumerable<T>> ListAsync(bool isTraking = false)
    {
        return isTraking ? await _dbContext.Set<T>().AsNoTracking().ToListAsync() 
            : await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate, bool isTraking = false)
    {
        return isTraking ? await _dbContext.Set<T>().AsNoTracking().Where(predicate).ToListAsync() :
            await _dbContext.Set<T>().Where(predicate).ToListAsync();
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

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
