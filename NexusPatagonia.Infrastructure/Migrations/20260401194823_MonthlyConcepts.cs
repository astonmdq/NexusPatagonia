using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusPatagonia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MonthlyConcepts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Concepts_Companies_CompanyId",
                table: "Concepts");

            migrationBuilder.DropIndex(
                name: "IX_Concepts_CompanyId",
                table: "Concepts");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Concepts");

            migrationBuilder.DropColumn(
                name: "Net",
                table: "Concepts");

            migrationBuilder.DropColumn(
                name: "NotTaxed",
                table: "Concepts");

            migrationBuilder.DropColumn(
                name: "Period",
                table: "Concepts");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Concepts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "ProfitReport",
                table: "Concepts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "MontlysConcepts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Period = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ConceptId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CompanyId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Net = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NonTaxable = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MontlysConcepts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MontlysConcepts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MontlysConcepts_Concepts_ConceptId",
                        column: x => x.ConceptId,
                        principalTable: "Concepts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MontlysConcepts_CompanyId",
                table: "MontlysConcepts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_MontlysConcepts_ConceptId",
                table: "MontlysConcepts",
                column: "ConceptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MontlysConcepts");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Concepts");

            migrationBuilder.DropColumn(
                name: "ProfitReport",
                table: "Concepts");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Concepts",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<decimal>(
                name: "Net",
                table: "Concepts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "NotTaxed",
                table: "Concepts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "Period",
                table: "Concepts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Concepts_CompanyId",
                table: "Concepts",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Concepts_Companies_CompanyId",
                table: "Concepts",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
