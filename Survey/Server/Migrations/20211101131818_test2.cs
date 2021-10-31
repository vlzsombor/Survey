using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Survey.Server.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccessGuid",
                table: "BoardFillers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BoardModelId",
                table: "BoardFillers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BoardFillers_BoardModelId",
                table: "BoardFillers",
                column: "BoardModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardFillers_BoardModel_BoardModelId",
                table: "BoardFillers",
                column: "BoardModelId",
                principalTable: "BoardModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardFillers_BoardModel_BoardModelId",
                table: "BoardFillers");

            migrationBuilder.DropIndex(
                name: "IX_BoardFillers_BoardModelId",
                table: "BoardFillers");

            migrationBuilder.DropColumn(
                name: "AccessGuid",
                table: "BoardFillers");

            migrationBuilder.DropColumn(
                name: "BoardModelId",
                table: "BoardFillers");
        }
    }
}
