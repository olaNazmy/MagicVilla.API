using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedVillas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreationDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Pool, Wi-Fi, Breakfast", new DateTime(2025, 8, 9, 14, 55, 2, 710, DateTimeKind.Local).AddTicks(8485), "A luxurious royal villa with ocean views.", "https://dotnetmastery.com/bluevillaimages/villa1.jpg", "Royal Villa", 4, 200.0, 550, new DateTime(2025, 8, 9, 14, 55, 2, 736, DateTimeKind.Local).AddTicks(7036) },
                    { 2, "Pool, Beach, Wi-Fi", new DateTime(2025, 8, 9, 14, 55, 2, 736, DateTimeKind.Local).AddTicks(7539), "Spacious villa with a private pool and beach access.", "https://dotnetmastery.com/bluevillaimages/villa2.jpg", "Premium Pool Villa", 5, 300.0, 600, new DateTime(2025, 8, 9, 14, 55, 2, 736, DateTimeKind.Local).AddTicks(7550) },
                    { 3, "Beach, Pool, Wi-Fi, Breakfast", new DateTime(2025, 8, 9, 14, 55, 2, 736, DateTimeKind.Local).AddTicks(7557), "Exclusive beachfront villa with modern amenities.", "https://dotnetmastery.com/bluevillaimages/villa3.jpg", "Luxury Beachfront Villa", 6, 400.0, 750, new DateTime(2025, 8, 9, 14, 55, 2, 736, DateTimeKind.Local).AddTicks(7559) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
