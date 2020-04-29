﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Solaris.Web.SolarApi.Infrastructure.Data;

namespace Solaris.Web.SolarApi.Presentation.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200429204057_AddUniqueContrains")]
    partial class AddUniqueContrains
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Solaris.Web.SolarApi.Core.Models.Entities.Planet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<float>("AverageSolarWindVelocity")
                        .HasColumnType("float");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<float>("GravityForce")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<float>("OxygenPercentage")
                        .HasColumnType("float");

                    b.Property<float>("PlanetRadius")
                        .HasColumnType("float");

                    b.Property<byte>("PlanetStatus")
                        .HasColumnType("tinyint unsigned");

                    b.Property<float>("PlanetSurfaceMagneticField")
                        .HasColumnType("float");

                    b.Property<Guid>("SolarSystemId")
                        .HasColumnType("char(36)");

                    b.Property<float>("SpinFrequency")
                        .HasColumnType("float");

                    b.Property<float>("TemperatureDay")
                        .HasColumnType("float");

                    b.Property<float>("TemperatureNight")
                        .HasColumnType("float");

                    b.Property<float>("WaterPercentage")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("SolarSystemId");

                    b.ToTable("Planets");
                });

            modelBuilder.Entity("Solaris.Web.SolarApi.Core.Models.Entities.SolarSystem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<float>("DistanceToEarth")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("SpacePosition")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("SolarSystems");
                });

            modelBuilder.Entity("Solaris.Web.SolarApi.Core.Models.Entities.Planet", b =>
                {
                    b.HasOne("Solaris.Web.SolarApi.Core.Models.Entities.SolarSystem", "SolarSystem")
                        .WithMany("Planets")
                        .HasForeignKey("SolarSystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
