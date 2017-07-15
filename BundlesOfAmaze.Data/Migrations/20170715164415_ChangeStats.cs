using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BundlesOfAmaze.Data.Migrations
{
    public partial class ChangeStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Personality",
                table: "Cat");

            migrationBuilder.AddColumn<int>(
                name: "High",
                table: "Stats",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Kind",
                table: "Stats",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Lazy",
                table: "Stats",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Outgoing",
                table: "Stats",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Resourceful",
                table: "Stats",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Thirst",
                table: "Stats",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "High",
                table: "Stats");

            migrationBuilder.DropColumn(
                name: "Kind",
                table: "Stats");

            migrationBuilder.DropColumn(
                name: "Lazy",
                table: "Stats");

            migrationBuilder.DropColumn(
                name: "Outgoing",
                table: "Stats");

            migrationBuilder.DropColumn(
                name: "Resourceful",
                table: "Stats");

            migrationBuilder.DropColumn(
                name: "Thirst",
                table: "Stats");

            migrationBuilder.AddColumn<int>(
                name: "Personality",
                table: "Cat",
                nullable: false,
                defaultValue: 0);
        }
    }
}
