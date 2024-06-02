using System;
using Microsoft.EntityFrameworkCore.Metadata;
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
                name: "ApiLogs",
                columns: table => new
                {
                    logNo = table.Column<int>(type: "int", nullable: false, comment: "로그순번")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    elapsedSec = table.Column<TimeSpan>(type: "time(6)", nullable: true, comment: "경과시간"),
                    controllerName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "controller name")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    action = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "action")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    path = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "path")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    method = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, comment: "method")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    statusCode = table.Column<int>(type: "int", maxLength: 20, nullable: true, comment: "http result"),
                    userId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "사용자아이디")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    reqContents = table.Column<string>(type: "longtext", nullable: true, comment: "reqContents")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    resContents = table.Column<string>(type: "longtext", nullable: true, comment: "resContents")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "최초등록일시"),
                    createdId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "최초입력자")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdBy = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "최초입력프로그램명")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdIp = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "최초입력ip주소")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "최종수정일시"),
                    updatedId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "최종수정자")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updatedBy = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "최종수정프로그램명")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updatedIp = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "최종입력ip주소")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isValid = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "사용여부")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiLogs", x => x.logNo);
                },
                comment: "api로그")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ExceptionLogs",
                columns: table => new
                {
                    logNo = table.Column<int>(type: "int", nullable: false, comment: "로그순번")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    controllerName = table.Column<string>(type: "longtext", nullable: true, comment: "controller name")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    actionName = table.Column<string>(type: "longtext", nullable: true, comment: "action")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    methodName = table.Column<string>(type: "longtext", nullable: true, comment: "methodName")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    message = table.Column<string>(type: "longtext", nullable: true, comment: "message")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    stackTrace = table.Column<string>(type: "longtext", nullable: true, comment: "stackTrace")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "최초등록일시"),
                    createdId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "최초입력자")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdBy = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "최초입력프로그램명")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdIp = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "최초입력ip주소")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "최종수정일시"),
                    updatedId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "최종수정자")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updatedBy = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "최종수정프로그램명")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updatedIp = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "최종입력ip주소")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isValid = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "사용여부")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExceptionLogs", x => x.logNo);
                },
                comment: "Exception로그")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SchedulerLogs",
                columns: table => new
                {
                    jobNo = table.Column<int>(type: "int", nullable: false, comment: "작업번호")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    jobType = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "작업타입")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    resultType = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, comment: "작업결과타입")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    remarks = table.Column<string>(type: "longtext", nullable: true, comment: "비고")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "최초등록일시"),
                    createdId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "최초입력자")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdBy = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "최초입력프로그램명")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdIp = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "최초입력ip주소")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "최종수정일시"),
                    updatedId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "최종수정자")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updatedBy = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "최종수정프로그램명")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updatedIp = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "최종입력ip주소")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isValid = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "사용여부")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulerLogs", x => x.jobNo);
                },
                comment: "스케줄러 로그")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, comment: "아이디")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    userName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, comment: "이름")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "이메일")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, comment: "비밀번호")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phoneNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "전화번호")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    zipCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, comment: "우편번호")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address1 = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "주소1")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address2 = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "주소2")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    totalTodos = table.Column<int>(type: "int", nullable: false, comment: "투두토탈"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "최초등록일시"),
                    createdId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "최초입력자")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdBy = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "최초입력프로그램명")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdIp = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "최초입력ip주소")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "최종수정일시"),
                    updatedId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "최종수정자")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updatedBy = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "최종수정프로그램명")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updatedIp = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "최종입력ip주소")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isValid = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "사용여부")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                },
                comment: "사용자 정보")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    refreshToken = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, comment: "refreshToken")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    userId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, comment: "아이디")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    refreshTokenExpiryTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "토큰유효일시"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "최초등록일시"),
                    createdId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "최초입력자")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdBy = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "최초입력프로그램명")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdIp = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "최초입력ip주소")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "최종수정일시"),
                    updatedId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "최종수정자")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updatedBy = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "최종수정프로그램명")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updatedIp = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "최종입력ip주소")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isValid = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "사용여부")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => new { x.userId, x.refreshToken });
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Refresh Token 정보")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    todoNo = table.Column<int>(type: "int", nullable: false, comment: "투두번호")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<string>(type: "varchar(20)", nullable: false, comment: "사용자 아이디")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "타이틀")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    contents = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "콘텐츠")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "최초등록일시"),
                    createdId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "최초입력자")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdBy = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "최초입력프로그램명")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdIp = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "최초입력ip주소")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "최종수정일시"),
                    updatedId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "최종수정자")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updatedBy = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "최종수정프로그램명")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updatedIp = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "최종입력ip주소")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isValid = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "사용여부")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.todoNo);
                    table.ForeignKey(
                        name: "FK_Todos_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "투두")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ApiLogs_createdAt",
                table: "ApiLogs",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_refreshToken",
                table: "RefreshTokens",
                column: "refreshToken");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulerLogs_createdAt",
                table: "SchedulerLogs",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_userId",
                table: "Todos",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiLogs");

            migrationBuilder.DropTable(
                name: "ExceptionLogs");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "SchedulerLogs");

            migrationBuilder.DropTable(
                name: "Todos");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
