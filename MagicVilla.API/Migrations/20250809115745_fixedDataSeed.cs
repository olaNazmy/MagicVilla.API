using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla.API.Migrations
{
    /// <inheritdoc />
    public partial class fixedDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreationDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 8, 9, 14, 55, 2, 710, DateTimeKind.Local).AddTicks(8485), new DateTime(2025, 8, 9, 14, 55, 2, 736, DateTimeKind.Local).AddTicks(7036) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 8, 9, 14, 55, 2, 736, DateTimeKind.Local).AddTicks(7539), new DateTime(2025, 8, 9, 14, 55, 2, 736, DateTimeKind.Local).AddTicks(7550) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreationDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 8, 9, 14, 55, 2, 736, DateTimeKind.Local).AddTicks(7557), new DateTime(2025, 8, 9, 14, 55, 2, 736, DateTimeKind.Local).AddTicks(7559) });
        }
    }
}
