using Microsoft.EntityFrameworkCore.Migrations;

namespace GeeksBank.Core.Api.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fibonacci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fibonacci", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Number1 = table.Column<int>(type: "int", nullable: false),
                    Number2 = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Fibonacci",
                columns: new[] { "Id", "Number" },
                values: new object[,]
                {
                    { 1, 0L },
                    { 73, 1582341984L },
                    { 72, 696897233L },
                    { 71, 885444751L },
                    { 70, -188547518L },
                    { 69, 1073992269L },
                    { 68, -1262539787L },
                    { 67, -1958435240L },
                    { 66, 695895453L },
                    { 65, 1640636603L },
                    { 64, -944741150L },
                    { 63, -1709589543L },
                    { 62, 764848393L },
                    { 61, 1820529360L },
                    { 60, -1055680967L },
                    { 59, -1418756969L },
                    { 58, 363076002L },
                    { 57, -1781832971L },
                    { 56, 2144908973L },
                    { 55, 368225352L },
                    { 54, 1776683621L },
                    { 53, -1408458269L },
                    { 74, -2015728079L },
                    { 52, -1109825406L },
                    { 75, -433386095L },
                    { 77, 1412467027L },
                    { 98, -798870975L },
                    { 97, 708252800L },
                    { 96, -1507123775L },
                    { 95, -2079590721L },
                    { 94, 572466946L },
                    { 93, 1642909629L },
                    { 92, -1070442683L },
                    { 91, -1581614984L },
                    { 90, 511172301L },
                    { 89, -2092787285L },
                    { 88, -1691007710L },
                    { 87, -401779575L },
                    { 86, -1289228135L },
                    { 85, 887448560L },
                    { 84, 2118290601L },
                    { 83, -1230842041L }
                });

            migrationBuilder.InsertData(
                table: "Fibonacci",
                columns: new[] { "Id", "Number" },
                values: new object[,]
                {
                    { 82, -945834654L },
                    { 81, -285007387L },
                    { 80, -660827267L },
                    { 79, 375819880L },
                    { 78, -1036647147L },
                    { 76, 1845853122L },
                    { 51, -298632863L },
                    { 50, -811192543L },
                    { 49, 512559680L },
                    { 22, 10946L },
                    { 21, 6765L },
                    { 20, 4181L },
                    { 19, 2584L },
                    { 18, 1597L },
                    { 17, 987L },
                    { 16, 610L },
                    { 15, 377L },
                    { 14, 233L },
                    { 13, 144L },
                    { 12, 89L },
                    { 11, 55L },
                    { 10, 34L },
                    { 9, 21L },
                    { 8, 13L },
                    { 7, 8L },
                    { 6, 5L },
                    { 5, 3L },
                    { 4, 2L },
                    { 3, 1L },
                    { 2, 1L },
                    { 23, 17711L },
                    { 24, 28657L },
                    { 25, 46368L },
                    { 26, 75025L },
                    { 48, -1323752223L },
                    { 47, 1836311903L },
                    { 46, 1134903170L },
                    { 45, 701408733L },
                    { 44, 433494437L },
                    { 43, 267914296L },
                    { 42, 165580141L },
                    { 41, 102334155L }
                });

            migrationBuilder.InsertData(
                table: "Fibonacci",
                columns: new[] { "Id", "Number" },
                values: new object[,]
                {
                    { 40, 63245986L },
                    { 39, 39088169L },
                    { 99, -90618175L },
                    { 38, 24157817L },
                    { 36, 9227465L },
                    { 35, 5702887L },
                    { 34, 3524578L },
                    { 33, 2178309L },
                    { 32, 1346269L },
                    { 31, 832040L },
                    { 30, 514229L },
                    { 29, 317811L },
                    { 28, 196418L },
                    { 27, 121393L },
                    { 37, 14930352L },
                    { 100, -889489150L }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fibonacci");

            migrationBuilder.DropTable(
                name: "Results");
        }
    }
}
