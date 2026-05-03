using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusPatagonia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CategoryEmployeeRelatedUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmployeeRelated",
                table: "Categories",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeRelated",
                table: "Categories");
        }
    }
}
