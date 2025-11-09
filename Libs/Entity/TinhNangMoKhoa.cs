using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Entity
{
    public class TinhNangMoKhoa
    {
        public long Id { get; set; }

        // Giữ UserId để tương thích Identity
        [Required]
        public string UserId { get; set; } = default!;

        [Required, MaxLength(64)]          // ví dụ: "LuyenCauSai"
        public string TenTinhNang { get; set; } = default!;

        public bool DangHoatDong { get; set; }

        public DateTimeOffset? KichHoatLuc { get; set; }
        public DateTimeOffset? HetHanLuc { get; set; }

        public long? DonHangId { get; set; }

        // decimal — sẽ cấu hình precision(18,2) trong DbContext
        public decimal SoTienDaTra { get; set; }

        public DateTimeOffset NgayTao { get; set; } = DateTimeOffset.UtcNow;
    }
}
