using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BundlesOfAmaze.Data
{
    public class ItemConfiguration : EntityTypeConfiguration<Item>
    {
        public override void Map(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable(typeof(Item).Name);
            builder.HasKey(i => i.Id);

            ////builder.HasDiscriminator<string>("blog_type")
            ////    .HasValue<Blog>("blog_base")
            ////    .HasValue<RssBlog>("blog_rss");
        }
    }
}