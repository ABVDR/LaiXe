using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Libs.Migrations
{
    /// <inheritdoc />
    public partial class themdonhang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BaiThis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenBaiThi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaiThis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChuDes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenChuDe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuDes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DonHangs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHangs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaoDichThanhToans",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonHangId = table.Column<long>(type: "bigint", nullable: false),
                    CongThanhToan = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    MaDonCong = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    MaGiaoDichCuoi = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    ThongBaoLoi = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    NgayTao = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    NgayCapNhat = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaoDichThanhToans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LichSuThis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaiThiId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenBaiThi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayThi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TongSoCau = table.Column<int>(type: "int", nullable: false),
                    SoCauDung = table.Column<int>(type: "int", nullable: false),
                    PhanTramDung = table.Column<double>(type: "float", nullable: false),
                    Diem = table.Column<int>(type: "int", nullable: false),
                    KetQua = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MacLoiNghiemTrong = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichSuThis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiBangLais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenLoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoaiXe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGianThi = table.Column<int>(type: "int", nullable: false),
                    DiemToiThieu = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiBangLais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shares",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shares", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TinhNangMoKhoas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenTinhNang = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    DangHoatDong = table.Column<bool>(type: "bit", nullable: false),
                    KichHoatLuc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    HetHanLuc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DonHangId = table.Column<long>(type: "bigint", nullable: true),
                    SoTienDaTra = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    NgayTao = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinhNangMoKhoas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VisitLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisitorId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaiSaHinhs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TenBai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoaiBangLaiId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaiSaHinhs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaiSaHinhs_LoaiBangLais_LoaiBangLaiId",
                        column: x => x.LoaiBangLaiId,
                        principalTable: "LoaiBangLais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CauHois",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LuaChonA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LuaChonB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LuaChonC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LuaChonD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DapAnDung = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    GiaiThich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiemLiet = table.Column<bool>(type: "bit", nullable: false),
                    MediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiMedia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeoGhiNho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ChuDeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoaiBangLaiId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHois", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CauHois_ChuDes_ChuDeId",
                        column: x => x.ChuDeId,
                        principalTable: "ChuDes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CauHois_LoaiBangLais_LoaiBangLaiId",
                        column: x => x.LoaiBangLaiId,
                        principalTable: "LoaiBangLais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoPhongs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DapAn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoaiBangLaiId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoPhongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoPhongs_LoaiBangLais_LoaiBangLaiId",
                        column: x => x.LoaiBangLaiId,
                        principalTable: "LoaiBangLais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShareReplies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShareId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentReplyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShareReplies", x => x.id);
                    table.ForeignKey(
                        name: "FK_ShareReplies_Shares_ShareId",
                        column: x => x.ShareId,
                        principalTable: "Shares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CauHoiSais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CauHoiId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgaySai = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHoiSais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CauHoiSais_CauHois_CauHoiId",
                        column: x => x.CauHoiId,
                        principalTable: "CauHois",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietBaiThis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaiThiId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CauHoiId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietBaiThis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietBaiThis_BaiThis_BaiThiId",
                        column: x => x.BaiThiId,
                        principalTable: "BaiThis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietBaiThis_CauHois_CauHoiId",
                        column: x => x.CauHoiId,
                        principalTable: "CauHois",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietLichSuThis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LichSuThiId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CauHoiId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CauTraLoi = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    DungSai = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietLichSuThis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietLichSuThis_CauHois_CauHoiId",
                        column: x => x.CauHoiId,
                        principalTable: "CauHois",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietLichSuThis_LichSuThis_LichSuThiId",
                        column: x => x.LichSuThiId,
                        principalTable: "LichSuThis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShareReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShareId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShareReplyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShareReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShareReports_ShareReplies_ShareReplyId",
                        column: x => x.ShareReplyId,
                        principalTable: "ShareReplies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ShareReports_Shares_ShareId",
                        column: x => x.ShareId,
                        principalTable: "Shares",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "05f2400b-5471-466a-8b7e-27752367e4d6", null, "User", "USER" },
                    { "10f2400b-5471-466a-8b7e-27752367e4d6", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8d581a98-361e-4333-a651-74e88ef572a4", 0, "f67e2437-61a2-4458-ac14-de7ab48158b6", new DateTime(2025, 10, 31, 20, 54, 14, 0, DateTimeKind.Unspecified), "user@gmail.com", true, false, null, "USER@GMAIL.COM", "USER", "AQAAAAIAAYagAAAAEN8TWXW9pNZ+VVyeftOLixsSfyDOtPTZpv84QtbFESyzd6kZ0i70eIPvnvNBKX0Q9Q==", null, false, "DF7GIIY7UNBVCVLZD73QO6PGSVQXBSTW", false, "user" },
                    { "9ae1058d-b602-4025-ab1d-74e7bced8f3b", 0, "6e66d8c1-89da-46df-bc24-ec54c7e7e7cf", new DateTime(2025, 10, 31, 20, 53, 44, 0, DateTimeKind.Unspecified), "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEFY87mzNg88TIJtuXRcRIeT0MXYto4NkcukxwFGpl+p5IHBJVqlPbyFx9UJIOmu7eA==", null, false, "3XVVZIW5RPRWT7MKN3Y6VRNTHXY2JGK5", false, "admin" }
                });

            migrationBuilder.InsertData(
                table: "BaiThis",
                columns: new[] { "Id", "TenBaiThi" },
                values: new object[] { new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), "Đề 4" });

            migrationBuilder.InsertData(
                table: "ChuDes",
                columns: new[] { "Id", "ImageUrl", "MoTa", "TenChuDe", "isDeleted" },
                values: new object[,]
                {
                    { new Guid("1365e7dd-6325-4662-ba5a-fe0312d5519a"), "/images/cautao.png", "Cấu tạo và sửa chữa", "Cấu tạo và sửa chữa", false },
                    { new Guid("51923743-b8a3-42e8-9810-219fcffaa9ee"), "/images/vanhoa.png", "Văn hóa và đạo đức", "Văn hóa và đạo đức", false },
                    { new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "/images/warning.png", "Khái niệm và quy tắc giao thông", "Câu hỏi điểm liệt", false },
                    { new Guid("78627311-083d-42f9-a921-168e88e5f187"), "/images/30f08b72-5724-4e94-94a5-c8211d05d547.png", "Khái niệm và quy tắc", "Khái niệm và quy tắc", false },
                    { new Guid("a1720db9-1416-4e62-a922-e34364f67418"), "/images/sahinh.png", "Sa hình", "Sa hình", false },
                    { new Guid("a5674e1d-af51-4a56-bdd8-758210677c1a"), "/images/bienbao.png", "Biển báo đường bộ", "Biển báo đường bộ", false },
                    { new Guid("cdcfb0da-a22d-4891-bac4-1fea55614508"), "/images/laixe.png", "Kỹ thuật lái xe", "Kỹ thuật lái xe", false },
                    { new Guid("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"), "/images/vantai.png", "Nghiệp vụ vận tải", "Nghiệp vụ vận tải", false }
                });

            migrationBuilder.InsertData(
                table: "LoaiBangLais",
                columns: new[] { "Id", "DiemToiThieu", "LoaiXe", "MoTa", "TenLoai", "ThoiGianThi", "isDeleted" },
                values: new object[,]
                {
                    { new Guid("25edbe66-cf05-43ca-ae57-a6dfe679563a"), 23, "Xe máy", "Xe mô tô ba bánh", "A3", 19, false },
                    { new Guid("84031e45-3634-445a-9edc-fe146cb5fd20"), 23, "Xe máy", "Xe các loại máy kéo nhỏ có trọng tải đến 1000kg.", "A4", 19, false },
                    { new Guid("899712b1-bbe9-4fb6-8c7c-046b9de9e58b"), 23, "Xe máy", "Xe mô tô dung tích xy lanh từ 175 cm3 trở lên", "A2", 19, false },
                    { new Guid("94d00b92-77fa-4efd-bc92-64da47a5bd1e"), 2, "1", "1", "SAETR", 2, true },
                    { new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), 21, "Xe máy", "Xe mô tô hai bánh có dung tích xy lanh từ 50 cm3 đến dưới 175 cm3", "A1", 19, false },
                    { new Guid("bfa465f9-2b2d-4f82-9242-b30b4af0a375"), 40, "Xe oto", "Ô tô chuyên dùng có trọng tải thiết kế từ 3500 kg trở lên. Máy kéo kéo một rơ moóc  từ 3500 kg trở lên.", "C", 24, false },
                    { new Guid("de0f2329-c8c2-4736-9ea3-107239c304a1"), 30, "Xe oto", "Ô tô số tự động chở người đến 9 chỗ ngồi, Ô tô tải dưới 3.500 kg.", "B1", 20, false },
                    { new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), 35, "Xe oto", "Ô tô 4 – 9 chỗ, ô tô chuyên dùng có trọng tải thiết kế dưới 3,5 tấn", "B2", 22, false }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "05f2400b-5471-466a-8b7e-27752367e4d6", "8d581a98-361e-4333-a651-74e88ef572a4" },
                    { "10f2400b-5471-466a-8b7e-27752367e4d6", "9ae1058d-b602-4025-ab1d-74e7bced8f3b" }
                });

            migrationBuilder.InsertData(
                table: "CauHois",
                columns: new[] { "Id", "ChuDeId", "DapAnDung", "DiemLiet", "GiaiThich", "LoaiBangLaiId", "LoaiMedia", "LuaChonA", "LuaChonB", "LuaChonC", "LuaChonD", "MediaUrl", "MeoGhiNho", "NoiDung", "isDeleted" },
                values: new object[,]
                {
                    { new Guid("018d8cd1-13cc-42f9-a8e6-1c745ce6db87"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "B", true, "Xe mô tô không được mang vác vật cồng kềnh.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Được mang, vác tùy trường hợp cụ thể.", "Không được mang, vác.", "Được mang, vác nhưng phải đảm bảo an toàn.", "Được mang, vác tùy theo sức khoẻ của bản thân.", null, null, "Người ngồi trên xe mô tô hai bánh, ba bánh, xe gắn máy khi tham gia giao thông có được mang, vác vật cồng kềnh hay không?", false },
                    { new Guid("057a01a9-d102-4013-8d4e-40e580010abe"), new Guid("51923743-b8a3-42e8-9810-219fcffaa9ee"), "B", false, null, new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Thực hiện cầm máu trực tiếp.", "Thực hiện cầm máu không trực tiếp (chặn động mạch).", null, null, null, null, "Khi sơ cứu người bị tai nạn giao thông đường bộ, có vết thương chảy máu ngoài, màu đỏ tươi phun thành tia và phun mạnh khi mạch đập, bạn phải làm gì dưới đây?", false },
                    { new Guid("0c7fc479-bfa1-4f32-99c0-250934199496"), new Guid("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"), "D", false, "Cả ý 1 và ý 2 đều đúng. Bởi vì không thể chở hành khách trên mui được, nên ý 3 sai.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Đón, trả khách đúng nơi quy định, không trở hành khách trên mui, trong khoang hành lý hoặc để hành khách đu bám bên ngoài xe.", "Không chở hàng nguy hiểm, hàng có mùi hôi thối hoặc động vật, hàng hóa khác có ảnh hưởng đến sức khỏe của hành khách.", "Chở hành khách trên mui; để hàng hóa trong khoang chở khách, chở quá số người theo quy định.", "Cả ý 1 và ý 2.", null, null, "Người lái xe khách phải chấp hành những quy định nào dưới đây?", false },
                    { new Guid("0d63c479-795a-417f-8c79-7fcbd15f9ae9"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "C", true, "", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Được phép sản xuất, sử dụng khi bị mất biển số.", "Được phép mua bán, sử dụng khi bị mất biển số.", "Nghiêm cấm sản xuất, mua bán, sử dụng trái phép.", "", null, null, "Việc sản xuất, mua bán, sử dụng biển số xe cơ giới, xe máy chuyên dùng được quy định như thế nào trong Luật Giao thông đường bộ?", false },
                    { new Guid("0e29fb07-bc96-41de-be34-3a5ac5adbcb6"), new Guid("78627311-083d-42f9-a921-168e88e5f187"), "B", true, "Không được phép giao xe cho người không đủ điều kiện tham gia giao thông.", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Chỉ được thực hiện nếu đã hướng dẫn đầy đủ.", "Không được phép.", "Được phép tùy từng trường hợp.", "Chỉ được phép thực hiện với thành viên trong gia đình.", null, null, "Hành vi giao xe cơ giới, xe máy chuyên dùng cho người không đủ điều kiện để điều khiển xe tham gia giao thông có được phép hay không?", false },
                    { new Guid("0e418bb1-c0a8-429c-964d-08d8b97149bf"), new Guid("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"), "A", false, "Không lái xe liên tục quá 4 giờ.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Không quá 4 giờ.", "Không quá 6 giờ.", "Không quá 8 giờ.", "Liên tục tùy thuộc vào sức khỏe và khả năng của người lái xe.", null, null, "Thời gian làm việc của người lái xe ô tô không được lái xe liên tục quá bao nhiêu giờ trong trường hợp nào dưới đây?", false },
                    { new Guid("1395eeb5-dcaf-47fa-b4b9-46197875e73f"), new Guid("78627311-083d-42f9-a921-168e88e5f187"), "B", false, "Phần đường xe chạy là phần của đường bộ được sử dụng cho phương tiện giao thông qua lại.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), "image", "Phần mặt đường và lề đường.", "Phần đường xe chạy.", "Phần đường xe cơ giới.", null, null, null, "Phần của đường bộ được sử dụng cho các phương tiện giao thông qua lại là gì?", false },
                    { new Guid("14be8af4-5d8f-4c40-a51e-d323d23fbcd9"), new Guid("1365e7dd-6325-4662-ba5a-fe0312d5519a"), "C", false, null, new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Kính chắn gió, kính cửa phải là loại kính an toàn, bảo đảm tần nhìn cho người điều khiển; có đủ hệ thống hãm và hệ thống chuyển hướng có hiệu lực, tay lái xe ô tô ở bên trái của xe, có còi với âm lượng đúng quy chuẩn kỹ thuật.", "Có đủ đèn chiếu sáng gần và xa, đèn soi biển số, đèn báo hãm, đèn tín hiệu; có đủ bộ phận giảm thanh, giảm khói, các kết cấu phải đủ độ bền và bảo đảm tính năng vận hành ổn định.", "Cả ý 1 và ý 2.", null, null, null, "Xe ô tô tham gia giao thông đường bộ phải bảo đảm các quy định về chất lượng, an toàn kỹ thuật và bảo vệ môi trường nào ghi dưới đây?", false },
                    { new Guid("160cfe60-432a-48d8-b501-4d288bfe2d5a"), new Guid("a1720db9-1416-4e62-a922-e34364f67418"), "A", true, "ádfasdf súadf", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), "image", "1", "1", "1", null, "/images/838377c1-3ae9-4b08-89bb-7d6b1d104d67.png", "gfdsgfsd", "nội dung 2 sua", false },
                    { new Guid("1616459f-d0ba-4f82-a028-f52f596e6cdb"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "A", false, "Không được vượt khi đang phát tín hiệu ưu tiên.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Không được vượt.", "Được vượt khi đang đi trên cầu.", "Được phép vượt khi đi qua nơi giao nhau có ít phương tiện cùng tham gia giao thông.", "Được vượt khi đảm bảo an toàn.", null, null, "Bạn đang lái xe, phía trước có một xe cứu thương đang phát tín hiệu ưu tiên bạn có được phép vượt hay không?", false },
                    { new Guid("1760daa6-f038-4f65-8531-418edc1056f8"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "A", true, "Người điều khiển bị cấm sử dụng rượu, bia khi tham gia giao thông.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Người điều khiển: xe ô tô, me mô tô, xe đạp, xe gắn máy.", "Người ngồi phía sau người điều khiển xe cơ giới.", "Người đi bộ.", "Cả ý 1 và ý 2.", null, null, "Theo Luật phòng chống tác hại của rượu, bia đối tượng nào dưới đây bị cấm sử dụng rượu, bia khi tham gia giao thông?", false },
                    { new Guid("17df60b3-08c7-4804-97b1-13f6c07725a7"), new Guid("cdcfb0da-a22d-4891-bac4-1fea55614508"), "A", true, "Xe mô tô xuống dốc dài cần sử dụng cả phanh trước và phanh sau để giảm tốc độ.", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Giữ tay ga ở mức độ phù hợp, sử dụng phanh trước và phanh sau để giảm tốc độ.", "Nhả hết tay ga, tắt động cơ, sử dụng phanh trước và phanh sau để giảm tốc độ.", "Sử dụng phanh trước để giảm tốc độ kết hợp với tắt chìa khóa điện của xe.", "", null, null, "Khi điều khiển xe mô tô tay ga xuống đường dốc dài...", false },
                    { new Guid("18b0cc7f-aa5e-497c-9a7d-a158ca561315"), new Guid("51923743-b8a3-42e8-9810-219fcffaa9ee"), "C", false, null, new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Là sự hiểu biết và chấp hành nghiêm chỉnh pháp luật về giao thông; là ý thức trách nhiệm với cộng đồng khi tham gia giao thông.", "Là ứng xử có văn hóa, có tình yêu thương con người trong các tình huống không may xảy ra khi tham gia giao thông", "Cả ý 1 và ý 2.", null, null, null, "Khái niệm về văn hóa giao thông được hiểu như thế nào là đúng?", false },
                    { new Guid("1a1040db-498c-4942-9542-29df91a1f15a"), new Guid("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"), "B", false, "Không làm việc 1 ngày của lái xe quá 10 giờ.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Không quá 8 giờ.", "Không quá 10 giờ.", "Không quá 12 giờ.", "Không hạn chế tùy thuộc vào sức khỏe và khả năng của người lái xe.", null, null, "Thời gian làm việc trong một ngày của người lái xe ô tô không được vượt quá bao nhiêu giờ trong trường hợp dưới đây?", false },
                    { new Guid("1c44f24a-713c-4ebf-bbff-396c10f45693"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "A", false, "Chỉ quay đầu xe ở điểm giao cắt hoặc nơi có biển báo cho phép quay đầu.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Đi tiếp đến điểm giao cắt gần nhất hoặc nơi có biển báo cho phép quay đầu xe.", "Bấm đèn khẩn cấp và quay đầu xe từ từ bảo đảm an toàn.", "Bấm còi liên tục khi quay đầu để cảnh báo các xe khác.", "Nhờ một người ra hiệu giao thông trên đường chậm lại trước khi quay đầu.", null, null, "Bạn đang lái xe trong khu dân cư, có đông xe qua lại, nếu muốn quay đầu bạn cần làm gì để tránh ùn tắc và đảm bảo an toàn giao thông?", false },
                    { new Guid("1c6dbaba-a773-4df9-a5e2-dcfa93ce43db"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "A", true, "", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Buông cả hai tay; sử dụng xe để kéo, đẩy xe khác, vật khác; sử dụng chân chống của xe để quệt xuống đường khi xe đang chạy.", "Buông một tay; sử dụng xe để chở người hoặc hàng hóa; để chân chạm xuống đất khi khởi hành.", "Đội mũ bảo hiểm; chạy xe đúng tốc độ quy định và chấp hành đúng quy tắc giao thông đường bộ", "Chở người ngồi sau dưới 16 tuổi.", null, null, "Khi điều khiển xe mô tô hai bánh, xe mô tô ba bánh, xe gắn máy, những hành vi nào không được phép?", false },
                    { new Guid("2008b29d-79af-4dad-83d8-30b5098afc26"), new Guid("51923743-b8a3-42e8-9810-219fcffaa9ee"), "C", false, null, new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Cho xe chạy thật nhanh qua vũng nước.", "Giảm tốc độ cho xe chạy chậm qua vũng nước.", "Giảm tốc độ cho xe chạy chậm qua vũng nước.", "Giảm tốc độ cho xe chạy qua làn đường dành cho mô tô để tránh vũng nước.", null, null, "Trên làn đường dành cho ô tô có vũng nước lớn, có nhiều người đi xe mô tô trên làn đường bên cạnh, người lái xe ô tô xử lý như thế nào là có văn hóa giao thông?", false },
                    { new Guid("215ea988-8d2d-48f5-bd03-c63e965a7be3"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "A", true, "Không được phép quay đầu xe ở phần đường dành cho người đi bộ qua đường.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Ở phần đường dành cho người đi bộ qua đường, trên cầu, đầu cầu, đường cao tốc, đường hẹp, đường dốc, tại nơi đường bộ giao nhau cùng mức với đường sắt.", "Ở phía trước hoặc phía sau của phần đường dành cho người đi bộ qua đường, trên đường quốc lộ, tại nơi đường bộ giao nhau không cùng mức với đường sắt.", "Cả ý 1 và ý 2.", "", null, null, "Người lái xe không được quay đầu xe trong các trường hợp nào dưới đây?", false },
                    { new Guid("222fc5e2-7ec5-446f-983d-43f589a82a5e"), new Guid("78627311-083d-42f9-a921-168e88e5f187"), "A", true, "Người điều khiển bị cấm sử dụng rượu, bia khi tham gia giao thông.", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Người điều khiển: xe ô tô, me mô tô, xe đạp, xe gắn máy.", "Người ngồi phía sau người điều khiển xe cơ giới.", "Người đi bộ.", "Cả ý 1 và ý 2.", null, null, "Theo Luật phòng chống tác hại của rượu, bia đối tượng nào dưới đây bị cấm sử dụng rượu, bia khi tham gia giao thông?", false },
                    { new Guid("2240d397-9322-4194-ac69-9174f4a6dfa6"), new Guid("78627311-083d-42f9-a921-168e88e5f187"), "A", false, "Khổ giới hạn đường bộ có giới hạn về chiều cao, chiều rộng.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Là khoảng trống có kích thước giới hạn về chiều cao, chiều rộng của đường, cầu, bến phà, hầm đường bộ để các xe kể cả hàng hóa xếp trên xe đi qua được an toàn.", "Là khoảng trống có kích thước giới hạn về chiều rộng của đường, cầu, bến phà, hầm trên đường bộ để các xe kể cả hàng hóa xếp trên xe đi qua được an toàn.", null, null, null, null, "Khái niệm \"Khổ giới hạn của đường bộ\" được hiểu như thế nào là đúng?", false },
                    { new Guid("2720fc79-6586-496b-b8c4-8f59e913a926"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "C", true, "", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Được phép.", "Tùy trường hợp.", "Không được phép.", "", null, null, "Khi điều khiển xe mô tô hai bánh, xe mô tô ba bánh, xe gắn máy, những hành vi buông cả hai tay; sử dụng xe để kéo, đẩy xe khác, vật khác; sử dụng chân chống của xe quệt xuống đường khi xe đang chạy có được phép hay không?", false },
                    { new Guid("2892a1a3-c46d-4dd0-8c64-da93d5432c69"), new Guid("51923743-b8a3-42e8-9810-219fcffaa9ee"), "B", false, null, new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Là bình thường.", "Là thiếu văn hóa giao thông.", "Là có văn hóa giao thông.", null, null, null, "Người lái xe cố tình không phân biệt làn đường, vạch phân làn, phóng nhanh, vượt ẩu, vượt đèn đỏ, đi vào đường cấm, đường một chiều được coi là hành vi nào trong các hành vi dưới đây?", false },
                    { new Guid("2e311460-e16f-44e1-981c-1c40c1268c1a"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "B", true, "Sử dụng rượu, bia khi lái xe bị phạt hành chính hoặc xử lý hình sự.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Chỉ bị nhắc nhở.", "Bị xử phạt hành chính hoặc có thể bị xử lý hình sự tùy theo mức độ vi phạm.", "Không bị xử lý hình sự.", null, null, null, "Sử dụng rượu, bia khi lái xe, nếu bị phát hiện thì bị xử lý như thế nào?", false },
                    { new Guid("2e88303a-6350-4a59-bb0f-4829665fea92"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "D", false, "Xe kéo đã kéo rơ moóc không được kéo thêm xe.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Chỉ được thực hiện trên đường quốc lộ có hai làn xe một chiều.", "Chỉ được thực hiện trên đường cao tốc.", "Không được thực hiện vào ban ngày.", "Không được phép.", null, null, "Khi xe đã kéo 1 xe hoặc xe đã kéo 1 rơ moóc, bạn có được phép kéo thêm xe (kể cả xe thô sơ) hoặc rơ moóc thứ hai hay không?", false },
                    { new Guid("2eda2d78-132f-4100-bf02-1ab43e19e64e"), new Guid("1365e7dd-6325-4662-ba5a-fe0312d5519a"), "B", false, "", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Không bắt buộc.", "Bắt buộc.", "Tùy từng trường hợp.", "", null, null, "Xe mô tô và xe ô tô tham gia giao thông trên đường bộ phải bắt buộc có đủ bộ phận giảm thanh không?", false },
                    { new Guid("302e8466-579c-4a1f-8b76-d84b452a7b2e"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "A", true, "Người điều khiển bị cấm sử dụng rượu, bia khi tham gia giao thông.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Người điều khiển: xe ô tô, me mô tô, xe đạp, xe gắn máy.", "Người ngồi phía sau người điều khiển xe cơ giới.", "Người đi bộ.", "Cả ý 1 và ý 2.", null, null, "Theo Luật phòng chống tác hại của rượu, bia đối tượng nào dưới đây bị cấm sử dụng rượu, bia khi tham gia giao thông?", false },
                    { new Guid("33431925-d90e-443f-bb4d-0dc5a048394f"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "C", true, "", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Không bị nghiêm cấm.", "Không bị nghiêm cấm khi rất vội.", "Bị nghiêm cấm.", "Không bị nghiêm cấm khi khẩn cấp.", null, null, "Hành vi vượt xe tại các vị trí có tầm nhìn hạn chế, đường vòng, đầu dốc có bị nghiêm cấm hay không?", false },
                    { new Guid("3393dbff-9dee-4f39-9d0d-08219b69d1ed"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "D", false, "Trong đô thị sử dụng đèn chiếu gần.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Bất cứ đèn nào miễn là mắt nhìn rõ phía trước.", "Chỉ bật đèn chiếu xa (đèn pha) khi không nhìn rõ đường.", "Đèn chiếu xa (đèn pha) khi đường vắng, đèn pha chiếu gần (đèn cốt) khi có xe đi ngược chiều.", "Đèn chiếu gần (đèn cốt).", null, null, "Người lái xe sử dụng đèn như thế nào khi lái xe trong khu đô thị và đông dân cư vào ban đêm?", false },
                    { new Guid("33ca0bba-4d98-429a-9aee-2ddcf4cd4778"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "B", true, "Không được dừng, đỗ xe trên miệng cống thoát nước.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Được dừng xe, đỗ xe trong trường hợp cần thiết.", "Không được dừng xe, đỗ xe.", "Được dừng xe, không được đỗ xe.", "", null, null, "Người điều khiển phương tiện giao thông trên đường phố có được dừng xe, đỗ xe trên miệng cống thoát nước, miệng hầm của đường điện thoại, điện cao thế, chỗ dành riêng cho xe chữa cháy lấy nước hay không?", false },
                    { new Guid("355f1057-5b9c-4ef7-9483-a696f784b122"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "C", false, "Độ tuổi lấy bằng theo hạng (cách nhau 3 tuổi): 16: Xe dưới 50cm3; 18: Hạng A, B1, B2; 21: Hạng C, FB; 24: Hạng D, FC; 27: Hạng E, FD.", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "23 tuổi.", "24 tuổi.", "27 tuổi.", "30 tuổi.", null, null, "Người lái xe ô tô chở người trên 30 chỗ ngồi (hạng E), lái xe hạng D kéo rơ moóc (FD) phải đủ bao nhiêu tuổi trở lên?", false },
                    { new Guid("35a9212a-d786-46c8-b6e4-b1b1b7b96c57"), new Guid("1365e7dd-6325-4662-ba5a-fe0312d5519a"), "B", false, "Không được phép thay đổi so với giấy chứng nhận đăng ký xe.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Được phép thay đổi bằng cách dán đề can với màu sắc phù hợp.", "Không được phép thay đổi.", "Tùy từng loại phương tiện cơ giới đường bộ.", null, null, null, "Chủ phương tiện cơ giới đường bộ có được tự ý thay đổi màu sơn, nhãn hiệu hoặc các đặc tính kỹ thuật của phương tiện so với chứng nhận đăng ký xe hay không?", false },
                    { new Guid("3693b3fb-0656-4358-aa61-6f1852544cb2"), new Guid("51923743-b8a3-42e8-9810-219fcffaa9ee"), "C", false, "", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Cho xe chạy thật nhanh qua vũng nước.", "Giảm tốc độ cho xe chạy chậm qua vũng nước.Giảm tốc độ cho xe chạy chậm qua vũng nước.", "Giảm tốc độ cho xe chạy qua làn đường dành cho mô tô để tránh vũng nước.", "", null, null, "Trên làn đường dành cho ô tô có vũng nước lớn...", false },
                    { new Guid("3a8c9643-02b5-4536-9abe-bcaecba531e1"), new Guid("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"), "B", false, "Không làm việc 1 ngày của lái xe quá 10 giờ.", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Không quá 8 giờ.", "Không quá 10 giờ.", "Không quá 12 giờ.", "Không hạn chế tùy thuộc vào sức khỏe và khả năng của người lái xe.", null, null, "Thời gian làm việc trong một ngày của người lái xe ô tô không được vượt quá bao nhiêu giờ trong trường hợp dưới đây?", false },
                    { new Guid("3b850e12-f5a8-408c-88d2-e22e3ddf1598"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "B", false, "Độ tuổi lấy bằng theo hạng (cách nhau 3 tuổi): 16: Xe dưới 50cm3; 18: Hạng A, B1, B2; 21: Hạng C, FB; 24: Hạng D, FC; 27: Hạng E, FD.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "16 tuổi.", "18 tuổi.", "17 tuổi.", "", null, null, "Người đủ bao nhiêu tuổi trở lên thì được điều khiển xe mô tô hai bánh, xe mô tô ba bánh có dung tích xi lanh từ 50 cm3 trở lên và các loại xe có kết cấu tương tự; xe ô tô tải, máy kéo có trọng tải dưới 3.500 kg; xe ô tô chở người đến 9 chỗ ngồi?", false },
                    { new Guid("459f6775-e179-4288-9c2d-1d725cf3b6d8"), new Guid("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"), "C", false, "Nghiêm cấm vận chuyển hàng cấm lưu thông.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Vận chuyển hàng nguy hiểm nhưng có giấy phép.", "Vận chuyển động vật hoang dã nhưng thực hiện đủ các quy định có liên quan.", "Vận chuyển hàng hóa cấm lưu thông; vận chuyển trái phép hàng nguy hiểm, động vật hoang dã.", "", null, null, "Trong hoạt động vận tải đường bộ, các hành vi nào dưới đây bị nghiêm cấm?", false },
                    { new Guid("48e73c98-017f-4788-aa43-620816a54274"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "C", false, "Độ tuổi lấy bằng theo hạng (cách nhau 3 tuổi): 16: Xe dưới 50cm3; 18: Hạng A, B1, B2; 21: Hạng C, FB; 24: Hạng D, FC; 27: Hạng E, FD.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "23 tuổi.", "24 tuổi.", "27 tuổi.", "30 tuổi.", null, null, "Người lái xe ô tô chở người trên 30 chỗ ngồi (hạng E), lái xe hạng D kéo rơ moóc (FD) phải đủ bao nhiêu tuổi trở lên?", false },
                    { new Guid("4b2328e6-2256-4d66-bcb4-878a4ba6c433"), new Guid("1365e7dd-6325-4662-ba5a-fe0312d5519a"), "D", false, null, new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Đèn chiếu sáng gần và xa.", "Đèn soi biển số, đèn báo hãm và đèn tín hiệu.", "Dàn đèn pha trên nóc xe.", "Cả ý 1 và ý 2.", null, null, "Xe ô tô tham gia giao thông trên đường bộ phải có đủ các loại đèn gì dưới đây?", false },
                    { new Guid("5291883a-5d70-4d44-be76-8fc7e5196da1"), new Guid("a1720db9-1416-4e62-a922-e34364f67418"), "B", false, "Thứ tự ưu tiên: Xe ưu tiên - Đường ưu tiên - Đường cùng cấp theo thứ tự bên phải trống - rẽ phải - đi thẳng - rẽ trái. 1. Xe tải: Đường ưu tiên và đi thẳng; 2. Mô tô: Đường ưu tiên và rẽ trái; 3. Xe khách: Đường không ưu tiên, đi thẳng. 4. Xe con: Đường không ưu tiên, rẽ trái.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Xe tải, xe khách, xe con, mô tô.", "Xe khách, xe tải, xe con, mô tô.", "Mô tô, xe khách, xe tải, xe con.", "Mô tô, xe khách, xe tải, xe con.", "/images/h6.webp", null, "Thứ tự các xe đi như thế nào là đúng quy tắc giao thông?", false },
                    { new Guid("537e9dd8-6ad4-4c73-93f5-c651354b7d14"), new Guid("a5674e1d-af51-4a56-bdd8-758210677c1a"), "A", false, null, new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Biển 1.", "Biển 2.", "Biển 1 và 3.", "Cả ba biển.", "/images/h1.webp", null, "Yêu cầu của kính chắn gió, chọn \"Loại kính an toàn\".", false },
                    { new Guid("5a28d77b-4387-49e6-8541-059f96459f50"), new Guid("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"), "A", false, "Lái xe khách, xe buýt thực hiện nghiêm biểu đồ chạy xe được phân công.", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "1. Luôn có ý thức về tính tổ chức, kỷ luật, thực hiện nghiêm biểu đồ xe chạy được phân công; thực hiện đúng hành trình, lịch trình, đón trả khách đúng nơi quy định; giúp đỡ hành khách đi xe, đặc biệt là những người khuyết tật, người già, trẻ em và phụ nữ có thai, có con nhỏ.", "2. Luôn có ý thức về tính tổ chức, kỷ luật, thực hiện linh hoạt biểu đồ xe chạy được phân công để tiết kiệm chi phí; thực hiện đúng hành trình, lịch trình khi có khách đi xe, đón trả khách ở những nơi thuận tiện cho hành khách đi xe.", "", "", null, null, "Người lái xe khách, xe buýt cần thực hiện những nhiệm vụ gì dưới đây?", false },
                    { new Guid("5b5d2259-c2c1-4960-bfcd-f8ced656b95a"), new Guid("78627311-083d-42f9-a921-168e88e5f187"), "A", true, "Uống rượu bia không được lái xe,", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Không được phép", "Chỉ được lái ở tốc độ chậm và quãng đường ngắn.", "Chỉ được lái nếu trong cơ thể có nồng độ cồn thấp.", "", null, null, "Việc lái xe mô tô, ô tô, máy kéo ngay sau khi uống rượu, bia có được phép hay không?", false },
                    { new Guid("5dfc4fdf-f830-4e96-9b4c-d3601a8a4214"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "A", true, "Xe mô tô không được kéo xe khác.", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Không được vận chuyển.", "Chỉ được vận chuyển khi đã chằng buộc cẩn thận.", "Chỉ được vận chuyển vật cồng kềnh trên xe máy nếu khoảng cách về nhà ngắn hơn 2 km.", "", null, null, "Hành vi vận chuyển đồ vật cồng kềnh bằng xe mô tô, xe gắn máy khi tham gia giao thông có được phép hay không?", false },
                    { new Guid("5fab968e-1c99-47f8-954e-b7d8750594ea"), new Guid("a5674e1d-af51-4a56-bdd8-758210677c1a"), "D", false, null, new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Cả ba biển.", "Biển 2 và 3.", "Biển 1 và 3.", "Biển 1 và 2.", "/images/h2.jpg", null, "Biển nào cấm ô tô tải?", false },
                    { new Guid("62f88d57-380c-49c6-8dc6-418133f8c194"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "B", true, "Không được phép quay đầu xe ở phần đường dành cho người đi bộ qua đường.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Được phép.", "Không được phép.", "Tùy từng trường hợp.", "", null, null, "Ở phần đường dành cho người đi bộ qua đường, trên cầu, đầu cầu, đường cao tốc, đường hẹp, đường dốc, tại nơi đường bộ giao nhau cùng mức với đường sắt có được quay đầu xe hay không?", false },
                    { new Guid("668c65d9-aa62-49bc-994a-38e95163dfff"), new Guid("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"), "C", false, "Nghiêm cấm đe dọa, xúc phạm, tranh giành, lôi kéo hành khách.", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Cạnh tranh nhau nhằm tăng lợi nhuận.", "Giảm giá để thu hút khách.", "Đe dọa, xúc phạm, tranh giành, lôi kéo hành khách; bắt ép hành khách sử dụng dịch vụ ngoài ý muốn; xuống khách nhằm trốn tránh phát hiện xe chở quá số người quy định.", "Tất cả các ý trên.", null, null, "Trong hoạt động vận tải khách, những hành vi nào dưới đây bị nghiêm cấm?", false },
                    { new Guid("6cc0e7d0-9cd0-4a2e-bac3-6a9e49996449"), new Guid("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"), "C", false, "Nghiêm cấm đe dọa, xúc phạm, tranh giành, lôi kéo hành khách.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Cạnh tranh nhau nhằm tăng lợi nhuận.", "Giảm giá để thu hút khách.", "Đe dọa, xúc phạm, tranh giành, lôi kéo hành khách; bắt ép hành khách sử dụng dịch vụ ngoài ý muốn; xuống khách nhằm trốn tránh phát hiện xe chở quá số người quy định.", "Tất cả các ý trên.", null, null, "Trong hoạt động vận tải khách, những hành vi nào dưới đây bị nghiêm cấm?", false },
                    { new Guid("6dd81400-e240-4796-bcec-fa453132e043"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "B", false, "Độ tuổi lấy bằng theo hạng (cách nhau 3 tuổi): 16: Xe dưới 50cm3; 18: Hạng A, B1, B2; 21: Hạng C, FB; 24: Hạng D, FC; 27: Hạng E, FD.", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "19 tuổi.", "21 tuổi.", "20 tuổi.", "", null, null, "Người đủ bao nhiêu tuổi trở lên thì được điều khiển xe ô tô tải, máy kéo có trọng tải từ 3.500 kg trở lên; xe hạng B2 kéo rơ moóc (FB2)?", false },
                    { new Guid("6e08d610-d90e-4c49-81ed-dd2544e0e2d3"), new Guid("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"), "A", false, "Lái xe khách, xe buýt thực hiện nghiêm biểu đồ chạy xe được phân công.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "1. Luôn có ý thức về tính tổ chức, kỷ luật, thực hiện nghiêm biểu đồ xe chạy được phân công; thực hiện đúng hành trình, lịch trình, đón trả khách đúng nơi quy định; giúp đỡ hành khách đi xe, đặc biệt là những người khuyết tật, người già, trẻ em và phụ nữ có thai, có con nhỏ.", "2. Luôn có ý thức về tính tổ chức, kỷ luật, thực hiện linh hoạt biểu đồ xe chạy được phân công để tiết kiệm chi phí; thực hiện đúng hành trình, lịch trình khi có khách đi xe, đón trả khách ở những nơi thuận tiện cho hành khách đi xe.", "", "", null, null, "Người lái xe khách, xe buýt cần thực hiện những nhiệm vụ gì dưới đây?", false },
                    { new Guid("7078b656-ed8b-4e2a-bc52-df9b6bcab446"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "C", true, null, new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Chỉ lớn hơn tốc độ tối đa cho phép khi đường vắng.", "Chỉ lớn hơn tốc độ tối đa cho phép vào ban đêm.", "Không vượt quá tốc độ cho phép.", null, null, null, "Khi lái xe trên đường, người lái xe cần quan sát và đảm bảo tốc độ phương tiện như thế nào?", false },
                    { new Guid("713d0b90-a65b-46aa-a988-14e97dc5ca08"), new Guid("51923743-b8a3-42e8-9810-219fcffaa9ee"), "B", false, "", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Thực hiện cầm máu trực tiếp.", "Thực hiện cầm máu không trực tiếp (chặn động mạch).", "", "", null, null, "Khi sơ cứu người bị tai nạn giao thông...", false },
                    { new Guid("71482e7a-3b46-415c-b38b-50ac587d8b18"), new Guid("51923743-b8a3-42e8-9810-219fcffaa9ee"), "C", false, null, new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Có trách nhiệm với bản thân và với cộng đồng; tôn trọng, nhường nhịn người khác.", "Tận tình giúp đỡ người tham gia giao thông gặp hoạn nạn; giúp đỡ người khuyết tật, trẻ em và người cao tuổi.", "Cả ý 1 và ý 2.", null, null, null, "Người lái xe có văn hóa khi tham gia giao thông phải đáp ứng các điều kiện nào dưới đây?", false },
                    { new Guid("7377f93b-481c-4ca4-9c55-dff8d700783f"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "D", false, "Được vượt khi xe không phát tín hiệu ưu tiên", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Không được vượt.", "Được vượt khi đang đi trên cầu.", "Được phép vượt khi đi qua nơi giao nhau có ít phương tiện cùng tham gia giao thông.", "Được vượt khi đảm bảo an toàn.", null, null, "Bạn đang lái xe, phía trước có một xe cảnh sát giao thông không phát tín hiệu ưu tiên bạn có được phép vượt hay không?", false },
                    { new Guid("73dd9082-af95-49a5-9151-ef2afc730462"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "A", true, "Không được vượt trên cầu hẹp có một làn xe.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Trên cầu hẹp có một làn xe. Nơi đường giao nhau, đường bộ giao nhau cùng mức với đường sắt; xe được quyền ưu tiên đang phát tín hiệu ưu tiên đi làm nhiệm vụ.", "Trên cầu có từ 02 làn xe trở lên; nơi đường bộ giao nhau không cùng mức với đường sắt; xe được quyền ưu tiên đang đi phía trước nhưng không phát tín hiệu ưu tiên.", "Trên đường có 2 làn đường được phân chia làn bằng vạch kẻ nét đứt.", "", null, null, "Người lái xe không được vượt xe khác khi gặp trường hợp nào ghi ở dưới đây?", false },
                    { new Guid("755996ec-0569-4a13-aeb4-28b38b33d0d3"), new Guid("a1720db9-1416-4e62-a922-e34364f67418"), "D", false, "Thứ tự ưu tiên: Xe ưu tiên - Đường ưu tiên - Đường cùng cấp theo thứ tự bên phải trống - rẽ phải - đi thẳng - rẽ trái. 1. Xe công an: Xe ưu tiên; 2. Xe tải: Đường ưu tiên; 3. Xe khách: Đường không ưu tiên, bên phải trống; 4. Xe con: Đường không ưu tiên, bên phải vướng xe khách nên phải nhường.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Xe tải, xe công an, xe khách, xe con.", "Xe công an, xe khách, xe con, xe tải.", "Xe công an, xe con, xe tải, xe khách.", "Xe công an, xe tải, xe khách, xe con", "/images/h7.webp", null, "Theo hướng mũi tên, thứ tự các xe đi như thế nào là đúng quy tắc giao thông?", false },
                    { new Guid("76763bcd-5e4c-4bcf-9e89-916b48d7e991"), new Guid("51923743-b8a3-42e8-9810-219fcffaa9ee"), "B", false, "", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Là bình thường.", "Là thiếu văn hóa giao thông.", "Là có văn hóa giao thông.", "", null, null, "Người lái xe cố tình không phân biệt làn đường...", false },
                    { new Guid("78e0ce2c-e8a7-45a9-8999-2914161bc940"), new Guid("51923743-b8a3-42e8-9810-219fcffaa9ee"), "D", false, null, new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Kiểm tra các điều kiện bảo đảm an toàn của xe sau khi khởi hành; có trách nhiệm lái xe thật nhanh khi chậm giờ của khách.", "Kiểm tra các điều kiện bảo đảm an toàn của xe trước khi khởi hành; có thái độ văn minh, lịch sự, hướng dẫn hành khách ngồi đúng nơi quy định; kiểm tra việc sắp xếp, chằng buộc hành lý, bảo đảm an toàn.", "Có biện pháp bảo vệ tính mạng, sức khỏe, tài sản của hành khách đi xe, giữ gìn trật tự; vệ sinh trong xe; đóng cửa lên xuống của xe trước và trong khi xe chạy.", "Cả ý 2 và ý 3.", null, null, "Người lái xe và nhân viên phục vụ trên ô tô vận tải hành khách phải có những trách nhiệm gì theo quy định dưới đây?", false },
                    { new Guid("798d3390-105b-40e6-b030-23af2ccfa26a"), new Guid("51923743-b8a3-42e8-9810-219fcffaa9ee"), "B", false, null, new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Điều khiển xe đi trên phần đường, làn đường có ít phương tiện tham gia giao thông, chỉ đội mũ bảo hiểm ở nơi có biển báo bắt buộc đội mũ bảo hiểm.", "Chấp hành quy định về tốc độ, đèn tín hiệu, biển báo hiệu, vạch kẻ đường khi lái xe; chấp hành hiệu lệnh, chỉ dẫn của người điều khiển giao thông; nhường đường cho người đi bộ, người già, trẻ em và người khuyết tật.", "Cả ý 1 và ý 2.", null, null, null, "Trong các hành vi dưới đây, người lái xe mô tô có văn hóa giao thông phải ứng xử như thế nào?", false },
                    { new Guid("79b7fb83-88ca-4e71-a0ff-741cb374ff85"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "B", true, "Không được dừng, đỗ xe trên miệng cống thoát nước.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Được dừng xe, đỗ xe trong trường hợp cần thiết.", "Không được dừng xe, đỗ xe.", "Được dừng xe, không được đỗ xe.", null, null, null, "Người điều khiển phương tiện giao thông trên đường phố có được dừng xe, đỗ xe trên miệng cống thoát nước, miệng hầm của đường điện thoại, điện cao thế, chỗ dành riêng cho xe chữa cháy lấy nước hay không?", false },
                    { new Guid("7cb30d72-1d84-485b-b255-7bcf3b8f6cde"), new Guid("a1720db9-1416-4e62-a922-e34364f67418"), "A", false, "", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Biển 1.", "Biển 2.", "Biển 1 và 3.", "Cả ba biển.", "/images/20250515231400_4fe4816e_h2.jpg", null, "Yêu cầu của kính chắn gió, chọn \"Loại kính an toàn\".", false },
                    { new Guid("7ea708f2-b8b1-4787-a953-58d5e0cf27e0"), new Guid("78627311-083d-42f9-a921-168e88e5f187"), "B", true, "Sử dụng rượu, bia khi lái xe bị phạt hành chính hoặc xử lý hình sự.", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Chỉ bị nhắc nhở.", "Bị xử phạt hành chính hoặc có thể bị xử lý hình sự tùy theo mức độ vi phạm.", "Không bị xử lý hình sự.", "", null, null, "Sử dụng rượu, bia khi lái xe, nếu bị phát hiện thì bị xử lý như thế nào?", false },
                    { new Guid("7ead6682-19ab-4252-8d15-ca735035b4f1"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "D", true, "Xe mô tô không được kéo xe khác.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Được phép", "Được bám trong trường hợp phương tiện của mình bị hỏng.", "Được kéo, đẩy trong trường hợp phương tiện khác bị hỏng.", "Không được phép.", null, null, "Người ngồi trên xe mô tô hai bánh, xe mô tô ba bánh, xe gắn máy khi tham gia giao thông có được bám, kéo hoặc đẩy các phương tiện khác không?", false },
                    { new Guid("7fe4fdbf-084a-4fbc-82af-a1a18b2606d5"), new Guid("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"), "A", false, "Không lái xe liên tục quá 4 giờ.", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Không quá 4 giờ.", "Không quá 6 giờ.", "Không quá 8 giờ.", "Liên tục tùy thuộc vào sức khỏe và khả năng của người lái xe.", null, null, "Thời gian làm việc của người lái xe ô tô không được lái xe liên tục quá bao nhiêu giờ trong trường hợp nào dưới đây?", false },
                    { new Guid("8196069a-93ea-4aa5-bf8b-90799f10b999"), new Guid("a1720db9-1416-4e62-a922-e34364f67418"), "D", false, "Thứ tự ưu tiên: Xe ưu tiên - Đường ưu tiên - Đường cùng cấp theo thứ tự bên phải trống - rẽ phải - đi thẳng - rẽ trái. 1. Xe công an: Xe ưu tiên; 2. Xe tải: Đường ưu tiên; 3. Xe khách: Đường không ưu tiên, bên phải trống; 4. Xe con: Đường không ưu tiên, bên phải vướng xe khách nên phải nhường.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Xe tải, xe công an, xe khách, xe con.", "Xe công an, xe khách, xe con, xe tải.", "Xe công an, xe con, xe tải, xe khách.", "Xe công an, xe tải, xe khách, xe con", "/images/h7.webp", null, "Theo hướng mũi tên, thứ tự các xe đi như thế nào là đúng quy tắc giao thông?", false },
                    { new Guid("84c5978f-a44b-4f49-8b30-ed8734debdd2"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "C", true, "", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Chỉ lớn hơn tốc độ tối đa cho phép khi đường vắng.", "Chỉ lớn hơn tốc độ tối đa cho phép vào ban đêm.", "Không vượt quá tốc độ cho phép.", "", null, null, "Khi lái xe trên đường, người lái xe cần quan sát và đảm bảo tốc độ phương tiện như thế nào?", false },
                    { new Guid("874c0ec3-a8b1-45e7-9001-a3f15bf55500"), new Guid("78627311-083d-42f9-a921-168e88e5f187"), "B", false, "Làn đường có bề rộng đủ cho xe chạy an toàn.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), "image", "Là một phần của phần đường xe chạy được chia theo chiều dọc của đường, sử dụng cho xe chạy.", "Là một phần của phần đường xe chạy được chia theo chiều dọc của đường, có bề rộng đủ cho xe chạy an toàn.", "Là đường cho xe ô tô chạy, dừng, đỗ an toàn.", null, null, null, "\"Làn đường\" là gì?", false },
                    { new Guid("8887645d-e66e-4136-a243-16df6cc623be"), new Guid("1365e7dd-6325-4662-ba5a-fe0312d5519a"), "D", false, "", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Đèn chiếu sáng gần và xa.", "Đèn soi biển số, đèn báo hãm và đèn tín hiệu.", "Dàn đèn pha trên nóc xe.", "Cả ý 1 và ý 2.", null, null, "Xe ô tô tham gia giao thông trên đường bộ phải có đủ các loại đèn gì dưới đây?", false },
                    { new Guid("8be205cc-3339-4f8c-a331-491f6bb6a4de"), new Guid("cdcfb0da-a22d-4891-bac4-1fea55614508"), "A", false, "Khởi hành xe ô tô số tự động cần đạp phanh chân hết hành trình.", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Đạp bàn đạp phanh chân hết hành trình...", "Đạp bàn đạp để tăng ga với mức độ phù hợp...", "", "", null, null, "Khi vào số để khởi hành xe ô tô có số tự động...", false },
                    { new Guid("93e30bee-85c5-43ba-b539-f3088817a5a7"), new Guid("1365e7dd-6325-4662-ba5a-fe0312d5519a"), "A", false, "Yêu cầu của kính chắn gió, chọn \"Loại kính an toàn\".", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Là loại kính an toàn, kính nhiều lớp, đúng quy cách, không rạn nứt, đảm bảo hình ảnh quan sát rõ ràng, không bị méo mó.", "Là loại kính trong suốt, không rạn nứt, đảm bảo tầm nhìn cho người điều khiển về phía trước mặt và hai bên.", null, null, null, null, "Kính chắn gió của xe ô tô phải đảm bảo yêu cầu nào dưới đây?", false },
                    { new Guid("9903bb11-6828-462c-8fea-f1b0dc069cc4"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "B", true, "Không được phép lắp đặt còi đèn không đúng thiết kế.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Được phép.", "Không được phép.", "Được phép tùy từng trường hợp.", "", null, null, "Hành vi lắp đặt, sử dụng còi, đèn không đúng thiết kế của nhà sản xuất đối với từng loại xe cơ giới có được phép hay không?", false },
                    { new Guid("9ac6b70c-a8d8-4f44-aab5-15f0f377bf80"), new Guid("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"), "C", false, "Nghiêm cấm vận chuyển hàng cấm lưu thông.", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Vận chuyển hàng nguy hiểm nhưng có giấy phép.", "Vận chuyển động vật hoang dã nhưng thực hiện đủ các quy định có liên quan.", "Vận chuyển hàng hóa cấm lưu thông; vận chuyển trái phép hàng nguy hiểm, động vật hoang dã.", "", null, null, "Trong hoạt động vận tải đường bộ, các hành vi nào dưới đây bị nghiêm cấm?", false },
                    { new Guid("9b9167b2-d1ce-41c9-a47e-0c6fc37f1ee9"), new Guid("1365e7dd-6325-4662-ba5a-fe0312d5519a"), "C", false, "", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Kính chắn gió, kính cửa phải là loại kính an toàn...", "Có đủ đèn chiếu sáng gần và xa...", "Cả ý 1 và ý 2.", "", null, null, "Xe ô tô tham gia giao thông đường bộ phải bảo đảm các quy định...", false },
                    { new Guid("9c7a8aa0-8976-47ce-854d-6ade9c767913"), new Guid("cdcfb0da-a22d-4891-bac4-1fea55614508"), "A", false, "Khởi hành xe ô tô số tự động cần đạp phanh chân hết hành trình.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Đạp bàn đạp phanh chân hết hành trình, vào số và nhả phanh tay, kiểm tra lại xem có bị nhầm số không rồi mới cho xe lăn bánh.", "Đạp bàn đạp để tăng ga với mức độ phù hợp, vào số và kiểm tra lại xem có bị nhầm số không rồi mới cho xe lăn bánh.", null, null, null, null, "Khi vào số để khởi hành xe ô tô có số tự động, người lái xe phải thực hiện các thao tác nào để đảm bảo an toàn?", false },
                    { new Guid("9dcc5c60-d114-4389-aab5-be90f76c40fd"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "C", true, null, new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Không bị nghiêm cấm.", "Không bị nghiêm cấm khi rất vội.", "Bị nghiêm cấm.", "Không bị nghiêm cấm khi khẩn cấp.", null, null, "Hành vi vượt xe tại các vị trí có tầm nhìn hạn chế, đường vòng, đầu dốc có bị nghiêm cấm hay không?", false },
                    { new Guid("9ecc10c6-4184-49b1-8417-2ccac03588de"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "B", false, "Lắp đặt còi đèn không đúng thiết kế phải được chấp thuận của cơ quan có thẩm quyền.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Phải đảm bảo phụ tùng do đúng nhà sản xuất đó cung cấp.", "Phải được chấp thuận của cơ quan có thẩm quyền.", "Phải là xe đăng ký và hoạt động tại các khu vực có địa hình phức tạp.", "", null, null, "Trong trường hợp đặc biệt, để được lắp đặt, sử dụng còi, đèn không đúng với thiết kế của nhà sản xuất đối với từng loại xe cơ giới bạn phải đảm bảo yêu cầu nào dưới đây?", false },
                    { new Guid("a4712242-f7a6-4c4c-a680-afa6af560e21"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "C", true, null, new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Không bị nghiêm cấm.", "Không bị nghiêm cấm khi rất vội.", "Bị nghiêm cấm.", "Không bị nghiêm cấm khi khẩn cấp.", null, null, "Hành vi vượt xe tại các vị trí có tầm nhìn hạn chế, đường vòng, đầu dốc có bị nghiêm cấm hay không?", false },
                    { new Guid("a7082f8b-b688-4513-89da-95166775c8c8"), new Guid("a1720db9-1416-4e62-a922-e34364f67418"), "D", false, "", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), "Image", "Cả ba biển.", "Biển 2 và 3.", "Biển 1 và 3.", "Biển 1 và 2.", "/images/h2.jpg", null, "Biển nào cấm ô tô tải?", false },
                    { new Guid("aa5ad586-935e-42e0-9002-331a6e6f17e4"), new Guid("78627311-083d-42f9-a921-168e88e5f187"), "A", true, "Trong máu hoặc hơi thở có nồng độ cồn bị nghiêm cấm.", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Bị nghiêm cấm.", "Không bị nghiêm cấm", "Không bị nghiêm cấm, nếu nồng độ cồn trong máu ở mức nhẹ, có thể điều khiển phương tiện tham gia giao thông.", "", null, null, "Người điều khiển xe mô tô, ô tô, máy kéo trên đường mà trong máu hoặc hơi thở có nồng độ cồn có bị nghiêm cấm không?", false },
                    { new Guid("aba11629-bd97-4eb8-bc28-8846ccd51a4f"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "C", true, "", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Bị nghiêm cấm tùy từng trường hợp.", "Không bị nghiêm cấm.", "Bị nghiêm cấm.", "", null, null, "Hành vi điều khiển xe cơ giới chạy quá tốc độ quy định, giành đường, vượt ẩu có bị nghiêm cấm hay không?", false },
                    { new Guid("ac022356-dfff-478a-8920-a8246b340851"), new Guid("1365e7dd-6325-4662-ba5a-fe0312d5519a"), "A", false, "Yêu cầu của kính chắn gió, chọn \"Loại kính an toàn\".", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Là loại kính an toàn, kính nhiều lớp, đúng quy cách...", "Là loại kính trong suốt, không rạn nứt...", "", "", null, null, "Kính chắn gió của xe ô tô phải đảm bảo yêu cầu nào dưới đây?", false },
                    { new Guid("b0e3fc57-e1f5-4d66-a275-d80e1521cd0d"), new Guid("1365e7dd-6325-4662-ba5a-fe0312d5519a"), "B", false, null, new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Không bắt buộc.", "Bắt buộc.", "Tùy từng trường hợp.", null, null, null, "Xe mô tô và xe ô tô tham gia giao thông trên đường bộ phải bắt buộc có đủ bộ phận giảm thanh không?", false },
                    { new Guid("b17ab884-4488-4658-8ee8-3c22895dc21a"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "C", true, null, new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Chỉ lớn hơn tốc độ tối đa cho phép khi đường vắng.", "Chỉ lớn hơn tốc độ tối đa cho phép vào ban đêm.", "Không vượt quá tốc độ cho phép.", null, null, null, "Khi lái xe trên đường, người lái xe cần quan sát và đảm bảo tốc độ phương tiện như thế nào?", false },
                    { new Guid("b6400618-dbf7-4eff-9cc6-29ec22e49f30"), new Guid("cdcfb0da-a22d-4891-bac4-1fea55614508"), "B", false, "Khởi hành ô tô sử dụng hộp số đạp côn hết hành trình; vào số 1.", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Kiểm tra an toàn xung quanh xe ô tô...", "Kiểm tra an toàn xung quanh xe ô tô; đạp ly hợp (côn) hết hành trình...", "", "", null, null, "Khi khởi hành ô tô sử dụng hộp số cơ khí...", false },
                    { new Guid("b6f4b2ea-cc44-4e64-a167-34d529ee1683"), new Guid("78627311-083d-42f9-a921-168e88e5f187"), "A", true, "Có ma túy bị nghiêm cấm", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Bị nghiêm cấm.", "Không bị nghiêm cấm.", "Không bị nghiêm cấm, nếu có chất ma túy ở mức nhẹ, có thể điều khiển phương tiện tham gia giao thông.", "", null, null, "Người điều khiển phương tiện giao thông đường bộ mà trong cơ thể có chất ma túy có bị nghiêm cấm hay không?", false },
                    { new Guid("b795ed49-e1fb-4e2f-b1ce-994b257f0733"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "A", true, "Trong máu hoặc hơi thở có nồng độ cồn bị nghiêm cấm.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Bị nghiêm cấm.", "Không bị nghiêm cấm", "Không bị nghiêm cấm, nếu nồng độ cồn trong máu ở mức nhẹ, có thể điều khiển phương tiện tham gia giao thông.", null, null, null, "Người điều khiển xe mô tô, ô tô, máy kéo trên đường mà trong máu hoặc hơi thở có nồng độ cồn có bị nghiêm cấm không?", false },
                    { new Guid("ba1ea3e8-6ea1-40cf-9ca0-c79a5cc32dcf"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "A", true, "Có ma túy bị nghiêm cấm", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Bị nghiêm cấm.", "Không bị nghiêm cấm.", "Không bị nghiêm cấm, nếu có chất ma túy ở mức nhẹ, có thể điều khiển phương tiện tham gia giao thông.", null, null, null, "Người điều khiển phương tiện giao thông đường bộ mà trong cơ thể có chất ma túy có bị nghiêm cấm hay không?", false },
                    { new Guid("c386c5d2-a62c-46d0-90fc-28c7b066a298"), new Guid("cdcfb0da-a22d-4891-bac4-1fea55614508"), "B", false, "Thực hiện  phanh tay cần phải bóp khóa hãm đẩy cần phanh tay về phía trước.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Dùng lực tay phải kéo cần phanh tay về phía sau hết hành trình; nếu khóa hãm bị kẹt cứng phải đẩy mạnh phanh tay về phía trước, sau đó bóp khóa hãm.", "Dùng lực tay phải bóp khóa hãm đẩy cần phanh tay về phía trước hết hành trình; nếu khóa hãm bị kẹt cứng phải kéo cần phanh tay về phía sau đồng thời bóp khóa hãm.", "Dùng lực tay phải đẩy cần phanh tay về phía trước hết hành trình; nếu khóa hãm bị kẹt cứng phải đẩy mạnh phanh tay về phía trước, sau đó bóp khóa hãm.", null, null, null, "Khi nhả hệ thống phanh dừng cơ khí điều khiển bằng tay (phanh tay), người lái xe cần phải thực hiện các thao tác nào?", false },
                    { new Guid("c39d5ae0-6dc4-4457-89f4-6c76340d7291"), new Guid("51923743-b8a3-42e8-9810-219fcffaa9ee"), "B", false, "", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Điều khiển xe đi trên phần đường, làn đường có ít phương tiện...", "Chấp hành quy định về tốc độ, đèn tín hiệu...", "Cả ý 1 và ý 2.", "", null, null, "Trong các hành vi dưới đây, người lái xe mô tô có văn hóa giao thông phải ứng xử như thế nào?", false },
                    { new Guid("ca876d72-60f0-4666-ad3e-0aca1ef4df59"), new Guid("cdcfb0da-a22d-4891-bac4-1fea55614508"), "B", false, "Thực hiện  phanh tay cần phải bóp khóa hãm đẩy cần phanh tay về phía trước.", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Dùng lực tay phải kéo cần phanh tay về phía sau hết hành trình...", "Dùng lực tay phải bóp khóa hãm đẩy cần phanh tay về phía trước hết hành trình...", "Dùng lực tay phải đẩy cần phanh tay về phía trước hết hành trình...", "", null, null, "Khi nhả hệ thống phanh dừng cơ khí điều khiển bằng tay (phanh tay)...", false },
                    { new Guid("cd008ad4-3948-4a23-8341-94ac6e3b549f"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "B", false, "Chỉ sử dụng còi từ 5 giờ sáng đến 22 giờ tối.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Từ 22 giờ đêm đến 5 giờ sáng.", "Từ 5 giờ sáng đến 22 giờ tối.", "Từ 23 giờ đêm đến 5 giờ sáng hôm sau.", "", null, null, "Khi lái xe trong khu đô thị và đông dân cư trừ các khu vực có biển cấm sử dụng còi, người lái xe được sử dụng còi như thế nào trong các trường hợp dưới đây?", false },
                    { new Guid("ce50b6f1-6abc-481b-8a26-5fc8946efba0"), new Guid("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"), "D", false, "Cả ý 1 và ý 2 đều đúng. Bởi vì không thể tự do trả khách theo yêu cầu được.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Kiểm tra các điều kiện bảo đảm an toàn của xe trước khi khởi hành; kiểm tra việc sắp xếp, chằng buộc hành lý, hàng hóa bảo đảm an toàn.", "Đóng cửa lên xuống của xe trước và trong khi xe chạy.", "Đón trả khách tại vị trí do khách hàng yêu cầu.", "Cả ý 1 và ý 2.", null, null, "Lái xe kinh doanh vận tải khách phải có trách nhiệm gì sau đây?", false },
                    { new Guid("cebbff7c-ee35-4241-9f2b-b5f7bc948c83"), new Guid("a1720db9-1416-4e62-a922-e34364f67418"), "B", false, "", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Biển 1.", "Biển 2 và 3.", "Biển 1 và 3.", "Cả ba biển.", "/images/h3.jpg", null, "Biển nào cấm máy kéo?", false },
                    { new Guid("cf39ba06-f565-4f78-8f21-354b90b54dbc"), new Guid("cdcfb0da-a22d-4891-bac4-1fea55614508"), "A", true, "Xe mô tô xuống dốc dài cần sử dụng cả phanh trước và phanh sau để giảm tốc độ.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Giữ tay ga ở mức độ phù hợp, sử dụng phanh trước và phanh sau để giảm tốc độ.", "Nhả hết tay ga, tắt động cơ, sử dụng phanh trước và phanh sau để giảm tốc độ.", "Sử dụng phanh trước để giảm tốc độ kết hợp với tắt chìa khóa điện của xe.", null, null, null, "Khi điều khiển xe mô tô tay ga xuống đường dốc dài, độ dốc cao, người lái xe cần thực hiện các thao tác nào dưới đây để đảm bảo an toàn?", false },
                    { new Guid("d8e4bfbe-0914-49ac-aa8b-6321d20f1aca"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "B", true, "Không được phép giao xe cho người không đủ điều kiện tham gia giao thông.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Chỉ được thực hiện nếu đã hướng dẫn đầy đủ.", "Không được phép.", "Được phép tùy từng trường hợp.", "Chỉ được phép thực hiện với thành viên trong gia đình.", null, null, "Hành vi giao xe cơ giới, xe máy chuyên dùng cho người không đủ điều kiện để điều khiển xe tham gia giao thông có được phép hay không?", false },
                    { new Guid("dd25ba7a-3692-4aae-8e60-486edfbf96f0"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "D", true, "Xe mô tô không được kéo xe khác.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Được phép.", "Nếu phương tiện được kéo, đẩy có khối lượng nhỏ hơn phương tiện của mình", "Tùy trường hợp.Tùy trường hợp.", "Không được phép.", null, null, "Người điều khiển xe mô tô hai bánh, ba bánh, xe gắn máy có được phép sử dụng xe để kéo hoặc đẩy các phương tiện khác khi tham gia giao thông không?", false },
                    { new Guid("de1a4aa2-7f7a-4ce4-9abf-84979173ef35"), new Guid("a5674e1d-af51-4a56-bdd8-758210677c1a"), "B", false, null, new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Biển 1.", "Biển 2 và 3.", "Biển 1 và 3.", "Cả ba biển.", "/images/h3.jpg", null, "Biển nào cấm máy kéo?", false },
                    { new Guid("df574d98-027c-4559-952b-b9916e7b47f5"), new Guid("a1720db9-1416-4e62-a922-e34364f67418"), "B", false, "Thứ tự ưu tiên: Xe ưu tiên - Đường ưu tiên - Đường cùng cấp theo thứ tự bên phải trống - rẽ phải - đi thẳng - rẽ trái. 1. Xe tải: Đường ưu tiên và đi thẳng; 2. Mô tô: Đường ưu tiên và rẽ trái; 3. Xe khách: Đường không ưu tiên, đi thẳng. 4. Xe con: Đường không ưu tiên, rẽ trái.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Xe tải, xe khách, xe con, mô tô.", "Xe khách, xe tải, xe con, mô tô.", "Mô tô, xe khách, xe tải, xe con.", "Mô tô, xe khách, xe tải, xe con.", "/images/h6.webp", null, "Thứ tự các xe đi như thế nào là đúng quy tắc giao thông?", false },
                    { new Guid("df6b5953-a946-45f2-91ee-85bcfdc7b93f"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "A", true, "Tốc độ chậm đi ở làn bên phải trong cùng", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Đi ở làn bên phải trong cùng.", "Đi ở làn phía bên trái.", "Đi ở làn giữa.", "Đi ở bất cứ làn nào nhưng phải bấm đèn cảnh báo nguy hiểm để báo hiệu cho các phương tiện khác.", null, null, "Trên đường có nhiều làn đường, khi điều khiển phương tiện ở tốc độ chậm bạn phải đi ở làn đường nào?", false },
                    { new Guid("e1a5b85f-3b8d-4213-aad5-9f3ddca19f29"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "A", true, "Uống rượu bia không được lái xe,", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Không được phép", "Chỉ được lái ở tốc độ chậm và quãng đường ngắn.", "Chỉ được lái nếu trong cơ thể có nồng độ cồn thấp.", null, null, null, "Việc lái xe mô tô, ô tô, máy kéo ngay sau khi uống rượu, bia có được phép hay không?", false },
                    { new Guid("e1abc8bf-9e95-4ee8-8116-84e41132aa6e"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "A", true, "Tốc độ chậm đi ở làn bên phải trong cùng", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Đi ở làn bên phải trong cùng.", "Đi ở làn phía bên trái.", "Đi ở làn giữa.", "Đi ở bất cứ làn nào nhưng phải bấm đèn cảnh báo nguy hiểm để báo hiệu cho các phương tiện khác.", null, null, "Trên đường có nhiều làn đường, khi điều khiển phương tiện ở tốc độ chậm bạn phải đi ở làn đường nào?", false },
                    { new Guid("e1d2178f-44fe-4b9a-8766-757e6f7f38ce"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "C", true, null, new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Bị nghiêm cấm tùy từng trường hợp.", "Không bị nghiêm cấm.", "Bị nghiêm cấm.", null, null, null, "Hành vi điều khiển xe cơ giới chạy quá tốc độ quy định, giành đường, vượt ẩu có bị nghiêm cấm hay không?", false },
                    { new Guid("e33caaa1-67bb-48fc-8247-ac19d87a7057"), new Guid("cdcfb0da-a22d-4891-bac4-1fea55614508"), "D", false, "Cả ý 1 và ý 2 đều đúng. Ý 3 tránh nhau ban đêm bật đèn pha là sai.", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Không nên đi cố vào đường hẹp...", "Trong khi tránh nhau không nên đổi số...", "Khi tránh nhau ban đêm, phải thường xuyên bật đèn pha tắt đèn cốt.", "Cả ý 1 và ý 2.", null, null, "Khi tránh nhau trên đường hẹp, người lái xe cần phải chú ý những điểm nào để đảm bảo an toàn giao thông?", false },
                    { new Guid("eab73797-6fe7-4401-a2e2-c25e627534d7"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "A", true, "Tốc độ chậm đi ở làn bên phải trong cùng", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Đi ở làn bên phải trong cùng.", "Đi ở làn phía bên trái.", "Đi ở làn giữa.", "Đi ở bất cứ làn nào nhưng phải bấm đèn cảnh báo nguy hiểm để báo hiệu cho các phương tiện khác.", null, null, "Trên đường có nhiều làn đường, khi điều khiển phương tiện ở tốc độ chậm bạn phải đi ở làn đường nào?", false },
                    { new Guid("ee39efa3-5791-40d3-a0e4-71c56ad67046"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "B", true, "Không được phép giao xe cho người không đủ điều kiện tham gia giao thông.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Chỉ được thực hiện nếu đã hướng dẫn đầy đủ.", "Không được phép.", "Được phép tùy từng trường hợp.", "Chỉ được phép thực hiện với thành viên trong gia đình.", null, null, "Hành vi giao xe cơ giới, xe máy chuyên dùng cho người không đủ điều kiện để điều khiển xe tham gia giao thông có được phép hay không?", false },
                    { new Guid("f7007013-7bb6-4c61-b1e0-b7bf8daa9b8e"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "C", true, null, new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Bị nghiêm cấm tùy từng trường hợp.", "Không bị nghiêm cấm.", "Bị nghiêm cấm.", null, null, null, "Hành vi điều khiển xe cơ giới chạy quá tốc độ quy định, giành đường, vượt ẩu có bị nghiêm cấm hay không?", false },
                    { new Guid("f74e7ee0-4b3c-4635-a279-e2642106afc5"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "D", false, "Cấm lùi xe ở khu vực cấm dừng và nơi đường bộ giao nhau.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Ở khu vực cho phép đỗ xe.", "Ở khu vực cấm dừng và trên phần đường dành cho người đi bộ qua đường.", "Nơi đường bộ giao nhau, đường bộ giao nhau cùng mức với đường sắt, nơi tầm nhìn bị che khuất, trong hầm đường bộ, đường cao tốc.", "Cả ý 2 và ý 3.", null, null, "Người lái xe không được lùi xe ở những khu vực nào dưới đây?", false },
                    { new Guid("f8468803-69d9-4e31-b979-8b203bc267e1"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "B", false, "Độ tuổi lấy bằng theo hạng (cách nhau 3 tuổi): 16: Xe dưới 50cm3; 18: Hạng A, B1, B2; 21: Hạng C, FB; 24: Hạng D, FC; 27: Hạng E, FD.", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "16 tuổi.", "18 tuổi.", "17 tuổi.", "", null, null, "Người đủ bao nhiêu tuổi trở lên thì được điều khiển xe mô tô hai bánh, xe mô tô ba bánh có dung tích xi lanh từ 50 cm3 trở lên và các loại xe có kết cấu tương tự; xe ô tô tải, máy kéo có trọng tải dưới 3.500 kg; xe ô tô chở người đến 9 chỗ ngồi?", false },
                    { new Guid("fb056cb8-1fe7-4e3c-9355-375a41dbdeb1"), new Guid("1365e7dd-6325-4662-ba5a-fe0312d5519a"), "B", false, "Không được phép thay đổi so với giấy chứng nhận đăng ký xe.", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Được phép thay đổi bằng cách dán đề can với màu sắc phù hợp.", "Không được phép thay đổi.", "Tùy từng loại phương tiện cơ giới đường bộ.", "", null, null, "Chủ phương tiện cơ giới đường bộ có được tự ý thay đổi màu sơn...", false },
                    { new Guid("fb377bf2-a274-405b-b9e0-d981823b8a3e"), new Guid("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"), "B", true, "Không được dừng, đỗ xe trên miệng cống thoát nước.", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), null, "Được dừng xe, đỗ xe trong trường hợp cần thiết.", "Không được dừng xe, đỗ xe.", "Được dừng xe, không được đỗ xe.", null, null, null, "Người điều khiển phương tiện giao thông trên đường phố có được dừng xe, đỗ xe trên miệng cống thoát nước, miệng hầm của đường điện thoại, điện cao thế, chỗ dành riêng cho xe chữa cháy lấy nước hay không?", false },
                    { new Guid("fc2e0758-e3e8-4bcc-8388-74a81ff4c06f"), new Guid("78627311-083d-42f9-a921-168e88e5f187"), "A", true, "nội dung 1", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), "image", "1", "1", "1", null, "/images/d3fe7cfb-08ad-4626-9736-c53189250a7b.png", "nội dung 1", "nội dung 1", true },
                    { new Guid("fd2bcfe7-baf1-41c2-ae87-5a353bb6de65"), new Guid("cdcfb0da-a22d-4891-bac4-1fea55614508"), "A", false, "Thực hiện quay đầu xe với tốc độ thấp.", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Quan sát biển báo hiệu để biết nơi được phép quay đầu...", "Quan sát biển báo hiệu để biết nơi được phép quay đầu...", "", "", null, null, "Khi quay đầu xe, người lái xe cần phải quan sát và thực hiện các thao tác nào để đảm bảo an toàn giao thông?", false },
                    { new Guid("fecde70e-505e-4dd4-bd1f-6374bbcd0760"), new Guid("51923743-b8a3-42e8-9810-219fcffaa9ee"), "C", false, "", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Có trách nhiệm với bản thân và với cộng đồng...", "Tận tình giúp đỡ người tham gia giao thông gặp hoạn nạn...", "Cả ý 1 và ý 2.", "", null, null, "Người lái xe có văn hóa khi tham gia giao thông phải đáp ứng các điều kiện nào dưới đây?", false },
                    { new Guid("ff6d4a7d-b623-48c8-abe0-d0aad856cbc0"), new Guid("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"), "D", false, "Cả ý 1 và ý 2 đều đúng. Bởi vì không thể chở hành khách trên mui được, nên ý 3 sai.", new Guid("f5f61a67-1fc5-4917-8d76-e184b6d83848"), null, "Đón, trả khách đúng nơi quy định, không trở hành khách trên mui, trong khoang hành lý hoặc để hành khách đu bám bên ngoài xe.", "Không chở hàng nguy hiểm, hàng có mùi hôi thối hoặc động vật, hàng hóa khác có ảnh hưởng đến sức khỏe của hành khách.", "Chở hành khách trên mui; để hàng hóa trong khoang chở khách, chở quá số người theo quy định.", "Cả ý 1 và ý 2.", null, null, "Người lái xe khách phải chấp hành những quy định nào dưới đây?", false }
                });

            migrationBuilder.InsertData(
                table: "MoPhongs",
                columns: new[] { "Id", "DapAn", "LoaiBangLaiId", "NoiDung", "VideoUrl" },
                values: new object[,]
                {
                    { new Guid("1c32fc5d-51a6-4349-979d-c6d7b5d369ed"), "18.357427,19.170872,20.296029,21.334759,22.189645,23.587884", new Guid("de0f2329-c8c2-4736-9ea3-107239c304a1"), "Người đi bộ vượt đèn đỏ sang đường", "/videos/876b7aef-94dc-46e1-8d67-2b5f016e50ed.mp4" },
                    { new Guid("8e7344e6-7483-4912-bc52-26ccd1f4f3dc"), "0.806122,1.060729,1.320611,1.563675,1.816014,2.078444", new Guid("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"), "Nội dung 1", "/videos/823c2df0-3f5e-4dad-8ca2-663989bb3c37.mp4" },
                    { new Guid("a465960a-1525-4140-96b3-284205133163"), "10,11,12,13,14,15", new Guid("de0f2329-c8c2-4736-9ea3-107239c304a1"), "Người đi bộ sang đường bị khuất sau xe tải", "/videos/cau1.mp4" },
                    { new Guid("de0f2329-c8c2-4736-9ea3-107239c304a1"), "10,11,12,13,14,15", new Guid("de0f2329-c8c2-4736-9ea3-107239c304a1"), "Người đi bộ sang đường bị khuất sau xe tải", "/videos/cau1.mp4" }
                });

            migrationBuilder.InsertData(
                table: "ChiTietBaiThis",
                columns: new[] { "Id", "BaiThiId", "CauHoiId" },
                values: new object[,]
                {
                    { new Guid("04135ce2-4223-423f-bf4e-17d8622cd4a5"), new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), new Guid("62f88d57-380c-49c6-8dc6-418133f8c194") },
                    { new Guid("07d4dab3-fdf8-407b-9dac-85010738eed5"), new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), new Guid("14be8af4-5d8f-4c40-a51e-d323d23fbcd9") },
                    { new Guid("120b9ee0-57f8-4ed3-a534-fc87618f3c12"), new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), new Guid("0e418bb1-c0a8-429c-964d-08d8b97149bf") },
                    { new Guid("14a46bcd-158a-449e-92e2-0a40cd026bec"), new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), new Guid("2e311460-e16f-44e1-981c-1c40c1268c1a") },
                    { new Guid("154f3758-9a3b-486c-b11c-494ddd248aa4"), new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), new Guid("71482e7a-3b46-415c-b38b-50ac587d8b18") },
                    { new Guid("18107078-8e3a-4db6-9c01-4b16eade4895"), new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), new Guid("9ecc10c6-4184-49b1-8417-2ccac03588de") },
                    { new Guid("2df9031a-02d6-414f-88e0-f75518c89e52"), new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), new Guid("2892a1a3-c46d-4dd0-8c64-da93d5432c69") },
                    { new Guid("4c59ce7b-7582-41fb-8481-707314803dae"), new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), new Guid("b0e3fc57-e1f5-4d66-a275-d80e1521cd0d") },
                    { new Guid("4ff2476f-687d-441d-a12b-b523523b4a5d"), new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), new Guid("e1d2178f-44fe-4b9a-8766-757e6f7f38ce") },
                    { new Guid("54fce508-5f8c-41b8-abdf-199d61af8094"), new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), new Guid("cf39ba06-f565-4f78-8f21-354b90b54dbc") },
                    { new Guid("6647a7d5-bee3-4511-8d66-2a0dfd8b25c6"), new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), new Guid("c386c5d2-a62c-46d0-90fc-28c7b066a298") },
                    { new Guid("68108da6-bceb-4747-ad52-283fe3a5a3a0"), new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), new Guid("de1a4aa2-7f7a-4ce4-9abf-84979173ef35") },
                    { new Guid("6d5c8536-fbc1-4296-b71b-5c84e9294ab4"), new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), new Guid("1760daa6-f038-4f65-8531-418edc1056f8") },
                    { new Guid("74b69f03-ac0e-48c8-bee9-946b6a4d7f00"), new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), new Guid("537e9dd8-6ad4-4c73-93f5-c651354b7d14") },
                    { new Guid("8904a321-5dd6-44ee-aed7-403f23092a33"), new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), new Guid("3393dbff-9dee-4f39-9d0d-08219b69d1ed") },
                    { new Guid("89116701-a827-4acd-8b2d-d02780ca362e"), new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), new Guid("e1a5b85f-3b8d-4213-aad5-9f3ddca19f29") },
                    { new Guid("96d6bbc6-da76-4a88-9c54-c6b798fb4e58"), new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), new Guid("ce50b6f1-6abc-481b-8a26-5fc8946efba0") },
                    { new Guid("98714fe1-fdb6-4486-8fb2-2ec114e50838"), new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), new Guid("fc2e0758-e3e8-4bcc-8388-74a81ff4c06f") },
                    { new Guid("99f65f26-d3c8-47c9-9dac-85010738eed5"), new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), new Guid("9c7a8aa0-8976-47ce-854d-6ade9c767913") },
                    { new Guid("b8a61fac-5b0e-42d2-bdca-4beee01dcb72"), new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), new Guid("2e88303a-6350-4a59-bb0f-4829665fea92") },
                    { new Guid("bfd90d24-eb69-4796-a14c-09eec597a3de"), new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), new Guid("057a01a9-d102-4013-8d4e-40e580010abe") },
                    { new Guid("db79b5c3-3570-4604-97ba-95d504b2d572"), new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), new Guid("755996ec-0569-4a13-aeb4-28b38b33d0d3") },
                    { new Guid("dc0a953c-a089-419d-ae45-02e7cbfe39b7"), new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), new Guid("5fab968e-1c99-47f8-954e-b7d8750594ea") },
                    { new Guid("de9c9382-5245-4ecb-b515-662188b8d0a7"), new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), new Guid("33ca0bba-4d98-429a-9aee-2ddcf4cd4778") },
                    { new Guid("e10a6435-7b18-48d5-b7a4-c2e4cf84c8a3"), new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), new Guid("459f6775-e179-4288-9c2d-1d725cf3b6d8") },
                    { new Guid("fdb59178-9329-48a3-badd-a3c2d8b84eab"), new Guid("beb046b2-7109-481f-9e9e-5c2804c178a0"), new Guid("160cfe60-432a-48d8-b501-4d288bfe2d5a") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BaiSaHinhs_LoaiBangLaiId",
                table: "BaiSaHinhs",
                column: "LoaiBangLaiId");

            migrationBuilder.CreateIndex(
                name: "IX_CauHois_ChuDeId",
                table: "CauHois",
                column: "ChuDeId");

            migrationBuilder.CreateIndex(
                name: "IX_CauHois_LoaiBangLaiId",
                table: "CauHois",
                column: "LoaiBangLaiId");

            migrationBuilder.CreateIndex(
                name: "IX_CauHoiSais_CauHoiId",
                table: "CauHoiSais",
                column: "CauHoiId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBaiThis_BaiThiId",
                table: "ChiTietBaiThis",
                column: "BaiThiId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBaiThis_CauHoiId",
                table: "ChiTietBaiThis",
                column: "CauHoiId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietLichSuThis_CauHoiId",
                table: "ChiTietLichSuThis",
                column: "CauHoiId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietLichSuThis_LichSuThiId",
                table: "ChiTietLichSuThis",
                column: "LichSuThiId");

            migrationBuilder.CreateIndex(
                name: "IX_GiaoDichThanhToans_MaDonCong",
                table: "GiaoDichThanhToans",
                column: "MaDonCong");

            migrationBuilder.CreateIndex(
                name: "IX_GiaoDichThanhToans_MaGiaoDichCuoi",
                table: "GiaoDichThanhToans",
                column: "MaGiaoDichCuoi",
                unique: true,
                filter: "[MaGiaoDichCuoi] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GiaoDichThanhToans_TrangThai_NgayTao",
                table: "GiaoDichThanhToans",
                columns: new[] { "TrangThai", "NgayTao" });

            migrationBuilder.CreateIndex(
                name: "IX_MoPhongs_LoaiBangLaiId",
                table: "MoPhongs",
                column: "LoaiBangLaiId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareReplies_ShareId",
                table: "ShareReplies",
                column: "ShareId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareReports_ShareId",
                table: "ShareReports",
                column: "ShareId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareReports_ShareReplyId",
                table: "ShareReports",
                column: "ShareReplyId");

            migrationBuilder.CreateIndex(
                name: "UX_User_TinhNang_Active",
                table: "TinhNangMoKhoas",
                columns: new[] { "UserId", "TenTinhNang", "DangHoatDong" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BaiSaHinhs");

            migrationBuilder.DropTable(
                name: "CauHoiSais");

            migrationBuilder.DropTable(
                name: "ChiTietBaiThis");

            migrationBuilder.DropTable(
                name: "ChiTietLichSuThis");

            migrationBuilder.DropTable(
                name: "DonHangs");

            migrationBuilder.DropTable(
                name: "GiaoDichThanhToans");

            migrationBuilder.DropTable(
                name: "MoPhongs");

            migrationBuilder.DropTable(
                name: "ShareReports");

            migrationBuilder.DropTable(
                name: "TinhNangMoKhoas");

            migrationBuilder.DropTable(
                name: "VisitLogs");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BaiThis");

            migrationBuilder.DropTable(
                name: "CauHois");

            migrationBuilder.DropTable(
                name: "LichSuThis");

            migrationBuilder.DropTable(
                name: "ShareReplies");

            migrationBuilder.DropTable(
                name: "ChuDes");

            migrationBuilder.DropTable(
                name: "LoaiBangLais");

            migrationBuilder.DropTable(
                name: "Shares");
        }
    }
}
