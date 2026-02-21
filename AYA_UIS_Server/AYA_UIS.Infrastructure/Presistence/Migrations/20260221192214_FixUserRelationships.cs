using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Migrations
{
    /// <inheritdoc />
    public partial class FixUserRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseUploads_AspNetUsers_UserId",
                table: "CourseUploads");

            migrationBuilder.DropIndex(
                name: "IX_CourseUploads_UserId",
                table: "CourseUploads");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CourseUploads");

            migrationBuilder.CreateIndex(
                name: "IX_CourseUploads_UploadedByUserId",
                table: "CourseUploads",
                column: "UploadedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUploads_AspNetUsers_UploadedByUserId",
                table: "CourseUploads",
                column: "UploadedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseUploads_AspNetUsers_UploadedByUserId",
                table: "CourseUploads");

            migrationBuilder.DropIndex(
                name: "IX_CourseUploads_UploadedByUserId",
                table: "CourseUploads");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CourseUploads",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseUploads_UserId",
                table: "CourseUploads",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUploads_AspNetUsers_UserId",
                table: "CourseUploads",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
