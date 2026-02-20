using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDepartmentFeesTableAndUpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fees_DepartmentFees_DepartmentFeeId",
                table: "Fees");

            migrationBuilder.DropTable(
                name: "DepartmentFees");

            migrationBuilder.RenameColumn(
                name: "DepartmentFeeId",
                table: "Fees",
                newName: "StudyYearId");

            migrationBuilder.RenameIndex(
                name: "IX_Fees_DepartmentFeeId",
                table: "Fees",
                newName: "IX_Fees_StudyYearId");

            migrationBuilder.AlterColumn<int>(
                name: "Grade",
                table: "Registrations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Fees",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Fees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Fees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Fees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Fees_DepartmentId",
                table: "Fees",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fees_Departments_DepartmentId",
                table: "Fees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fees_StudyYears_StudyYearId",
                table: "Fees",
                column: "StudyYearId",
                principalTable: "StudyYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fees_Departments_DepartmentId",
                table: "Fees");

            migrationBuilder.DropForeignKey(
                name: "FK_Fees_StudyYears_StudyYearId",
                table: "Fees");

            migrationBuilder.DropIndex(
                name: "IX_Fees_DepartmentId",
                table: "Fees");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Fees");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Fees");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Fees");

            migrationBuilder.RenameColumn(
                name: "StudyYearId",
                table: "Fees",
                newName: "DepartmentFeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Fees_StudyYearId",
                table: "Fees",
                newName: "IX_Fees_DepartmentFeeId");

            migrationBuilder.AlterColumn<int>(
                name: "Grade",
                table: "Registrations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Fees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "DepartmentFees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    StudyYearId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentFees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentFees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepartmentFees_StudyYears_StudyYearId",
                        column: x => x.StudyYearId,
                        principalTable: "StudyYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentFees_DepartmentId",
                table: "DepartmentFees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentFees_StudyYearId",
                table: "DepartmentFees",
                column: "StudyYearId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fees_DepartmentFees_DepartmentFeeId",
                table: "Fees",
                column: "DepartmentFeeId",
                principalTable: "DepartmentFees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
