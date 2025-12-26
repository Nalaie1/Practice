using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Practice.API.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Regions",
                newName: "Id");

            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("6f1c2a9e-3b4d-4f8a-9c2e-1a5b7d8e0f12"), "Easy" },
                    { new Guid("8a3d5c1e-7f4b-4e2a-8d9c-0f1a2b3c4d5e"), "Medium" },
                    { new Guid("9b2e4f7a-1c5d-4a8e-b0c3-6d7e8f1a2b34"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("a1c3e5f7-2b4d-4a6e-9c8f-0d1e2b3a4c5d"), "NTH", "North", null },
                    { new Guid("b2d4f6a8-1c3e-4b5d-9f0e-7a8c1e2d3f4b"), "STH", "South", null },
                    { new Guid("c3e5a7b9-2d4f-4c6e-8a1b-0f2d3e4c5a6b"), "WST", "West", null },
                    { new Guid("d6b6a0e7-8b9c-9d0e-3f1a-6b7c8d9e0123"), "EST", "East", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("6f1c2a9e-3b4d-4f8a-9c2e-1a5b7d8e0f12"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("8a3d5c1e-7f4b-4e2a-8d9c-0f1a2b3c4d5e"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("9b2e4f7a-1c5d-4a8e-b0c3-6d7e8f1a2b34"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a1c3e5f7-2b4d-4a6e-9c8f-0d1e2b3a4c5d"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("b2d4f6a8-1c3e-4b5d-9f0e-7a8c1e2d3f4b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("c3e5a7b9-2d4f-4c6e-8a1b-0f2d3e4c5a6b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d6b6a0e7-8b9c-9d0e-3f1a-6b7c8d9e0123"));

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Regions",
                newName: "id");
        }
    }
}
