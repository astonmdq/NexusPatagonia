using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusPatagonia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TishEntityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tishes_CompanyId",
                table: "Tishes",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tishes_Companies_CompanyId",
                table: "Tishes",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tishes_Companies_CompanyId",
                table: "Tishes");

            migrationBuilder.DropIndex(
                name: "IX_Tishes_CompanyId",
                table: "Tishes");
        }
    }
}
