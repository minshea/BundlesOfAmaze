﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BundlesOfAmaze.Data
{
    public class CatRepository : ICatRepository
    {
        /// <summary>The data context</summary>
        private readonly IDataContext _dataContext;

        /// <summary>Initializes a new instance of the <see cref="CatRepository"/> class.</summary>
        /// <param name="dataContext">The data context.</param>
        public CatRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        ////private IQueryable<Cat> Queryable()
        ////{
        ////    return _dataContext.Cats.Include(i => i.Stats);
        ////}

        /// <summary>Finds a cat by name asynchronous.</summary>
        /// <param name="name">The name.</param>
        /// <returns>A <see cref="Cat"/> instance.</returns>
        public async Task<Cat> FindByNameAsync(string name)
        {
            var result = await _dataContext.Cats.FirstOrDefaultAsync(i => i.Name == name);
            return result;
        }

        /// <summary>Finds a cat by owner asynchronous.</summary>
        /// <param name="ownerId">The owner identifier.</param>
        /// <returns>A <see cref="Cat"/> instance.</returns>
        public async Task<Cat> FindByOwnerAsync(string ownerId)
        {
            var result = await _dataContext.Cats.FirstOrDefaultAsync(i => i.OwnerId == ownerId);
            return result;
        }

        /// <summary>Finds all cats.</summary>
        /// <returns>A <see cref="IEnumerable{Cat}"/> instance.</returns>
        public async Task<IEnumerable<Cat>> FindAllAsync()
        {
            return await _dataContext.Cats.ToListAsync();
        }

        /// <summary>Adds the entity asynchronous.</summary>
        /// <param name="entity"></param>
        /// <returns>A <see cref="Task"/> instance.</returns>
        public async Task AddAsync(Cat entity)
        {
            await _dataContext.Cats.AddAsync(entity);
        }

        /// <summary>Saves the changes asynchronous.</summary>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task"/> instance.</returns>
        public async Task SaveChangesAsync()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}