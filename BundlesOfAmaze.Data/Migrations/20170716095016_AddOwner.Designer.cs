using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BundlesOfAmaze.Data;

namespace BundlesOfAmaze.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20170716095016_AddOwner")]
    partial class AddOwner
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<long>("OwnerId");

                    b.HasKey("Id");

                    b.ToTable("Cat");
                });

            modelBuilder.Entity("BundlesOfAmaze.Data.Owner", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("BundlesOfAmaze.Data.OwnerInventoryItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ItemId");

                    b.Property<long>("OwnerId");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("OwnerInventoryItem");
                });

            modelBuilder.Entity("BundlesOfAmaze.Data.Stats", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CatId");

                    b.Property<int>("High");

                    b.Property<int>("Hunger");

                    b.Property<int>("Kind");

                    b.Property<int>("Lazy");

                    b.Property<int>("Outgoing");

                    b.Property<int>("Resourceful");

                    b.Property<int>("Thirst");

                    b.HasKey("Id");

                    b.HasIndex("CatId")
                        .IsUnique();

                    b.ToTable("Stats");
                });

            modelBuilder.Entity("BundlesOfAmaze.Data.OwnerInventoryItem", b =>
                {
                    b.HasOne("BundlesOfAmaze.Data.Owner", "Owner")
                        .WithMany("InventoryItems")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
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
