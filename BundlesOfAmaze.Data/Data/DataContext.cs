using Microsoft.EntityFrameworkCore;

namespace BundlesOfAmaze.Data
{
    public class DataContext : DbContext, IDataContext
    {
        public DbSet<Cat> Cats { get; set; }

        private const string ConnectionString = @"Server=hyperion;Database=BundlesOfAmaze;User Id=development;Password=development;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ////if (!optionsBuilder.IsConfigured)
            ////{
            ////    optionsBuilder.UseSqlServer(ConnectionString);
            ////}

            optionsBuilder.UseSqlServer(ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.AddConfiguration(new CatConfiguration());
        }
    }
}