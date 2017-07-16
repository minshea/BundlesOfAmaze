using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BundlesOfAmaze.Data.Migrations
{
    public partial class AddOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "OwnerId",
                table: "Cat",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OwnerInventoryItem",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemId = table.Column<long>(nullable: false),
                    OwnerId = table.Column<long>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerInventoryItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OwnerInventoryItem_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OwnerInventoryItem_OwnerId",
                table: "OwnerInventoryItem",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OwnerInventoryItem");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Cat",
                nullable: true,
                oldClrType: typeof(long));
        }
    }
}