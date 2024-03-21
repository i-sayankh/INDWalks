using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace INDWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataforDefficulties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("7d63a568-0db8-4dc8-ad25-3e9b2418cf57"), "Easy" },
                    { new Guid("976d24b9-46c5-4460-aa09-ba748114159e"), "Medium" },
                    { new Guid("a39137e0-e4ca-430b-b729-4c47e12454e5"), "Hard" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("7d63a568-0db8-4dc8-ad25-3e9b2418cf57"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("976d24b9-46c5-4460-aa09-ba748114159e"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("a39137e0-e4ca-430b-b729-4c47e12454e5"));
        }
    }
}
