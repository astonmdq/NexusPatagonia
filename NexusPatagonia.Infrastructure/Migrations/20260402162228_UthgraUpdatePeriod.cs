using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusPatagonia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UthgraUpdatePeriod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Uthgras",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Uthgras",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Uthgras_CompanyId",
                table: "Uthgras",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Uthgras_Companies_CompanyId",
                table: "Uthgras",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uthgras_Companies_CompanyId",
                table: "Uthgras");

            migrationBuilder.DropIndex(
                name: "IX_Uthgras_CompanyId",
                table: "Uthgras");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Uthgras");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Uthgras");
        }
    }
}
