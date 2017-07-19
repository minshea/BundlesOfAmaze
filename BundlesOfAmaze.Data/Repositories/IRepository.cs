using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BundlesOfAmaze.Data
{
    public interface IRepository<T>
    {
        /// <summary>Gets the queryable.</summary>
        /// <value>The queryable.</value>
        IQueryable<T> Queryable { get; }

        /// <summary>Finds the entity asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A <see cref="T"/> instance.</returns>
        Task<T> FindAsync(long id);

        /// <summary>Finds all entities asynchronous.</summary>
        /// <returns>A <see cref="IEnumerable{T}"/> instance.</returns>
        Task<IEnumerable<T>> FindAllAsync();

        /// <summary>Finds the matching asynchronous.</summary>
        /// <param name="expression">The expression.</param>
        /// <returns>A <see cref="T"/> instance.</returns>
        Task<T> FindMatchingAsync(Expression<Func<T, bool>> expression);

        /// <summary>Finds all matching asynchronous.</summary>
        /// <param name="expression">The expression.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> instance.</returns>
        Task<IEnumerable<T>> FindAllMatchingAsync(Expression<Func<T, bool>> expression);

        /// <summary>Adds the entity.</summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>Adds the entity.</summary>
        /// <param name="entity"></param>
        void Remove(T entity);

        /// <summary>Saves the changes asynchronous.</summary>
        /// <returns>A <see cref="Task"/> instance.</returns>
        Task SaveChangesAsync();
    }
}