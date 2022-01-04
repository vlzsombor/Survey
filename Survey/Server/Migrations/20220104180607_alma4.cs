using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Survey.Server.Migrations
{
    public partial class alma4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_AspNetUsers_IdentityUserId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_CardModel_CardModelId",
                table: "Rating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rating",
                table: "Rating");

            migrationBuilder.RenameTable(
                name: "Rating",
                newName: "RatingModel");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_IdentityUserId",
                table: "RatingModel",
                newName: "IX_RatingModel_IdentityUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_CardModelId",
                table: "RatingModel",
                newName: "IX_RatingModel_CardModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RatingModel",
                table: "RatingModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingModel_AspNetUsers_IdentityUserId",
                table: "RatingModel",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingModel_CardModel_CardModelId",
                table: "RatingModel",
                column: "CardModelId",
                principalTable: "CardModel",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingModel_AspNetUsers_IdentityUserId",
                table: "RatingModel");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingModel_CardModel_CardModelId",
                table: "RatingModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RatingModel",
                table: "RatingModel");

            migrationBuilder.RenameTable(
                name: "RatingModel",
                newName: "Rating");

            migrationBuilder.RenameIndex(
                name: "IX_RatingModel_IdentityUserId",
                table: "Rating",
                newName: "IX_Rating_IdentityUserId");

            migrationBuilder.RenameIndex(
                name: "IX_RatingModel_CardModelId",
                table: "Rating",
                newName: "IX_Rating_CardModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rating",
                table: "Rating",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_AspNetUsers_IdentityUserId",
                table: "Rating",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_CardModel_CardModelId",
                table: "Rating",
                column: "CardModelId",
                principalTable: "CardModel",
                principalColumn: "Id");
        }
    }
}
