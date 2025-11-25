using System;
using System.Threading;
using System.Threading.Tasks;
using Libs;                  
using Libs.Entity;
using Libs.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Libs.Service
{
    public interface ITinhNangService
    {
        Task<bool> KiemTraQuyenAsync(string userId, string tenTinhNang, CancellationToken ct = default);
        Task<long> TaoDonHangChoTinhNangAsync(string userId, string tenTinhNang, decimal soTien, CancellationToken ct = default);
    }

    public class TinhNangService : ITinhNangService
    {
        private readonly ITinhNangMoKhoaRepository _repo;
        private readonly ApplicationDbContext _db;

        public TinhNangService(ITinhNangMoKhoaRepository repo, ApplicationDbContext db)
        {
            _repo = repo;
            _db = db;
        }

        /// <summary>
        /// User có quyền dùng tính năng nếu đang hoạt động và chưa hết hạn.
        /// </summary>
        public async Task<bool> KiemTraQuyenAsync(string userId, string tenTinhNang, CancellationToken ct = default)
        {
            var f = await _repo.GetByUserAndFeatureAsync(userId, tenTinhNang, ct);
            return f is not null
                   && f.DangHoatDong
                   && f.HetHanLuc is not null
                   && f.HetHanLuc > DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// Tạo đơn hàng cho tính năng, link với bản ghi TinhNangMoKhoa (chưa kích hoạt).
        /// </summary>
        public async Task<long> TaoDonHangChoTinhNangAsync(string userId, string tenTinhNang, decimal soTien, CancellationToken ct = default)
        {
            if (soTien <= 0m) throw new ArgumentOutOfRangeException(nameof(soTien), "Số tiền phải > 0");

            // (Tuỳ chọn) Nếu đã có bản ghi cùng tính năng đang pending, có thể tái sử dụng.
            var existed = await _repo.GetByUserAndFeatureAsync(userId, tenTinhNang, ct);

            // Tạo đơn hàng
            var don = new DonHang
            {
                TongTien = soTien,
                TrangThai = TrangThaiThanhToan.ChoXuLy,
                NgayTao = DateTimeOffset.UtcNow,
                UserId = userId
            };

            _db.Set<DonHang>().Add(don);
            await _db.SaveChangesAsync(ct);

            // Link với TinhNangMoKhoa (chưa kích hoạt)
            if (existed is null || existed.DangHoatDong)
            {
                await _repo.CreateAsync(userId, tenTinhNang, don.Id, soTien, ct);
            }
            else
            {
                // Nếu đã có record nhưng chưa active: gán lại đơn & cộng tiền (tuỳ chính sách)
                existed.DonHangId = don.Id;
                existed.SoTienDaTra = soTien; // hoặc += soTien nếu muốn dồn
                _db.Update(existed);
                await _db.SaveChangesAsync(ct);
            }

            return don.Id;
        }
    }
}
