using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Entity
{
    public class DonHang
    {
        public long Id { get; set; }

        // decimal — sẽ cấu hình precision(18,2) trong DbContext
        public decimal TongTien { get; set; }

        public TrangThaiThanhToan TrangThai { get; set; } = TrangThaiThanhToan.ChoXuLy;

        // Dùng UTC nhất quán
        public DateTimeOffset NgayTao { get; set; } = DateTimeOffset.UtcNow;

        public string? UserId { get; set; }





    }
}
