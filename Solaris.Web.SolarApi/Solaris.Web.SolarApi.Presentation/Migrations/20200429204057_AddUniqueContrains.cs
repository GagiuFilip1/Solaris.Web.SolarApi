using Microsoft.EntityFrameworkCore.Migrations;

namespace Solaris.Web.SolarApi.Presentation.Migrations
{
    public partial class AddUniqueContrains : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SolarSystems_Name",
                table: "SolarSystems",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Planets_Name",
                table: "Planets",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SolarSystems_Name",
                table: "SolarSystems");

            migrationBuilder.DropIndex(
                name: "IX_Planets_Name",
                table: "Planets");
        }
    }
}
