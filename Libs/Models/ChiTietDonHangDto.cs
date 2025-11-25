using Libs.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Models
{
    public class ChiTietDonHangDto
    {
        public long DonHangId { get; set; }
        public decimal TongTien { get; set; }
        public TrangThaiThanhToan TrangThai { get; set; }
        public DateTimeOffset NgayTao { get; set; }

        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }

        public string? CongThanhToan { get; set; }
        public string? MaGiaoDich { get; set; }
        public TrangThaiThanhToan? TrangThaiGiaoDich { get; set; }
        public string? GhiChu { get; set; }
    }
}
