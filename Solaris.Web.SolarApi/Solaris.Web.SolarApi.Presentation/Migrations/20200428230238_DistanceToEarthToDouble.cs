using Microsoft.EntityFrameworkCore.Migrations;

namespace Solaris.Web.SolarApi.Presentation.Migrations
{
    public partial class DistanceToEarthToDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "DistanceToEarth",
                table: "SolarSystems",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "DistanceToEarth",
                table: "SolarSystems",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
