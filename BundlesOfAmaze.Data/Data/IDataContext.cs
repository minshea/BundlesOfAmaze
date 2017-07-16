using System.Threading;
using System.Threading.Tasks;

namespace BundlesOfAmaze.Data
{
    public interface IDataContext
    {
        /// <summary>Seeds this instance.</summary>
        /// <returns></returns>
        Task SeedAsync();

        /// <summary>Saves the changes asynchronous.</summary>
        /// <returns>The rows changed.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}