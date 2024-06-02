using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestWebApi.Migrations
{
    /// <inheritdoc />
    public partial class _20240602_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "userName",
                table: "Users",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "이름",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldComment: "이름")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "totalTodos",
                table: "Users",
                type: "int",
                nullable: true,
                comment: "투두토탈",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "투두토탈");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "userName",
                keyValue: null,
                column: "userName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "userName",
                table: "Users",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "이름",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "이름")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "totalTodos",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "투두토탈",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "투두토탈");
        }
    }
}
