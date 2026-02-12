using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCrossDbUserForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSchedules_User_UploadedByUserId",
                table: "AcademicSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseUploads_User_UploadedByUserId",
                table: "CourseUploads");

            migrationBuilder.DropIndex(
                name: "IX_CourseUploads_UploadedByUserId",
                table: "CourseUploads");

            migrationBuilder.DropIndex(
                name: "IX_AcademicSchedules_UploadedByUserId",
                table: "AcademicSchedules");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CourseUploads",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AcademicSchedules",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_DepartmentId",
                table: "User",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseUploads_UserId",
                table: "CourseUploads",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSchedules_UserId",
                table: "AcademicSchedules",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicSchedules_User_UserId",
                table: "AcademicSchedules",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUploads_User_UserId",
                table: "CourseUploads",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Departments_DepartmentId",
                table: "User",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSchedules_User_UserId",
                table: "AcademicSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseUploads_User_UserId",
                table: "CourseUploads");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Departments_DepartmentId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_DepartmentId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_CourseUploads_UserId",
                table: "CourseUploads");

            migrationBuilder.DropIndex(
                name: "IX_AcademicSchedules_UserId",
                table: "AcademicSchedules");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CourseUploads");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AcademicSchedules");

            migrationBuilder.CreateIndex(
                name: "IX_CourseUploads_UploadedByUserId",
                table: "CourseUploads",
                column: "UploadedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSchedules_UploadedByUserId",
                table: "AcademicSchedules",
                column: "UploadedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicSchedules_User_UploadedByUserId",
                table: "AcademicSchedules",
                column: "UploadedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUploads_User_UploadedByUserId",
                table: "CourseUploads",
                column: "UploadedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
