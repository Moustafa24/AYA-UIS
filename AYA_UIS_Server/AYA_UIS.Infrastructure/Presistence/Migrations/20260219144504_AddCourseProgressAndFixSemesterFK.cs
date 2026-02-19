using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseProgressAndFixSemesterFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_StudyYears_StudyYearId",
                table: "Semesters");

            // Clean up invalid data: Delete Semesters with NULL or invalid StudyYearId
            migrationBuilder.Sql(@"
                DELETE FROM Semesters 
                WHERE StudyYearId IS NULL 
                   OR StudyYearId NOT IN (SELECT Id FROM StudyYears)
            ");

            migrationBuilder.AlterColumn<int>(
                name: "StudyYearId",
                table: "Semesters",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Progress",
                table: "Registrations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_StudyYears_StudyYearId",
                table: "Semesters",
                column: "StudyYearId",
                principalTable: "StudyYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_StudyYears_StudyYearId",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "Progress",
                table: "Registrations");

            migrationBuilder.AlterColumn<int>(
                name: "StudyYearId",
                table: "Semesters",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_StudyYears_StudyYearId",
                table: "Semesters",
                column: "StudyYearId",
                principalTable: "StudyYears",
                principalColumn: "Id");
        }
    }
}
