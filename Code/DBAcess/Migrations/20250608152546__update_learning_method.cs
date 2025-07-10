using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBAcess.Migrations
{
    /// <inheritdoc />
    public partial class _update_learning_method : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "linkImage360",
                table: "HistoricalSites",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "Answers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Prompt",
                table: "AIGeneratedContent",
                type: "NVARCHAR",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Response",
                table: "AIGeneratedContent",
                type: "NVARCHAR",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "linkImage360",
                table: "HistoricalSites");

            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "Prompt",
                table: "AIGeneratedContent");

            migrationBuilder.DropColumn(
                name: "Response",
                table: "AIGeneratedContent");
        }
    }
}
