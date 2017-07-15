using Microsoft.EntityFrameworkCore;

namespace BundlesOfAmaze.Data
{
    public class DataContext : DbContext, IDataContext
    {
        private readonly string _connectionString;
        public DbSet<Cat> Cats { get; set; }

        public DataContext(string connectionString)
        {
            // This constructor is used during runtime
            _connectionString = connectionString;
        }

        public DataContext(DbContextOptions options)
            : base(options)
        {
            // This constructor is used by EF migrations
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Only configure the conenction string during normal runtime
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.AddConfiguration(new CatConfiguration());
        }
    }
}