using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBAcess.Migrations
{
    /// <inheritdoc />
    public partial class _change_table_video : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_LearningMethods_LearningMethodId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_LearningMethodId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "LearningMethodId",
                table: "Videos");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_LessonMethodId",
                table: "Videos",
                column: "LessonMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_LessonMethods_LessonMethodId",
                table: "Videos",
                column: "LessonMethodId",
                principalTable: "LessonMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_LessonMethods_LessonMethodId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_LessonMethodId",
                table: "Videos");

            migrationBuilder.AddColumn<int>(
                name: "LearningMethodId",
                table: "Videos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Videos_LearningMethodId",
                table: "Videos",
                column: "LearningMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_LearningMethods_LearningMethodId",
                table: "Videos",
                column: "LearningMethodId",
                principalTable: "LearningMethods",
                principalColumn: "Id");
        }
    }
}
