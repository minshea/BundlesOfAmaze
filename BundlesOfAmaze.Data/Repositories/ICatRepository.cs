using System.Collections.Generic;
using System.Threading.Tasks;

namespace BundlesOfAmaze.Data
{
    public interface ICatRepository : IRepository<Cat>
    {
        /// <summary>Finds a cat by name asynchronous.</summary>
        /// <param name="name">The name.</param>
        /// <returns>A <see cref="Cat"/> instance.</returns>
        Task<Cat> FindByNameAsync(string name);

        /// <summary>Finds a cat by owner asynchronous.</summary>
        /// <param name="ownerId">The owner identifier.</param>
        /// <returns>A <see cref="Cat"/> instance.</returns>
        Task<Cat> FindByOwnerAsync(string ownerId);

        /// <summary>Finds all cats.</summary>
        /// <returns>A <see cref="IEnumerable{Cat}"/> instance.</returns>
        Task<IEnumerable<Cat>> FindAllAsync();
    }
}