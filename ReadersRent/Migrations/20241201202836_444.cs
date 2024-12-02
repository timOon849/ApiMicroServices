using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadersRent.Migrations
{
    /// <inheritdoc />
    public partial class _444 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReaderBook",
                columns: table => new
                {
                    ID_ReaderBook = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Book = table.Column<int>(type: "int", nullable: false),
                    ID_Reader = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReaderBook", x => x.ID_ReaderBook);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rent_ID_Reader",
                table: "Rent",
                column: "ID_Reader");

            migrationBuilder.AddForeignKey(
                name: "FK_Rent_Reader_ID_Reader",
                table: "Rent",
                column: "ID_Reader",
                principalTable: "Reader",
                principalColumn: "Id_Reader",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rent_Reader_ID_Reader",
                table: "Rent");

            migrationBuilder.DropTable(
                name: "ReaderBook");

            migrationBuilder.DropIndex(
                name: "IX_Rent_ID_Reader",
                table: "Rent");
        }
    }
}
