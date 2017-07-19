using System.Collections.Generic;
using System.Threading.Tasks;
using BundlesOfAmaze.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace BundlesOfAmaze.Data
{
    public class DataContext : DbContext, IDataContext
    {
        private readonly string _connectionString;

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

            modelBuilder.AddConfiguration(new OwnerConfiguration());
            modelBuilder.AddConfiguration(new OwnerInventoryItemConfiguration());
            modelBuilder.AddConfiguration(new CatConfiguration());
            modelBuilder.AddConfiguration(new CatStatsConfiguration());
            modelBuilder.AddConfiguration(new AdventureEntryConfiguration());
            modelBuilder.AddConfiguration(new ItemConfiguration());
            modelBuilder.AddConfiguration(new FoodItemConfiguration());
        }

        public async Task SeedAsync()
        {
            await Database.EnsureCreatedAsync();
            await Database.MigrateAsync();

            await SeedItemsAsync();
        }

        private async Task SeedItemsAsync()
        {
            var sourceItems = new List<Item>
            {
                new FoodItem(ItemType.Drink, ItemRef.Milk, "Milk", "Just some regular milk", 7200),
                new FoodItem(ItemType.Drink, ItemRef.Water, "Water", "Just some regular water", 3600),
                new FoodItem(ItemType.Food, ItemRef.Tuna, "Tuna", "Tuna fish!", 7200),
                new FoodItem(ItemType.Food, ItemRef.Goldfish, "Goldfish", "Made with 24 carat gold! Shiny!", 14400),
                new Item(ItemType.Currency, ItemRef.Credit, "Trenni", "Silver coin")
            };

            Item item;
            var set = Set<Item>();
            foreach (var sourceItem in sourceItems)
            {
                item = await set.FirstOrDefaultAsync(i => i.Name == sourceItem.Name);
                if (item == null)
                {
                    // Add
                    Add(sourceItem);
                }
                else
                {
                    // Update
                    if (item.Equals(sourceItem))
                    {
                        continue;
                    }

                    item.Update(sourceItem);
                    Update(item);
                }
            }

            // Commit
            await SaveChangesAsync();
        }
    }
}