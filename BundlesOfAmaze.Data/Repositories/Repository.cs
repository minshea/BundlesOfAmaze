using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BundlesOfAmaze.Data
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        /// <summary>The data context</summary>
        protected readonly IDataContext DataContext;

        /// <summary>The database set</summary>
        protected readonly DbSet<T> DbSet;

        /// <summary>Initializes a new instance of the <see cref="Repository"/> class.</summary>
        /// <param name="dataContext">The data context.</param>
        protected Repository(IDataContext dataContext)
        {
            DataContext = dataContext;
            DbSet = ((DbContext)dataContext).Set<T>();
        }

        /// <summary>Gets the queryable.</summary>
        /// <value>The queryable.</value>
        public virtual IQueryable<T> Queryable => DbSet;

        /// <summary>Finds the asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A <see cref="T"/> instance.</returns>
        public async Task<T> FindAsync(long id)
        {
            return await Queryable.FirstOrDefaultAsync<T>(i => i.Id == id);
        }

        /// <summary>Finds all entities.</summary>
        /// <returns>A <see cref="IEnumerable{T}"/> instance.</returns>
        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await Queryable.ToListAsync();
        }

        /// <summary>Adds the entity asynchronous.</summary>
        /// <param name="entity"></param>
        /// <returns>A <see cref="Task"/> instance.</returns>
        public async Task AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
        }

        /// <summary>Saves the changes asynchronous.</summary>
        /// <returns>A <see cref="Task"/> instance.</returns>
        public async Task SaveChangesAsync()
        {
            await DataContext.SaveChangesAsync();
        }
    }
}