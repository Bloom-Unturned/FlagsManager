﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using FlagsManager.Databases;

namespace FlagsManager.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("OpenmodDatabaseExample.Models.Flags.Flags", b =>
                {
                    b.Property<ulong>("SteamID")
                        .HasColumnType("bigint unsigned");

                    b.Property<ushort>("id")
                        .HasColumnType("smallint unsigned");

                    b.Property<short>("value")
                        .HasColumnType("smallint");

                    b.HasKey("SteamID", "id");

                    b.ToTable("Flags");
                });

            modelBuilder.Entity("OpenmodDatabaseExample.Models.Players.Doors", b =>
                {
                    b.Property<uint>("InstanceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned");

                    b.Property<ulong>("SteamID")
                        .HasColumnType("bigint unsigned");

                    b.Property<float>("rot")
                        .HasColumnType("float");

                    b.Property<float>("x")
                        .HasColumnType("float");

                    b.Property<float>("y")
                        .HasColumnType("float");

                    b.Property<float>("z")
                        .HasColumnType("float");

                    b.HasKey("InstanceID");

                    b.ToTable("Doors");
                });
#pragma warning restore 612, 618
        }
    }
}
