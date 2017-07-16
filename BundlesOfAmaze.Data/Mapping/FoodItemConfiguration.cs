using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BundlesOfAmaze.Data
{
    public class FoodItemConfiguration : EntityTypeConfiguration<FoodItem>
    {
        public override void Map(EntityTypeBuilder<FoodItem> builder)
        {
            builder.ToTable(typeof(Item).Name);
            ////builder.HasKey(i => i.Id);

            ////builder.HasDiscriminator<string>("blog_type")
            ////    .HasValue<Blog>("blog_base")
            ////    .HasValue<RssBlog>("blog_rss");
        }
    }
}