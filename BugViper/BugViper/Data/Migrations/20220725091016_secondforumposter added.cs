using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugViper.Data.Migrations
{
    public partial class secondforumposteradded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SecondForumPoster",
                table: "Bug",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecondForumPoster",
                table: "Bug");
        }
    }
}
