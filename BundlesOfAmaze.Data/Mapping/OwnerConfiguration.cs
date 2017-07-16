using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BundlesOfAmaze.Data
{
    public class OwnerConfiguration : EntityTypeConfiguration<Owner>
    {
        public override void Map(EntityTypeBuilder<Owner> builder)
        {
            builder.ToTable(typeof(Owner).Name);
            builder.HasKey(i => i.Id);

            ////builder.HasOne(i => i.Cat).WithOne(i => i.Owner).HasForeignKey<Cat>(i => i.OwnerId);
            builder.HasMany(i => i.InventoryItems).WithOne(i => i.Owner).HasForeignKey(i => i.OwnerId);
        }
    }
}