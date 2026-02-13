using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Migrations
{
    /// <inheritdoc />
    public partial class FixAcademicSchedule_StudyYearFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSchedules_Semesters_SemesterId",
                table: "AcademicSchedules");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "SemesterId",
                table: "AcademicSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudyYearId",
                table: "AcademicSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSchedules_StudyYearId",
                table: "AcademicSchedules",
                column: "StudyYearId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicSchedules_Semesters_SemesterId",
                table: "AcademicSchedules",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicSchedules_StudyYears_StudyYearId",
                table: "AcademicSchedules",
                column: "StudyYearId",
                principalTable: "StudyYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSchedules_Semesters_SemesterId",
                table: "AcademicSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSchedules_StudyYears_StudyYearId",
                table: "AcademicSchedules");

            migrationBuilder.DropIndex(
                name: "IX_AcademicSchedules_StudyYearId",
                table: "AcademicSchedules");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "User");

            migrationBuilder.DropColumn(
                name: "StudyYearId",
                table: "AcademicSchedules");

            migrationBuilder.AlterColumn<int>(
                name: "SemesterId",
                table: "AcademicSchedules",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicSchedules_Semesters_SemesterId",
                table: "AcademicSchedules",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id");
        }
    }
}
