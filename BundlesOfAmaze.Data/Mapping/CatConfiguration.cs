using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BundlesOfAmaze.Data
{
    public class CatConfiguration : EntityTypeConfiguration<Cat>
    {
        public override void Map(EntityTypeBuilder<Cat> builder)
        {
            builder.ToTable(typeof(Cat).Name);
            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.Stats).WithOne(i => i.Cat).HasForeignKey<Stats>(i => i.CatId);
        }
    }
}