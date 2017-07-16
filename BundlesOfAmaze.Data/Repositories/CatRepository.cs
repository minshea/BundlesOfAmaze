using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BundlesOfAmaze.Data
{
    public class CatRepository : Repository<Cat>, ICatRepository
    {
        /// <summary>Initializes a new instance of the <see cref="CatRepository"/> class.</summary>
        /// <param name="dataContext">The data context.</param>
        public CatRepository(IDataContext dataContext)
            : base(dataContext)
        {
        }

        /// <summary>Gets the queryable.</summary>
        /// <value>The queryable.</value>
        public override IQueryable<Cat> Queryable => DbSet.Include(i => i.Stats);

        /// <summary>Finds a cat by name asynchronous.</summary>
        /// <param name="name">The name.</param>
        /// <returns>A <see cref="Cat"/> instance.</returns>
        public async Task<Cat> FindByNameAsync(string name)
        {
            return await Queryable.FirstOrDefaultAsync(i => i.Name == name);
        }

        /// <summary>Finds a cat by owner asynchronous.</summary>
        /// <param name="ownerId">The owner identifier.</param>
        /// <returns>A <see cref="Cat"/> instance.</returns>
        public async Task<Cat> FindByOwnerAsync(long ownerId)
        {
            return await Queryable.FirstOrDefaultAsync(i => i.OwnerId == ownerId);
        }
    }
}