using System.ComponentModel.DataAnnotations;

namespace Libs.Entity
{
    public class LichSuThi
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid BaiThiId { get; set; }
        public string TenBaiThi { get; set; }
        public DateTime NgayThi { get; set; }
        public int TongSoCau { get; set; }
        public int SoCauDung { get; set; }
        public double PhanTramDung { get; set; }
        public int Diem { get; set; }
        public string KetQua { get; set; }
        public bool MacLoiNghiemTrong { get; set; }

        public string? UserId { get; set; }

        // Navigation
        public ICollection<ChiTietLichSuThi> ChiTietLichSuThis { get; set; }
    }
}
