using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class Dodal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerScore",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Players",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "Players");

            migrationBuilder.AddColumn<double>(
                name: "PlayerScore",
                table: "Players",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
