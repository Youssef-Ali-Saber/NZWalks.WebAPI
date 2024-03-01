using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class seedRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "PhotoUrl" },
                values: new object[,]
                {
                    { new Guid("132d1ab2-c55e-4366-b7bb-0e818a2c5c28"), "AKL", "Auckland", "yyyyyy.jpg" },
                    { new Guid("3e0d9932-b554-4e37-b49b-63f0eb702e14"), "AKL", "Auckland", "yyyyyy.jpg" },
                    { new Guid("5c593238-ce60-447a-bc52-9edfe7287d55"), "AKL", "Auckland", "yyyyyy.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("132d1ab2-c55e-4366-b7bb-0e818a2c5c28"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("3e0d9932-b554-4e37-b49b-63f0eb702e14"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("5c593238-ce60-447a-bc52-9edfe7287d55"));
        }
    }
}
