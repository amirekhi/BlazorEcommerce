using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorEcommerce.Migrations
{
    /// <inheritdoc />
    public partial class TrafficAnalytics_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "VisitLogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeviceType",
                table: "VisitLogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IPAddress",
                table: "VisitLogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PageVisited",
                table: "VisitLogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Referrer",
                table: "VisitLogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserAgent",
                table: "VisitLogs",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "VisitLogs");

            migrationBuilder.DropColumn(
                name: "DeviceType",
                table: "VisitLogs");

            migrationBuilder.DropColumn(
                name: "IPAddress",
                table: "VisitLogs");

            migrationBuilder.DropColumn(
                name: "PageVisited",
                table: "VisitLogs");

            migrationBuilder.DropColumn(
                name: "Referrer",
                table: "VisitLogs");

            migrationBuilder.DropColumn(
                name: "UserAgent",
                table: "VisitLogs");
        }
    }
}
