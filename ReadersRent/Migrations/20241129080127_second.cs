using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadersRent.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rent_Reader_ReaderId_Reader",
                table: "Rent");

            migrationBuilder.DropIndex(
                name: "IX_Rent_ReaderId_Reader",
                table: "Rent");

            migrationBuilder.RenameColumn(
                name: "ReaderId_Reader",
                table: "Rent",
                newName: "ID_Book");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID_Book",
                table: "Rent",
                newName: "ReaderId_Reader");

            migrationBuilder.CreateIndex(
                name: "IX_Rent_ReaderId_Reader",
                table: "Rent",
                column: "ReaderId_Reader");

            migrationBuilder.AddForeignKey(
                name: "FK_Rent_Reader_ReaderId_Reader",
                table: "Rent",
                column: "ReaderId_Reader",
                principalTable: "Reader",
                principalColumn: "Id_Reader",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
