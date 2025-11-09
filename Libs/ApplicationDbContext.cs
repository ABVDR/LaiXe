using Libs.Entity;
using Libs.Extensions;
using Libs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Libs
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<ChuDe> ChuDes { get; set; }
        public DbSet<LoaiBangLai> LoaiBangLais { get; set; }
        public DbSet<CauHoi> CauHois { get; set; }
        public DbSet<BaiThi> BaiThis { get; set; }
        public DbSet<ChiTietBaiThi> ChiTietBaiThis { get; set; }
        public DbSet<CauHoiSai> CauHoiSais { get; set; }

        public DbSet<BaiSaHinh> BaiSaHinhs { get; set; }
        public DbSet<LichSuThi> LichSuThis { get; set; }
        public DbSet<ChiTietLichSuThi> ChiTietLichSuThis { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<ShareReply> ShareReplies { get; set; }
        public DbSet<ShareReport> ShareReports { get; set; }
        public DbSet<VisitLog> VisitLogs { get; set; }
        public DbSet<MoPhong> MoPhongs { get; set; }

        //them pthuc thanh toan
        public DbSet<DonHang> DonHangs { get; set; } = default!;
        public DbSet<GiaoDichThanhToan> GiaoDichThanhToans { get; set; } = default!;
        public DbSet<TinhNangMoKhoa> TinhNangMoKhoas { get; set; } = default!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //them thanh toan cau hinhf moi 
            base.OnModelCreating(modelBuilder);

            // ====== CẤU HÌNH CHO CÁC BẢNG MỚI ======

            // DECIMAL precision (tránh cảnh báo và cắt số)
            modelBuilder.Entity<DonHang>()
                .Property(x => x.TongTien)
                .HasPrecision(18, 2);

            modelBuilder.Entity<TinhNangMoKhoa>()
                .Property(x => x.SoTienDaTra)
                .HasPrecision(18, 2);

            // Index/ràng buộc phục vụ truy vấn nhanh & idempotency
            modelBuilder.Entity<TinhNangMoKhoa>()
                .HasIndex(x => new { x.UserId, x.TenTinhNang, x.DangHoatDong })
                .HasDatabaseName("UX_User_TinhNang_Active");

            modelBuilder.Entity<GiaoDichThanhToan>()
                .HasIndex(x => x.MaDonCong); // có thể lặp do cổng gọi notify nhiều lần

            modelBuilder.Entity<GiaoDichThanhToan>()
                .HasIndex(x => x.MaGiaoDichCuoi)
                .IsUnique(); // duy nhất để chống xử lý trùng

            modelBuilder.Entity<GiaoDichThanhToan>()
                .HasIndex(x => new { x.TrangThai, x.NgayTao });

            // (Tuỳ chọn) Default UTC ở DB cho cột thời gian nếu muốn nhất quán do DB cấp
            modelBuilder.Entity<DonHang>()
                .Property(x => x.NgayTao)
                .HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<GiaoDichThanhToan>()
                .Property(x => x.NgayTao)
                .HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<TinhNangMoKhoa>()
                .Property(x => x.NgayTao)
                .HasDefaultValueSql("GETUTCDATE()");

            /////
            base.OnModelCreating(modelBuilder);
            Console.WriteLine("🚀 Application is seeding data..............................................................");
            modelBuilder.SeedingData();
        }
    }
}
