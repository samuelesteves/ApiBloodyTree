using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiBloodyTree.Migrations
{
    /// <inheritdoc />
    public partial class _002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrupoSanguineo",
                table: "Membros");

            migrationBuilder.AddColumn<double>(
                name: "GrupoA",
                table: "Membros",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "GrupoB",
                table: "Membros",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "GrupoO",
                table: "Membros",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrupoA",
                table: "Membros");

            migrationBuilder.DropColumn(
                name: "GrupoB",
                table: "Membros");

            migrationBuilder.DropColumn(
                name: "GrupoO",
                table: "Membros");

            migrationBuilder.AddColumn<string>(
                name: "GrupoSanguineo",
                table: "Membros",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
