using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiBloodyTree.Migrations
{
    /// <inheritdoc />
    public partial class _005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdAscendente",
                table: "Membros",
                newName: "IdPai");

            migrationBuilder.AddColumn<int>(
                name: "IdMae",
                table: "Membros",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdMae",
                table: "Membros");

            migrationBuilder.RenameColumn(
                name: "IdPai",
                table: "Membros",
                newName: "IdAscendente");
        }
    }
}
