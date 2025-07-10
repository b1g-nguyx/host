using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBAcess.Migrations
{
    /// <inheritdoc />
    public partial class _add_new_table_for_videos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LearningMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MethodName = table.Column<string>(type: "NVARCHAR", nullable: true),
                    Description = table.Column<string>(type: "NVARCHAR", nullable: true),
                    LinkImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LessonMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonId = table.Column<int>(type: "int", nullable: true),
                    LearningMethodId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonMethods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonMethods_LearningMethods_LearningMethodId",
                        column: x => x.LearningMethodId,
                        principalTable: "LearningMethods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LessonMethods_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "NVARCHAR", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    LessonMethodId = table.Column<int>(type: "int", nullable: false),
                    LearningMethodId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_LearningMethods_LearningMethodId",
                        column: x => x.LearningMethodId,
                        principalTable: "LearningMethods",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Flashcards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FrontContent = table.Column<string>(type: "NVARCHAR", nullable: true),
                    BackContent = table.Column<string>(type: "NVARCHAR", nullable: true),
                    LessonMethodId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flashcards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flashcards_LessonMethods_LessonMethodId",
                        column: x => x.LessonMethodId,
                        principalTable: "LessonMethods",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flashcards_LessonMethodId",
                table: "Flashcards",
                column: "LessonMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonMethods_LearningMethodId",
                table: "LessonMethods",
                column: "LearningMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonMethods_LessonId",
                table: "LessonMethods",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_LearningMethodId",
                table: "Videos",
                column: "LearningMethodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flashcards");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "LessonMethods");

            migrationBuilder.DropTable(
                name: "LearningMethods");
        }
    }
}
