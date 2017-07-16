using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BundlesOfAmaze.Data.Migrations
{
    public partial class AddItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnerInventoryItem_Owners_OwnerId",
                table: "OwnerInventoryItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Owners",
                table: "Owners");

            migrationBuilder.RenameTable(
                name: "Owners",
                newName: "Owner");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Owner",
                table: "Owner",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    ItemType = table.Column<int>(nullable: false),
                    Label = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    FoodValue = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_OwnerInventoryItem_Owner_OwnerId",
                table: "OwnerInventoryItem",
                column: "OwnerId",
                principalTable: "Owner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnerInventoryItem_Owner_OwnerId",
                table: "OwnerInventoryItem");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Owner",
                table: "Owner");

            migrationBuilder.RenameTable(
                name: "Owner",
                newName: "Owners");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Owners",
                table: "Owners",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OwnerInventoryItem_Owners_OwnerId",
                table: "OwnerInventoryItem",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
