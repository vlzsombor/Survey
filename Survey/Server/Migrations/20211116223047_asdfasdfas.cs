using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Survey.Server.Migrations
{
    public partial class asdfasdfas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "CardModel");

            migrationBuilder.CreateTable(
                name: "RatingModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RatingNumber = table.Column<int>(type: "int", nullable: false),
                    CardModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingModel_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RatingModel_CardModel_CardModelId",
                        column: x => x.CardModelId,
                        principalTable: "CardModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RatingModel_CardModelId",
                table: "RatingModel",
                column: "CardModelId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingModel_IdentityUserId",
                table: "RatingModel",
                column: "IdentityUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RatingModel");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "CardModel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
