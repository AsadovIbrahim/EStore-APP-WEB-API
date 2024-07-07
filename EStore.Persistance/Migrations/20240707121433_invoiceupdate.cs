using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EStore.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class invoiceupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Barcode",
                table: "Invoices",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 7, 16, 14, 32, 691, DateTimeKind.Local).AddTicks(8414));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 7, 16, 14, 32, 691, DateTimeKind.Local).AddTicks(8432));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 7, 16, 14, 32, 691, DateTimeKind.Local).AddTicks(8436));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 7, 16, 14, 32, 691, DateTimeKind.Local).AddTicks(8439));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 209, 207, 153, 83, 90, 176, 61, 155, 59, 218, 184, 123, 41, 122, 14, 14, 193, 113, 106, 110, 112, 166, 77, 249, 65, 170, 39, 149, 183, 147, 128, 228 }, new byte[] { 230, 17, 137, 233, 0, 17, 159, 246, 115, 29, 159, 65, 213, 25, 115, 73, 124, 37, 183, 30, 19, 109, 154, 201, 173, 84, 243, 79, 110, 184, 223, 145, 108, 22, 13, 132, 166, 199, 156, 42, 119, 132, 169, 204, 209, 178, 34, 227, 146, 85, 178, 185, 224, 27, 241, 252, 250, 24, 218, 16, 106, 16, 244, 35 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

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
    }
}
