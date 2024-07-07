using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EStore.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Stock",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 7, 12, 12, 4, 235, DateTimeKind.Local).AddTicks(6487));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 7, 12, 12, 4, 235, DateTimeKind.Local).AddTicks(6500));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 7, 12, 12, 4, 235, DateTimeKind.Local).AddTicks(6501));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 7, 12, 12, 4, 235, DateTimeKind.Local).AddTicks(6503));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 94, 20, 86, 133, 241, 122, 115, 202, 124, 156, 224, 41, 254, 195, 210, 199, 161, 148, 131, 22, 143, 20, 39, 237, 162, 10, 192, 81, 163, 143, 241, 190 }, new byte[] { 137, 136, 243, 237, 248, 57, 109, 135, 176, 42, 27, 92, 33, 122, 196, 212, 225, 154, 134, 197, 177, 4, 59, 112, 22, 98, 8, 203, 51, 154, 135, 207, 148, 55, 181, 129, 251, 155, 119, 115, 159, 216, 157, 30, 18, 114, 18, 79, 231, 63, 145, 255, 208, 254, 166, 1, 115, 191, 68, 185, 16, 114, 143, 149 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Stock",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

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
    }
}
