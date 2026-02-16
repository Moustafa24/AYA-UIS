using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserStudyYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StudyYearId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudyYearId",
                table: "Semesters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserStudyYears",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    StudyYearId = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    EnrolledAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStudyYears", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserStudyYears_StudyYears_StudyYearId",
                        column: x => x.StudyYearId,
                        principalTable: "StudyYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserStudyYears_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_StudyYearId",
                table: "User",
                column: "StudyYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_StudyYearId",
                table: "Semesters",
                column: "StudyYearId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStudyYears_StudyYearId",
                table: "UserStudyYears",
                column: "StudyYearId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStudyYears_UserId_StudyYearId",
                table: "UserStudyYears",
                columns: new[] { "UserId", "StudyYearId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_StudyYears_StudyYearId",
                table: "Semesters",
                column: "StudyYearId",
                principalTable: "StudyYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_StudyYears_StudyYearId",
                table: "User",
                column: "StudyYearId",
                principalTable: "StudyYears",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_StudyYears_StudyYearId",
                table: "Semesters");

            migrationBuilder.DropForeignKey(
                name: "FK_User_StudyYears_StudyYearId",
                table: "User");

            migrationBuilder.DropTable(
                name: "UserStudyYears");

            migrationBuilder.DropIndex(
                name: "IX_User_StudyYearId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Semesters_StudyYearId",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "User");

            migrationBuilder.DropColumn(
                name: "StudyYearId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "StudyYearId",
                table: "Semesters");
        }
    }
}
