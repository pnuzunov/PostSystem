using Microsoft.EntityFrameworkCore.Migrations;

namespace PostSystem.Data.Migrations
{
    public partial class PostOfficeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Office_Post_Code",
                table: "PostOffices",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Office_Post_Code",
                table: "PostOffices");
        }
    }
}
