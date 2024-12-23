﻿using System.Linq.Expressions;
using Common;

namespace HotelReservation.Application.Interfaces;

public interface IRepository<T, TId> where T : EntityBase<TId>
{
    Task<T?> GetByIdAsync(TId id, bool isTraking = false, string[]? navigationProperties = null);
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate, bool isTraking = false, string[]? navigationProperties = null);
    Task<IEnumerable<T>> ListAsync(bool isTraking = false);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate, bool isTraking = false, string[]? navigationProperties = null);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    void DeleteAsync(T entity);
    void EditAsync(T entity);
    void EditRangeAsync(IEnumerable<T> entities);
    IQueryable<T> QueryAsync(bool isTraking = false);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
