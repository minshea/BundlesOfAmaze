using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BundlesOfAmaze.Data
{
    public class OwnerInventoryItemConfiguration : EntityTypeConfiguration<OwnerInventoryItem>
    {
        public override void Map(EntityTypeBuilder<OwnerInventoryItem> builder)
        {
            builder.ToTable(typeof(OwnerInventoryItem).Name);
            builder.HasKey(i => i.Id);
        }
    }
}