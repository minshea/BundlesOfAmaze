using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BundlesOfAmaze.Data
{
    public class AdventureEntryRepository : Repository<AdventureEntry>, IAdventureEntryRepository
    {
        public AdventureEntryRepository(IDataContext dataContext)
            : base(dataContext)
        {
        }

        public async Task<AdventureEntry> FindByCatIdAsync(long catId)
        {
            return await Queryable.FirstOrDefaultAsync(i => i.CatId == catId);
        }

        public async Task<IEnumerable<AdventureEntry>> FindByAdventureRefAsync(AdventureRef adventureRef)
        {
            return await Queryable.Where(i => i.AdventureRef == adventureRef).ToListAsync();
        }
    }
}