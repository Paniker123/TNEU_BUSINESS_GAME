using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccessLayer.Migrations
{
    public partial class Dota2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayersForGame_Games_GameId",
                table: "PlayersForGame");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayersForGame_Players_PlayerId",
                table: "PlayersForGame");

            migrationBuilder.DropTable(
                name: "UserPlayers");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayersForGame",
                table: "PlayersForGame");

            migrationBuilder.DropIndex(
                name: "IX_PlayersForGame_PlayerId",
                table: "PlayersForGame");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "PlayersForGame");

            migrationBuilder.RenameTable(
                name: "PlayersForGame",
                newName: "PlayerForGameEntity");

            migrationBuilder.RenameIndex(
                name: "IX_PlayersForGame_GameId",
                table: "PlayerForGameEntity",
                newName: "IX_PlayerForGameEntity_GameId");

            migrationBuilder.AddColumn<int>(
                name: "GameRole",
                table: "PlayerForGameEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PlayerForGameEntity",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerForGameEntity",
                table: "PlayerForGameEntity",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerForGameEntity_UserId",
                table: "PlayerForGameEntity",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerForGameEntity_Games_GameId",
                table: "PlayerForGameEntity",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerForGameEntity_Users_UserId",
                table: "PlayerForGameEntity",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerForGameEntity_Games_GameId",
                table: "PlayerForGameEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerForGameEntity_Users_UserId",
                table: "PlayerForGameEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerForGameEntity",
                table: "PlayerForGameEntity");

            migrationBuilder.DropIndex(
                name: "IX_PlayerForGameEntity_UserId",
                table: "PlayerForGameEntity");

            migrationBuilder.DropColumn(
                name: "GameRole",
                table: "PlayerForGameEntity");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PlayerForGameEntity");

            migrationBuilder.RenameTable(
                name: "PlayerForGameEntity",
                newName: "PlayersForGame");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerForGameEntity_GameId",
                table: "PlayersForGame",
                newName: "IX_PlayersForGame_GameId");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "PlayersForGame",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayersForGame",
                table: "PlayersForGame",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GameRole = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GamePlayerId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPlayers_Players_GamePlayerId",
                        column: x => x.GamePlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPlayers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayersForGame_PlayerId",
                table: "PlayersForGame",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPlayers_GamePlayerId",
                table: "UserPlayers",
                column: "GamePlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPlayers_UserId",
                table: "UserPlayers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayersForGame_Games_GameId",
                table: "PlayersForGame",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayersForGame_Players_PlayerId",
                table: "PlayersForGame",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
