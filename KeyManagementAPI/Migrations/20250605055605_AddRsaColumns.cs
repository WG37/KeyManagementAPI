using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRsaColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Algorithm",
                table: "Keys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "PrivKey",
                table: "Keys",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PubKey",
                table: "Keys",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Algorithm",
                table: "Keys");

            migrationBuilder.DropColumn(
                name: "PrivKey",
                table: "Keys");

            migrationBuilder.DropColumn(
                name: "PubKey",
                table: "Keys");
        }
    }
}
