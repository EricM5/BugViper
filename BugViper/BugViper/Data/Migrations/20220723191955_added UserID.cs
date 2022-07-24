using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugViper.Data.Migrations
{
    public partial class addedUserID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThisUserId",
                table: "Bug",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThisUserId",
                table: "Bug");
        }
    }
}
