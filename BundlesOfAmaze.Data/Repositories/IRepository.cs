using System.Threading.Tasks;

namespace BundlesOfAmaze.Data
{
    public interface IRepository<T>
    {
        /// <summary>Adds the entity asynchronous.</summary>
        Task AddAsync(T entity);

        /// <summary>Saves the changes asynchronous.</summary>
        /// <returns>A <see cref="Task"/> instance.</returns>
        Task SaveChangesAsync();
    }
}