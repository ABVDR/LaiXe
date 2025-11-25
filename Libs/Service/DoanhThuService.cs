using Libs.Entity;
using Libs.Models;
using Libs.Repositories;

namespace Libs.Service
{
    public class DoanhThuService
    {
        private readonly IDoanhThuRepository _repo;

        public DoanhThuService(IDoanhThuRepository repo)
        {
            _repo = repo;
        }

        // =============================
        // 1) Lấy chi tiết đơn hàng
        // =============================
        public Task<ChiTietDonHangDto?> LayChiTietDonHangAsync(long id, CancellationToken ct)
        {
            return _repo.LayChiTietDonHangAsync(id, ct);
        }

        // =============================
        // 2) Dashboard doanh thu
        // =============================
        public async Task<DoanhThuDashboardDto> LayDashboardAsync(
            DateTime? tuNgay,
            DateTime? denNgay,
            CancellationToken ct)
        {
            var from = tuNgay ?? DateTime.UtcNow.AddMonths(-1);
            var to = denNgay ?? DateTime.UtcNow;

            var donHangs = await _repo.LayDonHangTheoKhoangAsync(from, to, ct);
            var giaoDich = await _repo.LayGiaoDichAsync(ct);

            var donThanhCong = donHangs
                .Where(x => x.TrangThai == TrangThaiThanhToan.ThanhCong)
                .ToList();

            var dto = new DoanhThuDashboardDto
            {
                TongSoDon = donHangs.Count,
                SoDonThanhCong = donThanhCong.Count,
                SoDonThatBai = donHangs.Count(x => x.TrangThai == TrangThaiThanhToan.ThatBai),
                TongDoanhThu = donThanhCong.Sum(x => x.TongTien),
            };

            // =============================
            // Doanh thu theo ngày
            // =============================
            dto.DoanhThuTheoNgay = donThanhCong
                .GroupBy(x => x.NgayTao.ToString("yyyy-MM-dd"))
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(x => x.TongTien));

            // =============================
            // Doanh thu theo tháng
            // =============================
            dto.DoanhThuTheoThang = donThanhCong
                .GroupBy(x => x.NgayTao.ToString("yyyy-MM"))
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(x => x.TongTien));

            // =============================
            // Doanh thu theo cổng thanh toán
            // =============================
            dto.DoanhThuTheoCong = giaoDich
                .Where(x => x.TrangThai == TrangThaiThanhToan.ThanhCong)
                .GroupBy(x => x.CongThanhToan)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(gd =>
                        donThanhCong.First(d => d.Id == gd.DonHangId).TongTien));

            return dto;
        }

        // =============================
        // 3) Danh sách giao dịch (Paged)
        // =============================
        public Task<PagedResult<GiaoDichItemDto>> LayDanhSachGiaoDichAsync(
            DoanhThuFilterDto filter,
            CancellationToken ct)
        {
            return _repo.LayDanhSachGiaoDichAsync(filter, ct);
        }

        // =============================
        // 4) Chi tiết giao dịch
        // =============================
       
        public async Task<ChiTietGiaoDichDto?> LayChiTietGiaoDichAsync(long id, CancellationToken ct)
        {
            var gd = await _repo.LayGiaoDichTheoIdAsync(id, ct);
            if (gd == null) return null;

            var dh = await _repo.LayDonHangTheoIdAsync(gd.DonHangId, ct);

            return new ChiTietGiaoDichDto
            {
                Id = gd.Id,
                DonHangId = gd.DonHangId,
                CongThanhToan = gd.CongThanhToan,
                MaDonCong = gd.MaDonCong,
                MaGiaoDichCuoi = gd.MaGiaoDichCuoi,
                TrangThai = (int)gd.TrangThai,
                NgayTao = gd.NgayTao,
                NgayCapNhat = gd.NgayCapNhat,

                // lấy từ DonHang
                UserId = dh?.UserId,
                SoTien = dh?.TongTien ?? 0,

                // ghi chú từ giao dịch
                GhiChu = gd.ThongBaoLoi
            };
        }

    }
}
