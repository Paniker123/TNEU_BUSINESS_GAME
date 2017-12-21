using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class Doba2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPlayers_Players_PlayerId",
                table: "UserPlayers");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "UserPlayers",
                newName: "GamePlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPlayers_PlayerId",
                table: "UserPlayers",
                newName: "IX_UserPlayers_GamePlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPlayers_Players_GamePlayerId",
                table: "UserPlayers",
                column: "GamePlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPlayers_Players_GamePlayerId",
                table: "UserPlayers");

            migrationBuilder.RenameColumn(
                name: "GamePlayerId",
                table: "UserPlayers",
                newName: "PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPlayers_GamePlayerId",
                table: "UserPlayers",
                newName: "IX_UserPlayers_PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPlayers_Players_PlayerId",
                table: "UserPlayers",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
