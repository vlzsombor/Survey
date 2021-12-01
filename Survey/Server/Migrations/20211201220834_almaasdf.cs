using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Survey.Server.Migrations
{
    public partial class almaasdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_BoardModel_BoardModelId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_BoardModel_BoardModelId",
                table: "AspNetUsers",
                column: "BoardModelId",
                principalTable: "BoardModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_BoardModel_BoardModelId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_BoardModel_BoardModelId",
                table: "AspNetUsers",
                column: "BoardModelId",
                principalTable: "BoardModel",
                principalColumn: "Id");
        }
    }
}
