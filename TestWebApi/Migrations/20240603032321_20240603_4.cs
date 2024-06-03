using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestWebApi.Migrations
{
    /// <inheritdoc />
    public partial class _20240603_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_Todos_todoNo_createdAt",
                table: "Todos",
                columns: new[] { "todoNo", "createdAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Todos_todoNo_createdAt",
                table: "Todos");

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
    }
}
