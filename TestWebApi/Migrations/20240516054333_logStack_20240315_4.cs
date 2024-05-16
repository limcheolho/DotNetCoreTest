using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestWebApi.Migrations
{
    /// <inheritdoc />
    public partial class logStack_20240315_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    column1 = table.Column<string>(type: "varchar(255)", nullable: false, comment: "테스트 코멘트1")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    column2 = table.Column<string>(type: "longtext", nullable: false, comment: "테스트 코멘트2")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.column1);
                },
                comment: "테스트")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tests");
        }
    }
}
