using Libs.Entity;
using Libs.Models;
using Microsoft.EntityFrameworkCore;

namespace Libs.Repositories
{
    public interface IDoanhThuRepository
    {
        Task<List<DonHang>> LayDonHangTheoKhoangAsync(DateTime tuNgay, DateTime denNgay, CancellationToken ct);
        Task<List<GiaoDichThanhToan>> LayGiaoDichAsync(CancellationToken ct);
        Task<PagedResult<GiaoDichItemDto>> LayDanhSachGiaoDichAsync(DoanhThuFilterDto filter, CancellationToken ct);
        Task<GiaoDichThanhToan?> LayGiaoDichTheoIdAsync(long id, CancellationToken ct);
        Task<DonHang?> LayDonHangTheoIdAsync(long id, CancellationToken ct);
        Task<ChiTietDonHangDto?> LayChiTietDonHangAsync(long id, CancellationToken ct);
    }


    public class DoanhThuRepository : IDoanhThuRepository
    {
        private readonly ApplicationDbContext _db;
        public DoanhThuRepository(ApplicationDbContext db) => _db = db;

        public Task<List<DonHang>> LayDonHangTheoKhoangAsync(DateTime tuNgay, DateTime denNgay, CancellationToken ct)
        {
            return _db.DonHangs
                .Where(x => x.NgayTao >= tuNgay && x.NgayTao <= denNgay)
                .ToListAsync(ct);
        }

        public Task<List<GiaoDichThanhToan>> LayGiaoDichAsync(CancellationToken ct)
        {
            return _db.GiaoDichThanhToans.ToListAsync(ct);
        }

        public async Task<PagedResult<GiaoDichItemDto>> LayDanhSachGiaoDichAsync(DoanhThuFilterDto filter, CancellationToken ct)
        {
            var query =
                from gd in _db.GiaoDichThanhToans
                join dh in _db.DonHangs on gd.DonHangId equals dh.Id
                select new { gd, dh };

            // ================== Bộ lọc ==================
            if (filter.TuNgay.HasValue)
                query = query.Where(x => x.gd.NgayTao >= filter.TuNgay.Value);

            if (filter.DenNgay.HasValue)
                query = query.Where(x => x.gd.NgayTao <= filter.DenNgay.Value);

            if (!string.IsNullOrEmpty(filter.CongThanhToan))
                query = query.Where(x => x.gd.CongThanhToan == filter.CongThanhToan);

            if (filter.TrangThai >= 0)
                query = query.Where(x => (int)x.gd.TrangThai == filter.TrangThai);

            // Tổng dòng
            var total = await query.CountAsync(ct);

            // ================== Phân trang ==================
            var items = await query
                .OrderByDescending(x => x.gd.NgayTao)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(x => new GiaoDichItemDto
                {
                    Id = x.gd.Id,
                    DonHangId = x.gd.DonHangId,
                    CongThanhToan = x.gd.CongThanhToan,
                    SoTien = x.dh.TongTien,
                    TrangThai = (int)x.gd.TrangThai,
                    NgayTao = x.gd.NgayTao,
                    NgayCapNhat = x.gd.NgayCapNhat
                })
                .ToListAsync(ct);

            return new PagedResult<GiaoDichItemDto>
            {
                TotalItems = total,
                Items = items
            };
        }

        public Task<GiaoDichThanhToan?> LayGiaoDichTheoIdAsync(long id, CancellationToken ct)
        {
            return _db.GiaoDichThanhToans.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public Task<DonHang?> LayDonHangTheoIdAsync(long id, CancellationToken ct)
        {
            return _db.DonHangs.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<ChiTietDonHangDto?> LayChiTietDonHangAsync(long id, CancellationToken ct)
        {
            var dh = await _db.DonHangs
                .FirstOrDefaultAsync(x => x.Id == id, ct);

            if (dh == null)
                return null;

            var gd = await _db.GiaoDichThanhToans
                .Where(x => x.DonHangId == id)
                .OrderByDescending(x => x.NgayTao)
                .FirstOrDefaultAsync(ct);

            return new ChiTietDonHangDto
            {
                DonHangId = dh.Id,
                TongTien = dh.TongTien,
                TrangThai = dh.TrangThai,
                NgayTao = dh.NgayTao,
                UserId = dh.UserId,

                CongThanhToan = gd?.CongThanhToan,
                MaGiaoDich = gd?.MaGiaoDichCuoi,
                TrangThaiGiaoDich = gd?.TrangThai,
                GhiChu = gd?.ThongBaoLoi
            };
        }
    }
}
