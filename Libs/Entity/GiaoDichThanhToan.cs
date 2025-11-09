using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Entity
{
    public class GiaoDichThanhToan
    {
        public long Id { get; set; }

        // Khóa ngoại sang DonHang
        public long DonHangId { get; set; }

        [Required, MaxLength(32)]          // "MoMo" / "PayPal"
        public string CongThanhToan { get; set; } = default!;

        [MaxLength(128)]                   // Mã đơn do cổng trả về (có thể lặp do cổng gọi lại notify)
        public string? MaDonCong { get; set; }

        [MaxLength(128)]                   // Mã giao dịch cuối cùng (duy nhất)
        public string? MaGiaoDichCuoi { get; set; }

        public TrangThaiThanhToan TrangThai { get; set; } = TrangThaiThanhToan.ChoXuLy;

        [MaxLength(1024)]
        public string? ThongBaoLoi { get; set; }

        public DateTimeOffset NgayTao { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? NgayCapNhat { get; set; }
    }
}
