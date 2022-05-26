using Microsoft.EntityFrameworkCore.Migrations;

namespace Task2.Data.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Texts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Texts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
