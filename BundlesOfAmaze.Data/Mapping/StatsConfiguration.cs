using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BundlesOfAmaze.Data
{
    public class StatsConfiguration : EntityTypeConfiguration<Stats>
    {
        public override void Map(EntityTypeBuilder<Stats> builder)
        {
            builder.ToTable(typeof(Stats).Name);
            builder.HasKey(i => i.Id);
        }
    }
}