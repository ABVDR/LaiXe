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

        // Thanh toán
        public DbSet<DonHang> DonHangs { get; set; } = default!;
        public DbSet<GiaoDichThanhToan> GiaoDichThanhToans { get; set; } = default!;
        public DbSet<TinhNangMoKhoa> TinhNangMoKhoas { get; set; } = default!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ⚡ Gọi base một lần duy nhất
            base.OnModelCreating(modelBuilder);

            // ==========================
            // 1) CẤU HÌNH DECIMAL
            // ==========================
            modelBuilder.Entity<DonHang>()
                .Property(x => x.TongTien)
                .HasPrecision(18, 2);

            modelBuilder.Entity<TinhNangMoKhoa>()
                .Property(x => x.SoTienDaTra)
                .HasPrecision(18, 2);

            // ==========================
            // 2) INDEX TỐI ƯU
            // ==========================
            modelBuilder.Entity<TinhNangMoKhoa>()
                .HasIndex(x => new { x.UserId, x.TenTinhNang, x.DangHoatDong })
                .HasDatabaseName("UX_User_TinhNang_Active");

            modelBuilder.Entity<GiaoDichThanhToan>()
                .HasIndex(x => x.MaDonCong);

            modelBuilder.Entity<GiaoDichThanhToan>()
                .HasIndex(x => x.MaGiaoDichCuoi)
                .IsUnique();

            modelBuilder.Entity<GiaoDichThanhToan>()
                .HasIndex(x => new { x.TrangThai, x.NgayTao });

            // ==========================
            // 3) DEFAULT TIME UTC
            // ==========================
            modelBuilder.Entity<DonHang>()
                .Property(x => x.NgayTao)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<GiaoDichThanhToan>()
                .Property(x => x.NgayTao)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<TinhNangMoKhoa>()
                .Property(x => x.NgayTao)
                .HasDefaultValueSql("GETUTCDATE()");

            // ==========================
            // 4) QUAN HỆ DonHang → User
            // ==========================
            //modelBuilder.Entity<DonHang>()
            //    .HasOne(d => d.User)
            //    .WithMany()
            //    .HasForeignKey(d => d.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            // ==========================
            // 5) SEED DATA
            // ==========================
            Console.WriteLine("🚀 Application is seeding data...");
            modelBuilder.SeedingData();
        }
    }
}
