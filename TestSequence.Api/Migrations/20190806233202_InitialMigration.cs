using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestSequence.Api.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Credits",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StoreId = table.Column<string>(maxLength: 50, nullable: false),
                    CreditNumber = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sequences",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LastNumber = table.Column<long>(nullable: false),
                    StoreId = table.Column<string>(maxLength: 50, nullable: false),
                    Type = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sequences", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Credits_CreditNumber_StoreId",
                table: "Credits",
                columns: new[] { "CreditNumber", "StoreId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sequences_StoreId_Type",
                table: "Sequences",
                columns: new[] { "StoreId", "Type" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Credits");

            migrationBuilder.DropTable(
                name: "Sequences");
        }
    }
}
