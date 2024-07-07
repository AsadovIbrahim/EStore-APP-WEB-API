using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EStore.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddedRefund : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 7, 12, 6, 16, 484, DateTimeKind.Local).AddTicks(2640));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 7, 12, 6, 16, 484, DateTimeKind.Local).AddTicks(2658));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 7, 12, 6, 16, 484, DateTimeKind.Local).AddTicks(2661));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 7, 12, 6, 16, 484, DateTimeKind.Local).AddTicks(2663));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 172, 216, 45, 234, 253, 245, 79, 8, 163, 196, 184, 255, 50, 95, 21, 82, 69, 135, 208, 241, 160, 80, 231, 171, 208, 178, 245, 211, 162, 27, 83, 26 }, new byte[] { 89, 239, 176, 131, 198, 121, 167, 139, 195, 54, 162, 11, 102, 180, 79, 137, 124, 219, 221, 46, 234, 222, 170, 184, 109, 29, 141, 181, 128, 228, 66, 213, 233, 89, 16, 192, 26, 59, 72, 59, 37, 221, 56, 189, 172, 68, 183, 28, 3, 244, 184, 150, 132, 230, 150, 164, 159, 206, 250, 172, 31, 181, 113, 75 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 7, 11, 58, 17, 279, DateTimeKind.Local).AddTicks(9067));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 7, 11, 58, 17, 279, DateTimeKind.Local).AddTicks(9082));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 7, 11, 58, 17, 279, DateTimeKind.Local).AddTicks(9084));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 7, 11, 58, 17, 279, DateTimeKind.Local).AddTicks(9086));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 72, 73, 170, 219, 244, 58, 239, 147, 6, 189, 228, 159, 9, 1, 114, 33, 42, 9, 194, 54, 74, 183, 204, 62, 121, 23, 102, 147, 218, 70, 93, 18 }, new byte[] { 39, 251, 174, 240, 128, 197, 159, 171, 30, 14, 228, 74, 204, 18, 182, 140, 252, 196, 61, 62, 90, 26, 102, 162, 62, 192, 100, 38, 88, 2, 232, 238, 170, 100, 78, 48, 247, 17, 132, 240, 61, 104, 64, 216, 4, 249, 46, 120, 124, 83, 2, 40, 221, 93, 224, 91, 173, 62, 60, 205, 23, 201, 89, 97 } });
        }
    }
}
