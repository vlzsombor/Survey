using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Survey.Server.Migrations
{
    public partial class fdadsafdsa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoardFillerGuid",
                table: "BoardFillers");

            migrationBuilder.DropColumn(
                name: "PinCode",
                table: "BoardFillers");

            migrationBuilder.AddColumn<string>(
                name: "identityUserId",
                table: "BoardFillers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BoardFillers_identityUserId",
                table: "BoardFillers",
                column: "identityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardFillers_AspNetUsers_identityUserId",
                table: "BoardFillers",
                column: "identityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardFillers_AspNetUsers_identityUserId",
                table: "BoardFillers");

            migrationBuilder.DropIndex(
                name: "IX_BoardFillers_identityUserId",
                table: "BoardFillers");

            migrationBuilder.DropColumn(
                name: "identityUserId",
                table: "BoardFillers");

            migrationBuilder.AddColumn<Guid>(
                name: "BoardFillerGuid",
                table: "BoardFillers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "PinCode",
                table: "BoardFillers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
