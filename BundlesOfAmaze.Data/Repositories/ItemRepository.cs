using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BundlesOfAmaze.Data
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        /// <summary>Initializes a new instance of the <see cref="ItemRepository"/> class.</summary>
        /// <param name="dataContext">The data context.</param>
        public ItemRepository(IDataContext dataContext)
            : base(dataContext)
        {
        }

        /// <summary>Finds the item by name asynchronous.</summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns>A <see cref="Item"/> instance.</returns>
        public async Task<Item> FindByNameAsync(string itemName)
        {
            return await Queryable.FirstOrDefaultAsync(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        }
    }
}