using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace BundlesOfAmaze.Data
{
    public class DataContextFactory : IDbContextFactory<DataContext>
    {
        public DataContext Create(DbContextFactoryOptions options)
        {
            var environmentName = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

            // Used only for EF .NET Core CLI tools (update database/migrations etc.)
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{environmentName}.json", true, true);
            var config = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<DataContext>().UseSqlServer(config.GetConnectionString("DataContext"));

            return new DataContext(optionsBuilder.Options);
        }
    }
}