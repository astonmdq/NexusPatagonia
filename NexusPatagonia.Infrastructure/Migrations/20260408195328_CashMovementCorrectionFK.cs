using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusPatagonia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CashMovementCorrectionFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashMovements_Subcategories_SubcategordyId",
                table: "CashMovements");

            migrationBuilder.DropIndex(
                name: "IX_CashMovements_SubcategordyId",
                table: "CashMovements");

            migrationBuilder.DropColumn(
                name: "SubcategordyId",
                table: "CashMovements");

            migrationBuilder.CreateIndex(
                name: "IX_CashMovements_SubcategoryId",
                table: "CashMovements",
                column: "SubcategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashMovements_Subcategories_SubcategoryId",
                table: "CashMovements",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashMovements_Subcategories_SubcategoryId",
                table: "CashMovements");

            migrationBuilder.DropIndex(
                name: "IX_CashMovements_SubcategoryId",
                table: "CashMovements");

            migrationBuilder.AddColumn<Guid>(
                name: "SubcategordyId",
                table: "CashMovements",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_CashMovements_SubcategordyId",
                table: "CashMovements",
                column: "SubcategordyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashMovements_Subcategories_SubcategordyId",
                table: "CashMovements",
                column: "SubcategordyId",
                principalTable: "Subcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
