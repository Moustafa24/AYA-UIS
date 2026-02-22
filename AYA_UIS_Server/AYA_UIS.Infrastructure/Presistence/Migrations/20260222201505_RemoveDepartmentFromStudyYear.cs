using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDepartmentFromStudyYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyYears_Departments_DepartmentId",
                table: "StudyYears");

            migrationBuilder.DropIndex(
                name: "IX_StudyYears_DepartmentId",
                table: "StudyYears");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "StudyYears");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "StudyYears",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudyYears_DepartmentId",
                table: "StudyYears",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyYears_Departments_DepartmentId",
                table: "StudyYears",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
