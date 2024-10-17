using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace glinoMISIS_back.Migrations
{
    /// <inheritdoc />
    public partial class _5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BirthDay",
                table: "Employees",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Wallet",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDay",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Wallet",
                table: "Employees");
        }
    }
}
