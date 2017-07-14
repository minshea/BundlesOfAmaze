using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BundlesOfAmaze.Data;

namespace BundlesOfAmaze.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BundlesOfAmaze.Data.Cat", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("DateOfBirth");

                    b.Property<int>("Gender");

                    b.Property<string>("Name");

                    b.Property<string>("OwnerId");

                    b.Property<int>("Personality");

                    b.HasKey("Id");

                    b.ToTable("Cat");
                });

            modelBuilder.Entity("BundlesOfAmaze.Data.Stats", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CatId");

                    b.Property<int>("Hunger");

                    b.HasKey("Id");

                    b.HasIndex("CatId")
                        .IsUnique();

                    b.ToTable("Stats");
                });

            modelBuilder.Entity("BundlesOfAmaze.Data.Stats", b =>
                {
                    b.HasOne("BundlesOfAmaze.Data.Cat", "Cat")
                        .WithOne("Stats")
                        .HasForeignKey("BundlesOfAmaze.Data.Stats", "CatId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
