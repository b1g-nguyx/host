using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBAcess.Migrations
{
    /// <inheritdoc />
    public partial class _update_course_image_base64 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageBase64",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageBase64",
                table: "Courses");
        }
    }
}
