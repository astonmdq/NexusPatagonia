using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusPatagonia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Active", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("b22c1935-7aad-4d72-88e0-9e11c28f2a12"), 0, true, "53d52027-711f-4191-9a63-9c4bef9ea644", new DateTime(2026, 4, 28, 22, 42, 31, 523, DateTimeKind.Utc).AddTicks(1612), "admin@nexuspatagonia.com", true, false, null, "admin@nexuspatagonia.com", "ADMIN@NEXUSPATAGONIA.COM", "ADMIN@NEXUSPATAGONIA.COM", "AQAAAAIAAYagAAAAEEAduXhXXY0drjK5I3m3gV8fD0/29AP+kleXixOu7t06UW6AT8VvMq8hJpH9hIrOGA==", null, false, "User", "0bd8a2fe-38a5-4f07-b62a-723ada53555c", false, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b22c1935-7aad-4d72-88e0-9e11c28f2a12"));
        }
    }
}
