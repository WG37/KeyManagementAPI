using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Keybytes",
                table: "Keys",
                newName: "KeyBytes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KeyBytes",
                table: "Keys",
                newName: "Keybytes");
        }
    }
}
