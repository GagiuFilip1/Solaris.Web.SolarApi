using Microsoft.EntityFrameworkCore.Migrations;

namespace Solaris.Web.SolarApi.Presentation.Migrations
{
    public partial class AverageSolarVelocityForPlanets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "SolarWindVelocity",
                "Planets");

            migrationBuilder.AddColumn<double>(
                "AverageSolarWindVelocity",
                "Planets",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "AverageSolarWindVelocity",
                "Planets");

            migrationBuilder.AddColumn<double>(
                "SolarWindVelocity",
                "Planets",
                "double",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
