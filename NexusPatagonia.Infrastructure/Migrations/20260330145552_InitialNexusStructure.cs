using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusPatagonia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialNexusStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Employees_EmployeeId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyUser_Companies_CompaniesId",
                table: "CompanyUser");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyUser_Users_UsersId",
                table: "CompanyUser");

            migrationBuilder.DropIndex(
                name: "IX_Companies_EmployeeId",
                table: "Companies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyUser",
                table: "CompanyUser");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Companies");

            migrationBuilder.RenameTable(
                name: "CompanyUser",
                newName: "UserCompanies");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyUser_UsersId",
                table: "UserCompanies",
                newName: "IX_UserCompanies_UsersId");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Employees",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCompanies",
                table: "UserCompanies",
                columns: new[] { "CompaniesId", "UsersId" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyId",
                table: "Employees",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Companies_CompanyId",
                table: "Employees",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCompanies_Companies_CompaniesId",
                table: "UserCompanies",
                column: "CompaniesId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCompanies_Users_UsersId",
                table: "UserCompanies",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Companies_CompanyId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCompanies_Companies_CompaniesId",
                table: "UserCompanies");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCompanies_Users_UsersId",
                table: "UserCompanies");

            migrationBuilder.DropIndex(
                name: "IX_Employees_CompanyId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCompanies",
                table: "UserCompanies");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "UserCompanies",
                newName: "CompanyUser");

            migrationBuilder.RenameIndex(
                name: "IX_UserCompanies_UsersId",
                table: "CompanyUser",
                newName: "IX_CompanyUser_UsersId");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Companies",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyUser",
                table: "CompanyUser",
                columns: new[] { "CompaniesId", "UsersId" });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_EmployeeId",
                table: "Companies",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Employees_EmployeeId",
                table: "Companies",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyUser_Companies_CompaniesId",
                table: "CompanyUser",
                column: "CompaniesId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyUser_Users_UsersId",
                table: "CompanyUser",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
