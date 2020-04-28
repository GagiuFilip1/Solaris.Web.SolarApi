using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Solaris.Web.SolarApi.Presentation.Migrations
{
    public partial class MainModelsInitialisation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "SolarSystems",
                table => new
                {
                    Id = table.Column<Guid>(),
                    SpacePosition = table.Column<string>(),
                    Name = table.Column<string>(nullable: true),
                    DistanceToEarth = table.Column<long>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolarSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                "Planets",
                table => new
                {
                    Id = table.Column<Guid>(),
                    Name = table.Column<string>(nullable: true),
                    TemperatureNight = table.Column<float>(),
                    TemperatureDay = table.Column<float>(),
                    WaterPercentage = table.Column<float>(),
                    OxygenPercentage = table.Column<float>(),
                    GravityForce = table.Column<float>(),
                    PlanetRadius = table.Column<double>(),
                    PlanetSurfaceMagneticField = table.Column<double>(),
                    SolarWindVelocity = table.Column<double>(),
                    SpinFrequency = table.Column<float>(),
                    PlanetStatus = table.Column<byte>(),
                    Description = table.Column<string>(nullable: true),
                    SolarSystemId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planets", x => x.Id);
                    table.ForeignKey(
                        "FK_Planets_SolarSystems_SolarSystemId",
                        x => x.SolarSystemId,
                        "SolarSystems",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Planets_SolarSystemId",
                "Planets",
                "SolarSystemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Planets");

            migrationBuilder.DropTable(
                "SolarSystems");
        }
    }
}
