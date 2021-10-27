using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Survey.Server.Migrations
{
    public partial class te : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardModel_BoardModel_BoardModelId",
                table: "CardModel");

            migrationBuilder.AlterColumn<Guid>(
                name: "BoardModelId",
                table: "CardModel",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "BoardModel",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_CardModel_BoardModel_BoardModelId",
                table: "CardModel",
                column: "BoardModelId",
                principalTable: "BoardModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardModel_BoardModel_BoardModelId",
                table: "CardModel");

            migrationBuilder.AlterColumn<int>(
                name: "BoardModelId",
                table: "CardModel",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "BoardModel",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_CardModel_BoardModel_BoardModelId",
                table: "CardModel",
                column: "BoardModelId",
                principalTable: "BoardModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
