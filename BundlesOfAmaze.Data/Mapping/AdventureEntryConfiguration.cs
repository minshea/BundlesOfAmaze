using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BundlesOfAmaze.Data.Mapping
{
    public class AdventureEntryConfiguration : EntityTypeConfiguration<AdventureEntry>
    {
        public override void Map(EntityTypeBuilder<AdventureEntry> builder)
        {
            builder.ToTable(typeof(AdventureEntry).Name);
            builder.HasKey(i => i.Id);
        }
    }
}