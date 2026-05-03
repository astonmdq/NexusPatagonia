using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusPatagonia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b22c1935-7aad-4d72-88e0-9e11c28f2a12"),
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "40f3d4b8-c6da-43fb-89c7-be6fd1f430db", new DateTime(2026, 4, 29, 12, 49, 20, 263, DateTimeKind.Utc).AddTicks(9367), "AQAAAAIAAYagAAAAEARwvaKbpOj5cnczEebvm/uZkl15N4QN16fJ1UPvDWa1PTn8ZGQxgCRZGU2fsRqKBg==", "8b3efd54-302f-4c37-8e5a-b914408d9cce", "admin@nexuspatagonia.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b22c1935-7aad-4d72-88e0-9e11c28f2a12"),
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "53d52027-711f-4191-9a63-9c4bef9ea644", new DateTime(2026, 4, 28, 22, 42, 31, 523, DateTimeKind.Utc).AddTicks(1612), "AQAAAAIAAYagAAAAEEAduXhXXY0drjK5I3m3gV8fD0/29AP+kleXixOu7t06UW6AT8VvMq8hJpH9hIrOGA==", "0bd8a2fe-38a5-4f07-b62a-723ada53555c", null });
        }
    }
}
