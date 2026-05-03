using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusPatagonia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b22c1935-7aad-4d72-88e0-9e11c28f2a14"),
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cea728ba-85fa-4732-8f0e-325337a172c7", new DateTime(2026, 4, 29, 14, 22, 56, 477, DateTimeKind.Utc).AddTicks(2198), "$2a$11$vB..BHOMHgyjmub0r4XM6ONVJk6JkSOogPsDWHxEuqwm1YIm7u5V6", "ac26bf64-16f2-4693-997c-55b128af52d5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b22c1935-7aad-4d72-88e0-9e11c28f2a14"),
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b06838db-dd6c-41dc-8b7c-95bc18f37ff8", new DateTime(2026, 4, 29, 14, 22, 8, 389, DateTimeKind.Utc).AddTicks(1365), "$2a$11$B8qFg12ejZzkziE9jzfDxOv6.wRQpZpolb3reBLzR2RnRaCrd8zhG", "d1d31c41-5c85-49f1-a650-8ddb7ba7ff0d" });
        }
    }
}
