using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiBloodyTree.Migrations
{
    /// <inheritdoc />
    public partial class _004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PortadorNegativo",
                table: "Membros",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PortadorNegativo",
                table: "Membros");
        }
    }
}
