using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Survey.Server.Migrations
{
    public partial class testaet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reply",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReplyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reply_CardModel_CardModelId",
                        column: x => x.CardModelId,
                        principalTable: "CardModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reply_Reply_ReplyId",
                        column: x => x.ReplyId,
                        principalTable: "Reply",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reply_CardModelId",
                table: "Reply",
                column: "CardModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Reply_ReplyId",
                table: "Reply",
                column: "ReplyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reply");
        }
    }
}
