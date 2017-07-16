using System.Linq;
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

        /// <summary>Adds the entity asynchronous.</summary>
        Task AddAsync(T entity);

        /// <summary>Saves the changes asynchronous.</summary>
        /// <returns>A <see cref="Task"/> instance.</returns>
        Task SaveChangesAsync();
    }
}