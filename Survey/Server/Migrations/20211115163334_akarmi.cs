using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Survey.Server.Migrations
{
    public partial class akarmi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardFillers");

            migrationBuilder.AddColumn<Guid>(
                name: "BoardModelId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "identityUserId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BoardModelId",
                table: "AspNetUsers",
                column: "BoardModelId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_identityUserId",
                table: "AspNetUsers",
                column: "identityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_identityUserId",
                table: "AspNetUsers",
                column: "identityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_BoardModel_BoardModelId",
                table: "AspNetUsers",
                column: "BoardModelId",
                principalTable: "BoardModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_identityUserId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_BoardModel_BoardModelId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BoardModelId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_identityUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BoardModelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "identityUserId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "BoardFillers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoardModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    identityUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardFillers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardFillers_AspNetUsers_identityUserId",
                        column: x => x.identityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardFillers_BoardModel_BoardModelId",
                        column: x => x.BoardModelId,
                        principalTable: "BoardModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardFillers_BoardModelId",
                table: "BoardFillers",
                column: "BoardModelId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardFillers_identityUserId",
                table: "BoardFillers",
                column: "identityUserId");
        }
    }
}
