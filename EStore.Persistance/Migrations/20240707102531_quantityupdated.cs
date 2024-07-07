using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EStore.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class quantityupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                value: new DateTime(2024, 7, 7, 14, 25, 31, 128, DateTimeKind.Local).AddTicks(339));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 7, 14, 25, 31, 128, DateTimeKind.Local).AddTicks(352));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 7, 14, 25, 31, 128, DateTimeKind.Local).AddTicks(354));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 7, 14, 25, 31, 128, DateTimeKind.Local).AddTicks(356));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 76, 175, 238, 145, 86, 100, 210, 255, 214, 185, 197, 35, 215, 243, 76, 141, 92, 148, 93, 48, 103, 224, 174, 247, 151, 230, 40, 147, 147, 163, 91, 50 }, new byte[] { 81, 150, 93, 133, 41, 242, 222, 174, 104, 194, 162, 33, 16, 157, 83, 253, 109, 216, 54, 125, 89, 255, 180, 228, 194, 37, 168, 213, 39, 29, 91, 223, 248, 194, 124, 222, 154, 47, 84, 90, 137, 216, 123, 89, 245, 87, 76, 35, 227, 109, 223, 117, 88, 57, 159, 103, 140, 249, 205, 155, 135, 55, 75, 142 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
