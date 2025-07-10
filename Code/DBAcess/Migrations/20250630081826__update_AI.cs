using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBAcess.Migrations
{
    /// <inheritdoc />
    public partial class _update_AI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "AIGeneratedContent",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AIGeneratedContent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AIGeneratedContent_UserId",
                table: "AIGeneratedContent",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AIGeneratedContent_Users_UserId",
                table: "AIGeneratedContent",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AIGeneratedContent_Users_UserId",
                table: "AIGeneratedContent");

            migrationBuilder.DropIndex(
                name: "IX_AIGeneratedContent_UserId",
                table: "AIGeneratedContent");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "AIGeneratedContent");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AIGeneratedContent");
        }
    }
}
