using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BundlesOfAmaze.Data
{
    public class CatStatsConfiguration : EntityTypeConfiguration<CatStats>
    {
        public override void Map(EntityTypeBuilder<CatStats> builder)
        {
            builder.ToTable(typeof(CatStats).Name);
            builder.HasKey(i => i.Id);
        }
    }
}