using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BundlesOfAmaze.Data
{
    public class OwnerRepository : Repository<Owner>, IOwnerRepository
    {
        /// <summary>Initializes a new instance of the <see cref="OwnerRepository"/> class.</summary>
        /// <param name="dataContext">The data context.</param>
        public OwnerRepository(IDataContext dataContext)
            : base(dataContext)
        {
        }

        /// <summary>Gets the queryable.</summary>
        /// <value>The queryable.</value>
        public override IQueryable<Owner> Queryable => DbSet.Include(i => i.InventoryItems);

        /// <summary>Finds the by author identifier asynchronous.</summary>
        /// <param name="authorId">The author identifier.</param>
        /// <returns>A <see cref="Owner"/> instance.</returns>
        public async Task<Owner> FindByAuthorIdAsync(string authorId)
        {
            return await Queryable.FirstOrDefaultAsync(i => i.AuthorId == authorId);
        }
    }
}