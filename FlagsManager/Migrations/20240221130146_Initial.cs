using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlagsManager.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flags",
                columns: table => new
                {
                    SteamID = table.Column<ulong>(nullable: false),
                    id = table.Column<ushort>(nullable: false),
                    value = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flags", x => new { x.SteamID, x.id });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
