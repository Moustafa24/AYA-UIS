using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelsAndRemoveUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop all foreign key constraints that reference the User table
            migrationBuilder.Sql(@"
                DECLARE @sql NVARCHAR(MAX) = N'';
                
                SELECT @sql += N'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id)) + '.' 
                    + QUOTENAME(OBJECT_NAME(parent_object_id)) 
                    + ' DROP CONSTRAINT ' + QUOTENAME(name) + ';'
                FROM sys.foreign_keys
                WHERE referenced_object_id = OBJECT_ID('User');
                
                EXEC sp_executesql @sql;
            ");
            
            // Drop foreign keys FROM User table
            migrationBuilder.Sql(@"
                DECLARE @sql NVARCHAR(MAX) = N'';
                
                SELECT @sql += N'ALTER TABLE [User] DROP CONSTRAINT ' + QUOTENAME(name) + ';'
                FROM sys.foreign_keys
                WHERE parent_object_id = OBJECT_ID(' User');
                
                EXEC sp_executesql @sql;
            ");

            // Drop the User table - it should only exist in Identity database
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.AddColumn<bool>(
                name: "IsPassed",
                table: "Registrations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPassed",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Courses");

            // Note: User table recreation omitted - should not exist in Info DB
        }
    }
}
