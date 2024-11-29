using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadersRent.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reader",
                columns: table => new
                {
                    Id_Reader = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reader", x => x.Id_Reader);
                });

            migrationBuilder.CreateTable(
                name: "Rent",
                columns: table => new
                {
                    ID_Rent = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date_Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date_End = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Srok = table.Column<int>(type: "int", nullable: false),
                    ID_Reader = table.Column<int>(type: "int", nullable: false),
                    ReaderId_Reader = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rent", x => x.ID_Rent);
                    table.ForeignKey(
                        name: "FK_Rent_Reader_ReaderId_Reader",
                        column: x => x.ReaderId_Reader,
                        principalTable: "Reader",
                        principalColumn: "Id_Reader",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rent_ReaderId_Reader",
                table: "Rent",
                column: "ReaderId_Reader");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rent");

            migrationBuilder.DropTable(
                name: "Reader");
        }
    }
}
