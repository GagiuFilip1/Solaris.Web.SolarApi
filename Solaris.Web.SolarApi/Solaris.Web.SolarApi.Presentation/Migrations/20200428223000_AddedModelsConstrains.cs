using Microsoft.EntityFrameworkCore.Migrations;

namespace Solaris.Web.SolarApi.Presentation.Migrations
{
    public partial class AddedModelsConstrains : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "Name",
                "SolarSystems",
                "varchar(256)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET latin1",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "Name",
                "Planets",
                "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET latin1",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "Description",
                "Planets",
                "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET latin1",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "Name",
                "SolarSystems",
                "longtext CHARACTER SET latin1",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(256)");

            migrationBuilder.AlterColumn<string>(
                "Name",
                "Planets",
                "longtext CHARACTER SET latin1",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                "Description",
                "Planets",
                "longtext CHARACTER SET latin1",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
