using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStudyYearUsersNav : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_StudyYears_StudyYearId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StudyYearId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StudyYearId",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudyYearId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StudyYearId",
                table: "AspNetUsers",
                column: "StudyYearId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_StudyYears_StudyYearId",
                table: "AspNetUsers",
                column: "StudyYearId",
                principalTable: "StudyYears",
                principalColumn: "Id");
        }
    }
}
