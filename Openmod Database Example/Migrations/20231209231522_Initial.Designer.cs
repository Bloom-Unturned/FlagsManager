﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OpenmodDatabaseExample.Databases;

namespace OpenmodDatabaseExample.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20231209231522_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("OpenmodDatabaseExample.Models.Players.Players", b =>
                {
                    b.Property<ulong>("SteamID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.HasKey("SteamID");

                    b.ToTable("OpenmodDatabaseExample_Servers");
                });
#pragma warning restore 612, 618
        }
    }
}
