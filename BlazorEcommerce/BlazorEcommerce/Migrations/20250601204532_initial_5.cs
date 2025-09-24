using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlazorEcommerce.Migrations
{
    /// <inheritdoc />
    public partial class initial_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bb2b129d-9ae2-4c99-9fcd-7f72f774a2d2", "bb2b129d-9ae2-4c99-9fcd-7f72f774a2d2", "User", "USER" },
                    { "d60c5b44-34e7-49ef-a0c5-763a98b6e74f", "d60c5b44-34e7-49ef-a0c5-763a98b6e74f", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb2b129d-9ae2-4c99-9fcd-7f72f774a2d2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d60c5b44-34e7-49ef-a0c5-763a98b6e74f");
        }
    }
}
