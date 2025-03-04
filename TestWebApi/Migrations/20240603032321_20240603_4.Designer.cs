﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestWebApi.DataContext;

#nullable disable

namespace TestWebApi.Migrations
{
    [DbContext(typeof(TestDbContext))]
    [Migration("20240603032321_20240603_4")]
    partial class _20240603_4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("TestWebApi.DataContext.Models.ApiLog", b =>
                {
                    b.Property<int>("logNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0)
                        .HasComment("로그순번");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("logNo"));

                    b.Property<string>("action")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnOrder(3)
                        .HasComment("action");

                    b.Property<string>("controllerName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnOrder(2)
                        .HasComment("controller name");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("datetime(6)")
                        .HasComment("최초등록일시");

                    b.Property<string>("createdBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("최초입력프로그램명");

                    b.Property<string>("createdId")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasComment("최초입력자");

                    b.Property<string>("createdIp")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("최초입력ip주소");

                    b.Property<TimeSpan?>("elapsedSec")
                        .HasColumnType("time(6)")
                        .HasColumnOrder(1)
                        .HasComment("경과시간");

                    b.Property<bool>("isValid")
                        .HasColumnType("tinyint(1)")
                        .HasComment("사용여부");

                    b.Property<string>("method")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnOrder(5)
                        .HasComment("method");

                    b.Property<string>("path")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnOrder(4)
                        .HasComment("path");

                    b.Property<string>("reqContents")
                        .HasColumnType("longtext")
                        .HasColumnOrder(8)
                        .HasComment("reqContents");

                    b.Property<string>("resContents")
                        .HasColumnType("longtext")
                        .HasColumnOrder(9)
                        .HasComment("resContents");

                    b.Property<int?>("statusCode")
                        .HasMaxLength(20)
                        .HasColumnType("int")
                        .HasColumnOrder(6)
                        .HasComment("http result");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("datetime(6)")
                        .HasComment("최종수정일시");

                    b.Property<string>("updatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("최종수정프로그램명");

                    b.Property<string>("updatedId")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasComment("최종수정자");

                    b.Property<string>("updatedIp")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("최종입력ip주소");

                    b.Property<string>("userId")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnOrder(7)
                        .HasComment("사용자아이디");

                    b.HasKey("logNo");

                    b.HasIndex("createdAt");

                    b.ToTable("ApiLogs", t =>
                        {
                            t.HasComment("api로그");
                        });
                });

            modelBuilder.Entity("TestWebApi.DataContext.Models.ExceptionLog", b =>
                {
                    b.Property<int>("logNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0)
                        .HasComment("로그순번");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("logNo"));

                    b.Property<string>("actionName")
                        .HasColumnType("longtext")
                        .HasColumnOrder(2)
                        .HasComment("action");

                    b.Property<string>("controllerName")
                        .HasColumnType("longtext")
                        .HasColumnOrder(1)
                        .HasComment("controller name");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("datetime(6)")
                        .HasComment("최초등록일시");

                    b.Property<string>("createdBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("최초입력프로그램명");

                    b.Property<string>("createdId")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasComment("최초입력자");

                    b.Property<string>("createdIp")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("최초입력ip주소");

                    b.Property<bool>("isValid")
                        .HasColumnType("tinyint(1)")
                        .HasComment("사용여부");

                    b.Property<string>("message")
                        .HasColumnType("longtext")
                        .HasColumnOrder(4)
                        .HasComment("message");

                    b.Property<string>("methodName")
                        .HasColumnType("longtext")
                        .HasColumnOrder(3)
                        .HasComment("methodName");

                    b.Property<string>("stackTrace")
                        .HasColumnType("longtext")
                        .HasColumnOrder(5)
                        .HasComment("stackTrace");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("datetime(6)")
                        .HasComment("최종수정일시");

                    b.Property<string>("updatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("최종수정프로그램명");

                    b.Property<string>("updatedId")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasComment("최종수정자");

                    b.Property<string>("updatedIp")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("최종입력ip주소");

                    b.HasKey("logNo");

                    b.ToTable("ExceptionLogs", t =>
                        {
                            t.HasComment("Exception로그");
                        });
                });

            modelBuilder.Entity("TestWebApi.DataContext.Models.RefreshToken", b =>
                {
                    b.Property<string>("userId")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasComment("아이디");

                    b.Property<string>("refreshToken")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasComment("refreshToken");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("datetime(6)")
                        .HasComment("최초등록일시");

                    b.Property<string>("createdBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("최초입력프로그램명");

                    b.Property<string>("createdId")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasComment("최초입력자");

                    b.Property<string>("createdIp")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("최초입력ip주소");

                    b.Property<bool>("isValid")
                        .HasColumnType("tinyint(1)")
                        .HasComment("사용여부");

                    b.Property<DateTime>("refreshTokenExpiryTime")
                        .HasColumnType("datetime(6)")
                        .HasComment("토큰유효일시");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("datetime(6)")
                        .HasComment("최종수정일시");

                    b.Property<string>("updatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("최종수정프로그램명");

                    b.Property<string>("updatedId")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasComment("최종수정자");

                    b.Property<string>("updatedIp")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("최종입력ip주소");

                    b.HasKey("userId", "refreshToken");

                    b.HasIndex("refreshToken");

                    b.ToTable("RefreshTokens", t =>
                        {
                            t.HasComment("Refresh Token 정보");
                        });
                });

            modelBuilder.Entity("TestWebApi.DataContext.Models.SchedulerLog", b =>
                {
                    b.Property<int>("jobNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("작업번호");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("jobNo"));

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("datetime(6)")
                        .HasComment("최초등록일시");

                    b.Property<string>("createdBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("최초입력프로그램명");

                    b.Property<string>("createdId")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasComment("최초입력자");

                    b.Property<string>("createdIp")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("최초입력ip주소");

                    b.Property<bool>("isValid")
                        .HasColumnType("tinyint(1)")
                        .HasComment("사용여부");

                    b.Property<string>("jobType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("작업타입");

                    b.Property<string>("remarks")
                        .HasColumnType("longtext")
                        .HasComment("비고");

                    b.Property<string>("resultType")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasComment("작업결과타입");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("datetime(6)")
                        .HasComment("최종수정일시");

                    b.Property<string>("updatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("최종수정프로그램명");

                    b.Property<string>("updatedId")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasComment("최종수정자");

                    b.Property<string>("updatedIp")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("최종입력ip주소");

                    b.HasKey("jobNo");

                    b.HasIndex("createdAt");

                    b.ToTable("SchedulerLogs", t =>
                        {
                            t.HasComment("스케줄러 로그");
                        });
                });

            modelBuilder.Entity("TestWebApi.DataContext.Models.Todo", b =>
                {
                    b.Property<int>("todoNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("투두번호");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("todoNo"));

                    b.Property<string>("contents")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasComment("콘텐츠");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("datetime(6)")
                        .HasComment("최초등록일시");

                    b.Property<string>("createdBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("최초입력프로그램명");

                    b.Property<string>("createdId")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasComment("최초입력자");

                    b.Property<string>("createdIp")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("최초입력ip주소");

                    b.Property<bool>("isValid")
                        .HasColumnType("tinyint(1)")
                        .HasComment("사용여부");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasComment("타이틀");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("datetime(6)")
                        .HasComment("최종수정일시");

                    b.Property<string>("updatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("최종수정프로그램명");

                    b.Property<string>("updatedId")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasComment("최종수정자");

                    b.Property<string>("updatedIp")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("최종입력ip주소");

                    b.Property<string>("userId")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasComment("사용자 아이디");

                    b.HasKey("todoNo");

                    b.HasIndex("userId");

                    b.HasIndex("todoNo", "createdAt");

                    b.ToTable("Todos", t =>
                        {
                            t.HasComment("투두");
                        });
                });

            modelBuilder.Entity("TestWebApi.DataContext.Models.User", b =>
                {
                    b.Property<string>("userId")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasComment("아이디");

                    b.Property<string>("address1")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasComment("주소1");

                    b.Property<string>("address2")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasComment("주소2");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("datetime(6)")
                        .HasComment("최초등록일시");

                    b.Property<string>("createdBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("최초입력프로그램명");

                    b.Property<string>("createdId")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasComment("최초입력자");

                    b.Property<string>("createdIp")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("최초입력ip주소");

                    b.Property<string>("email")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("이메일");

                    b.Property<bool>("isValid")
                        .HasColumnType("tinyint(1)")
                        .HasComment("사용여부");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasComment("비밀번호");

                    b.Property<string>("phoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasComment("전화번호");

                    b.Property<int>("totalTodos")
                        .HasColumnType("int")
                        .HasComment("투두토탈");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("datetime(6)")
                        .HasComment("최종수정일시");

                    b.Property<string>("updatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("최종수정프로그램명");

                    b.Property<string>("updatedId")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasComment("최종수정자");

                    b.Property<string>("updatedIp")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("최종입력ip주소");

                    b.Property<string>("userName")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasComment("이름");

                    b.Property<string>("zipCode")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasComment("우편번호");

                    b.HasKey("userId");

                    b.ToTable("Users", t =>
                        {
                            t.HasComment("사용자 정보");
                        });
                });

            modelBuilder.Entity("TestWebApi.DataContext.Models.RefreshToken", b =>
                {
                    b.HasOne("TestWebApi.DataContext.Models.User", "user")
                        .WithMany("refreshTokens")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("TestWebApi.DataContext.Models.Todo", b =>
                {
                    b.HasOne("TestWebApi.DataContext.Models.User", "user")
                        .WithMany("todos")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("TestWebApi.DataContext.Models.User", b =>
                {
                    b.Navigation("refreshTokens");

                    b.Navigation("todos");
                });
#pragma warning restore 612, 618
        }
    }
}
