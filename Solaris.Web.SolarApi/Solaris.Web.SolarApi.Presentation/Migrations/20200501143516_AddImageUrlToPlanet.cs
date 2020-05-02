using Microsoft.EntityFrameworkCore.Migrations;

namespace Solaris.Web.SolarApi.Presentation.Migrations
{
    public partial class AddImageUrlToPlanet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "ImageUrl",
                "Planets",
                "varchar(2048)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "ImageUrl",
                "Planets");
        }
    }
}