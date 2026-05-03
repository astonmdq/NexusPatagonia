using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusPatagonia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b22c1935-7aad-4d72-88e0-9e11c28f2a14"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Active", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("b22c1935-7aad-4d72-88e0-9e11c28f2a14"), 0, true, "b06838db-dd6c-41dc-8b7c-95bc18f37ff8", new DateTime(2026, 4, 29, 14, 22, 8, 389, DateTimeKind.Utc).AddTicks(1365), "admin@nexuspatagonia.com", true, false, null, "admin@nexuspatagonia.com", "ADMIN@NEXUSPATAGONIA.COM", "ADMIN@NEXUSPATAGONIA.COM", "$2a$11$B8qFg12ejZzkziE9jzfDxOv6.wRQpZpolb3reBLzR2RnRaCrd8zhG", null, false, "User", "d1d31c41-5c85-49f1-a650-8ddb7ba7ff0d", false, "admin@nexuspatagonia.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b22c1935-7aad-4d72-88e0-9e11c28f2a14"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Active", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("b22c1935-7aad-4d72-88e0-9e11c28f2a13"), 0, true, "14ece1f8-be73-4db8-99f8-c08226045ef1", new DateTime(2026, 4, 29, 12, 53, 35, 220, DateTimeKind.Utc).AddTicks(1849), "admin@nexuspatagonia.com", true, false, null, "admin@nexuspatagonia.com", "ADMIN@NEXUSPATAGONIA.COM", "ADMIN@NEXUSPATAGONIA.COM", "AQAAAAIAAYagAAAAEFrpovwtjHy6WKGaTxOqo5cDIW16pYR3zxnNUV2GVogxE+HwyaCYGIIQzef5og47tA==", null, false, "User", "e8b9ae65-726f-4f50-ab68-9af0af772d33", false, "admin@nexuspatagonia.com" });
        }
    }
}
