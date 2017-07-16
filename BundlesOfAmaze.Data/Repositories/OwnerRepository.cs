using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BundlesOfAmaze.Data
{
    public class OwnerRepository : IOwnerRepository
    {
        /// <summary>The data context</summary>
        private readonly IDataContext _dataContext;

        /// <summary>Initializes a new instance of the <see cref="OwnerRepository"/> class.</summary>
        /// <param name="dataContext">The data context.</param>
        public OwnerRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>Gets the queryable.</summary>
        /// <value>The queryable.</value>
        private IQueryable<Owner> Queryable
        {
            get
            {
                return _dataContext.Owners.Include(i => i.InventoryItems);
            }
        }

        /// <summary>Finds the by author identifier asynchronous.</summary>
        /// <param name="authorId">The author identifier.</param>
        /// <returns>A <see cref="Owner"/> instance.</returns>
        public async Task<Owner> FindByAuthorIdAsync(string authorId)
        {
            return await Queryable.FirstOrDefaultAsync(i => i.AuthorId == authorId);
        }

        /// <summary>Adds the entity asynchronous.</summary>
        /// <param name="entity"></param>
        /// <returns>A <see cref="Task"/> instance.</returns>
        public async Task AddAsync(Owner entity)
        {
            await _dataContext.Owners.AddAsync(entity);
        }

        /// <summary>Saves the changes asynchronous.</summary>
        /// <returns>A <see cref="Task"/> instance.</returns>
        public async Task SaveChangesAsync()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}