using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace GeeksBank.Microservice.Core.Api.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "geeksbank");

            migrationBuilder.CreateTable(
                name: "Results",
                schema: "geeksbank",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Number1 = table.Column<int>(nullable: false),
                    Number2 = table.Column<int>(nullable: false),
                    Result = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fibonacci",
                schema: "geeksbank",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_geeksbank", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fibonacci_Id",
                schema: "geeksbank",
                table: "Fibonacci",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Results_Id",
                schema: "geeksbank",
                table: "Results",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fibonacci",
                schema: "geeksbank");

            migrationBuilder.DropTable(
                name: "Results",
                schema: "geeksbank");
        }
    }
}
