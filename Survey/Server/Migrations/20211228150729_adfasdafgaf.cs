using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Survey.Server.Migrations
{
    public partial class adfasdafgaf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "BoardModel",
                newName: "ExpDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpDate",
                table: "BoardModel",
                newName: "StartDate");
        }
    }
}
