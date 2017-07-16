using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BundlesOfAmaze.Data
{
    public interface IDataContext
    {
        DbSet<Owner> Owners { get; set; }

        DbSet<Cat> Cats { get; set; }

        /// <summary>Saves the changes asynchronous.</summary>
        /// <returns>The rows changed.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}