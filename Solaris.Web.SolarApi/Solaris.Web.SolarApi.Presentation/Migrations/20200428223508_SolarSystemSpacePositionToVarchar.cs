using Microsoft.EntityFrameworkCore.Migrations;

namespace Solaris.Web.SolarApi.Presentation.Migrations
{
    public partial class SolarSystemSpacePositionToVarchar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "SpacePosition",
                "SolarSystems",
                "varchar(256)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET latin1");

            migrationBuilder.AlterColumn<long>(
                "DistanceToEarth",
                "SolarSystems",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(256)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "SpacePosition",
                "SolarSystems",
                "longtext CHARACTER SET latin1",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(256)");

            migrationBuilder.AlterColumn<string>(
                "DistanceToEarth",
                "SolarSystems",
                "varchar(256)",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
