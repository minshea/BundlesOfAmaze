using System.Threading.Tasks;

namespace BundlesOfAmaze.Data
{
    public interface IOwnerRepository : IRepository<Owner>
    {
        /// <summary>Finds the by author identifier asynchronous.</summary>
        /// <param name="authorId">The author identifier.</param>
        /// <returns>A <see cref="Owner"/> instance.</returns>
        Task<Owner> FindByAuthorIdAsync(string authorId);
    }
}