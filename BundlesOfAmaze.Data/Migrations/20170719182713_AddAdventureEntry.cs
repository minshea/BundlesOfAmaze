using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BundlesOfAmaze.Data.Migrations
{
    public partial class AddAdventureEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "OwnerInventoryItem");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "Item");

            migrationBuilder.AddColumn<int>(
                name: "ItemRef",
                table: "OwnerInventoryItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ItemRef",
                table: "Item",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AdventureEntry",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdventureRef = table.Column<int>(nullable: false),
                    CatId = table.Column<long>(nullable: false),
                    End = table.Column<DateTimeOffset>(nullable: false),
                    Start = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdventureEntry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatStats",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CatId = table.Column<long>(nullable: false),
                    High = table.Column<int>(nullable: false),
                    Hunger = table.Column<int>(nullable: false),
                    Kind = table.Column<int>(nullable: false),
                    Lazy = table.Column<int>(nullable: false),
                    Outgoing = table.Column<int>(nullable: false),
                    Resourceful = table.Column<int>(nullable: false),
                    Thirst = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatStats_Cat_CatId",
                        column: x => x.CatId,
                        principalTable: "Cat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatStats_CatId",
                table: "CatStats",
                column: "CatId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdventureEntry");

            migrationBuilder.DropTable(
                name: "CatStats");

            migrationBuilder.DropColumn(
                name: "ItemRef",
                table: "OwnerInventoryItem");

            migrationBuilder.DropColumn(
                name: "ItemRef",
                table: "Item");

            migrationBuilder.AddColumn<long>(
                name: "ItemId",
                table: "OwnerInventoryItem",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "Item",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CatId = table.Column<long>(nullable: false),
                    High = table.Column<int>(nullable: false),
                    Hunger = table.Column<int>(nullable: false),
                    Kind = table.Column<int>(nullable: false),
                    Lazy = table.Column<int>(nullable: false),
                    Outgoing = table.Column<int>(nullable: false),
                    Resourceful = table.Column<int>(nullable: false),
                    Thirst = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stats_Cat_CatId",
                        column: x => x.CatId,
                        principalTable: "Cat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stats_CatId",
                table: "Stats",
                column: "CatId",
                unique: true);
        }
    }
}
