using System.Linq.Expressions;
using Common;

namespace HotelReservation.Application.Interfaces;

public interface IRepository<T, TId> where T : EntityBase<TId>
{
    Task<T> GetByIdAsync(TId id, bool isTraking = false);
    Task<IEnumerable<T>> ListAsync(bool isTraking = false);
    Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate, bool isTraking = false);
    Task AddAsync(T entity);
    void DeleteAsync(T entity);
    void EditAsync(T entity);

    Task SaveChangesAsync();
}
