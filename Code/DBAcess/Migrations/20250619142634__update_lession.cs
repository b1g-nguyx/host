using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBAcess.Migrations
{
    /// <inheritdoc />
    public partial class _update_lession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Authors",
                table: "Lessons",
                type: "NVARCHAR(MAX)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "Lessons",
                type: "NVARCHAR(MAX)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Authors",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "Lessons");
        }
    }
}
