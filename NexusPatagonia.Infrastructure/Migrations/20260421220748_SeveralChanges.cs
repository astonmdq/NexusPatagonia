using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusPatagonia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeveralChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "File",
                table: "Employees",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "CashMovements",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "CheckingAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EmployeeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Period = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckingAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckingAccounts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProfitabilityReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Period = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SalesRevenue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Purhcases = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnvoucheredExpenses = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyServiceFeeA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyServiceFeeB = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalariesAndPayrollTaxes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrossReceiptsTaxExpense = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfitabilityReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfitabilityReports_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SalesInvoice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InternalSalesInvoice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreditNote = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DebitNote = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InternalSalesCreditNote = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InternalSalesDebitNote = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Period = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CashMovements_CompanyId",
                table: "CashMovements",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckingAccounts_EmployeeId",
                table: "CheckingAccounts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfitabilityReports_CompanyId",
                table: "ProfitabilityReports",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CompanyId",
                table: "Sales",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashMovements_Companies_CompanyId",
                table: "CashMovements",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashMovements_Companies_CompanyId",
                table: "CashMovements");

            migrationBuilder.DropTable(
                name: "CheckingAccounts");

            migrationBuilder.DropTable(
                name: "ProfitabilityReports");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_CashMovements_CompanyId",
                table: "CashMovements");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "CashMovements");

            migrationBuilder.AlterColumn<string>(
                name: "File",
                table: "Employees",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
