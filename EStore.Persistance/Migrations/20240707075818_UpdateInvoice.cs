using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EStore.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Barcode",
                table: "Invoices",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 25, 14, 47, 17, 60, DateTimeKind.Local).AddTicks(6681));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 25, 14, 47, 17, 60, DateTimeKind.Local).AddTicks(6696));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 25, 14, 47, 17, 60, DateTimeKind.Local).AddTicks(6698));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 25, 14, 47, 17, 60, DateTimeKind.Local).AddTicks(6700));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 217, 145, 13, 182, 15, 53, 86, 44, 178, 78, 156, 18, 245, 207, 221, 161, 202, 226, 40, 246, 58, 64, 43, 75, 191, 51, 59, 66, 248, 66, 123, 46 }, new byte[] { 187, 147, 42, 234, 24, 54, 119, 209, 55, 245, 2, 180, 78, 251, 93, 97, 117, 134, 108, 137, 121, 15, 184, 156, 150, 192, 38, 199, 255, 197, 119, 121, 9, 155, 144, 76, 127, 55, 149, 115, 221, 179, 229, 123, 39, 231, 192, 2, 53, 108, 79, 9, 249, 194, 246, 250, 122, 63, 187, 14, 140, 20, 50, 246 } });
        }
    }
}
