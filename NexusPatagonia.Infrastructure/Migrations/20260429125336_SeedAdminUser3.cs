using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusPatagonia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b22c1935-7aad-4d72-88e0-9e11c28f2a12"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Active", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("b22c1935-7aad-4d72-88e0-9e11c28f2a13"), 0, true, "14ece1f8-be73-4db8-99f8-c08226045ef1", new DateTime(2026, 4, 29, 12, 53, 35, 220, DateTimeKind.Utc).AddTicks(1849), "admin@nexuspatagonia.com", true, false, null, "admin@nexuspatagonia.com", "ADMIN@NEXUSPATAGONIA.COM", "ADMIN@NEXUSPATAGONIA.COM", "AQAAAAIAAYagAAAAEFrpovwtjHy6WKGaTxOqo5cDIW16pYR3zxnNUV2GVogxE+HwyaCYGIIQzef5og47tA==", null, false, "User", "e8b9ae65-726f-4f50-ab68-9af0af772d33", false, "admin@nexuspatagonia.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b22c1935-7aad-4d72-88e0-9e11c28f2a13"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Active", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("b22c1935-7aad-4d72-88e0-9e11c28f2a12"), 0, true, "40f3d4b8-c6da-43fb-89c7-be6fd1f430db", new DateTime(2026, 4, 29, 12, 49, 20, 263, DateTimeKind.Utc).AddTicks(9367), "admin@nexuspatagonia.com", true, false, null, "admin@nexuspatagonia.com", "ADMIN@NEXUSPATAGONIA.COM", "ADMIN@NEXUSPATAGONIA.COM", "AQAAAAIAAYagAAAAEARwvaKbpOj5cnczEebvm/uZkl15N4QN16fJ1UPvDWa1PTn8ZGQxgCRZGU2fsRqKBg==", null, false, "User", "8b3efd54-302f-4c37-8e5a-b914408d9cce", false, "admin@nexuspatagonia.com" });
        }
    }
}
