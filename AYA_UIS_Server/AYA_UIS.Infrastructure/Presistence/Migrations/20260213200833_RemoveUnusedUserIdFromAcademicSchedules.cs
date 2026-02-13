using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnusedUserIdFromAcademicSchedules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSchedules_User_UserId",
                table: "AcademicSchedules");

            migrationBuilder.DropIndex(
                name: "IX_AcademicSchedules_UserId",
                table: "AcademicSchedules");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AcademicSchedules");

            // Also remove from CourseUploads table which has the same issue
            migrationBuilder.DropForeignKey(
                name: "FK_CourseUploads_User_UserId",
                table: "CourseUploads");

            migrationBuilder.DropIndex(
                name: "IX_CourseUploads_UserId",
                table: "CourseUploads");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CourseUploads");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AcademicSchedules",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CourseUploads",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSchedules_UserId",
                table: "AcademicSchedules",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseUploads_UserId",
                table: "CourseUploads",
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
        }
    }
}
