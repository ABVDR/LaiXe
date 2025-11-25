using System;

namespace Libs.Models
{
    public class ChiTietGiaoDichDto
    {
        public long Id { get; set; }
        public long DonHangId { get; set; }

        public string? CongThanhToan { get; set; }
        public string? MaDonCong { get; set; }
        public string? MaGiaoDichCuoi { get; set; }

        public int TrangThai { get; set; }

        public DateTimeOffset? NgayTao { get; set; }
        public DateTimeOffset? NgayCapNhat { get; set; }

        public string? UserId { get; set; }
        public decimal SoTien { get; set; }

        public string? GhiChu { get; set; }
    }
}
