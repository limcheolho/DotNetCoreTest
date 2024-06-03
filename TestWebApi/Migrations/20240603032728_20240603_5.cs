using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestWebApi.Migrations
{
    /// <inheritdoc />
    public partial class _20240603_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "headerValue",
                table: "ApiLogs",
                type: "longtext",
                nullable: false,
                comment: "headerValue")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "headerValue",
                table: "ApiLogs");
        }
    }
}
