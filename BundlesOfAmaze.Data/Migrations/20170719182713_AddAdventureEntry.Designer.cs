using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BundlesOfAmaze.Data;

namespace BundlesOfAmaze.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20170719182713_AddAdventureEntry")]
    partial class AddAdventureEntry
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BundlesOfAmaze.Data.AdventureEntry", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdventureRef");

                    b.Property<long>("CatId");

                    b.Property<DateTimeOffset>("End");

                    b.Property<DateTimeOffset>("Start");

                    b.HasKey("Id");

                    b.ToTable("AdventureEntry");
                });

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

            modelBuilder.Entity("BundlesOfAmaze.Data.CatStats", b =>
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

                    b.ToTable("CatStats");
                });

            modelBuilder.Entity("BundlesOfAmaze.Data.Item", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("ItemRef");

                    b.Property<int>("ItemType");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Item");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Item");
                });

            modelBuilder.Entity("BundlesOfAmaze.Data.Owner", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Owner");
                });

            modelBuilder.Entity("BundlesOfAmaze.Data.OwnerInventoryItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ItemRef");

                    b.Property<long>("OwnerId");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("OwnerInventoryItem");
                });

            modelBuilder.Entity("BundlesOfAmaze.Data.FoodItem", b =>
                {
                    b.HasBaseType("BundlesOfAmaze.Data.Item");

                    b.Property<int>("FoodValue");

                    b.ToTable("Item");

                    b.HasDiscriminator().HasValue("FoodItem");
                });

            modelBuilder.Entity("BundlesOfAmaze.Data.CatStats", b =>
                {
                    b.HasOne("BundlesOfAmaze.Data.Cat", "Cat")
                        .WithOne("Stats")
                        .HasForeignKey("BundlesOfAmaze.Data.CatStats", "CatId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BundlesOfAmaze.Data.OwnerInventoryItem", b =>
                {
                    b.HasOne("BundlesOfAmaze.Data.Owner", "Owner")
                        .WithMany("InventoryItems")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
