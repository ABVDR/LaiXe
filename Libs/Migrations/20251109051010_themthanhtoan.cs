using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Libs.Migrations
{
    /// <inheritdoc />
    public partial class themthanhtoan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaoDichs");

            migrationBuilder.CreateTable(
                name: "DonHangs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()")
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
                name: "UX_User_TinhNang_Active",
                table: "TinhNangMoKhoas",
                columns: new[] { "UserId", "TenTinhNang", "DangHoatDong" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonHangs");

            migrationBuilder.DropTable(
                name: "GiaoDichThanhToans");

            migrationBuilder.DropTable(
                name: "TinhNangMoKhoas");

            migrationBuilder.CreateTable(
                name: "GiaoDichs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DaThanhToan = table.Column<bool>(type: "bit", nullable: false),
                    MaGiaoDich = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaoDichs", x => x.Id);
                });
        }
    }
}
