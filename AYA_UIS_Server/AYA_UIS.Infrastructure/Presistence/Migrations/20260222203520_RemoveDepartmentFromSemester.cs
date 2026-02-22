using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDepartmentFromSemester : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Departments_DepartmentId",
                table: "Semesters");

            migrationBuilder.DropIndex(
                name: "IX_Semesters_DepartmentId",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Semesters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Semesters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_DepartmentId",
                table: "Semesters",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Departments_DepartmentId",
                table: "Semesters",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
