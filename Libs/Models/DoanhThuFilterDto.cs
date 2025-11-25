using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Models
{
    public class DoanhThuFilterDto
    {
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public string? CongThanhToan { get; set; }   // "MoMo", "PayPal" hoặc null
        public int TrangThai { get; set; } = -1;     // -1 = tất cả, 0..3 = enum TrangThaiThanhToan
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public class GiaoDichItemDto
    {
        public long Id { get; set; }
        public long DonHangId { get; set; }
        public string? CongThanhToan { get; set; }
        public decimal SoTien { get; set; }
        public int TrangThai { get; set; }
        public DateTimeOffset NgayTao { get; set; }
        public DateTimeOffset? NgayCapNhat { get; set; }
    }

    public class PagedResult<T>
    {
        public int TotalItems { get; set; }
        public IReadOnlyList<T> Items { get; set; } = Array.Empty<T>();
    }
}
