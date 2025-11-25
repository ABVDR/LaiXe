using Libs.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Repositories
{
    public interface ITinhNangMoKhoaRepository
    {
        Task<TinhNangMoKhoa?> GetByUserAndFeatureAsync(string userId, string tenTinhNang, CancellationToken ct = default);
        Task<TinhNangMoKhoa> CreateAsync(string userId, string tenTinhNang, long donHangId, decimal soTien, CancellationToken ct = default);
        Task ActivateAsync(long donHangId, CancellationToken ct = default);
    }
    public class TinhNangMoKhoaRepository : ITinhNangMoKhoaRepository
    {
        private readonly ApplicationDbContext _db;

        public TinhNangMoKhoaRepository(ApplicationDbContext db) => _db = db;

        public Task<TinhNangMoKhoa?> GetByUserAndFeatureAsync(string userId, string tenTinhNang, CancellationToken ct = default)
        {
            return _db.Set<TinhNangMoKhoa>()
                .FirstOrDefaultAsync(p => p.UserId == userId && p.TenTinhNang == tenTinhNang, ct);
        }

        public async Task<TinhNangMoKhoa> CreateAsync(string userId, string tenTinhNang, long donHangId, decimal soTien, CancellationToken ct = default)
        {
            var feature = new TinhNangMoKhoa
            {
                UserId = userId,
                TenTinhNang = tenTinhNang,
                DangHoatDong = false,
                DonHangId = donHangId,
                SoTienDaTra = soTien,
                NgayTao = DateTimeOffset.UtcNow
            };

            _db.Set<TinhNangMoKhoa>().Add(feature);
            await _db.SaveChangesAsync(ct);
            return feature;
        }

        public async Task ActivateAsync(long donHangId, CancellationToken ct = default)
        {
            var feature = await _db.Set<TinhNangMoKhoa>()
                .FirstOrDefaultAsync(p => p.DonHangId == donHangId, ct);

            if (feature != null)
            {
                feature.DangHoatDong = true;
                feature.KichHoatLuc = DateTimeOffset.UtcNow;
                // Ví dụ: set hạn 30 ngày (tuỳ chính sách)
                feature.HetHanLuc = (feature.HetHanLuc is null || feature.HetHanLuc < DateTimeOffset.UtcNow)
                    ? DateTimeOffset.UtcNow.AddDays(30)
                    : feature.HetHanLuc.Value.AddDays(30);

                await _db.SaveChangesAsync(ct);
            }
        }
    }
}