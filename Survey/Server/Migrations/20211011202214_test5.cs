using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Survey.Server.Migrations
{
    public partial class test5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BoardModelId",
                table: "CardModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoardModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardModel_User_OwnerUserId",
                        column: x => x.OwnerUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardModel_BoardModelId",
                table: "CardModel",
                column: "BoardModelId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardModel_OwnerUserId",
                table: "BoardModel",
                column: "OwnerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardModel_BoardModel_BoardModelId",
                table: "CardModel",
                column: "BoardModelId",
                principalTable: "BoardModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardModel_BoardModel_BoardModelId",
                table: "CardModel");

            migrationBuilder.DropTable(
                name: "BoardModel");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_CardModel_BoardModelId",
                table: "CardModel");

            migrationBuilder.DropColumn(
                name: "BoardModelId",
                table: "CardModel");
        }
    }
}
