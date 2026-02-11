using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Migrations.IdentityAYADb
{
    /// <inheritdoc />
    public partial class AddUserSemesterGPAsNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AllowedCredits",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalCredits",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TotalGPA",
                table: "AspNetUsers",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowedCredits",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TotalCredits",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TotalGPA",
                table: "AspNetUsers");
        }
    }
}
