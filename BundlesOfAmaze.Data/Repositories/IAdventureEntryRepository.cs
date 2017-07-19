using System.Collections.Generic;
using System.Threading.Tasks;

namespace BundlesOfAmaze.Data
{
    public interface IAdventureEntryRepository : IRepository<AdventureEntry>
    {
        /// <summary>Finds an adventure by cat identifier asynchronous.</summary>
        /// <param name="catId">The cat identifier.</param>
        /// <returns>A <see cref="AdventureEntry"/> instance.</returns>
        Task<AdventureEntry> FindByCatIdAsync(long catId);

        /// <summary>Finds adventures by adventure identifier asynchronous.</summary>
        /// <param name="adventureRef">The adventure reference.</param>
        /// <returns>A <see cref="IEnumerable{AdventureEntry}"/> instance.</returns>
        Task<IEnumerable<AdventureEntry>> FindByAdventureRefAsync(AdventureRef adventureRef);
    }
}