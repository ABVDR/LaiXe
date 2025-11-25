using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Models
{
    public class DoanhThuDashboardDto
    {
        public decimal TongDoanhThu { get; set; }
        public int TongSoDon { get; set; }
        public int SoDonThanhCong { get; set; }
        public int SoDonThatBai { get; set; }

        public Dictionary<string, decimal> DoanhThuTheoNgay { get; set; } = new();
        public Dictionary<string, decimal> DoanhThuTheoThang { get; set; } = new();
        public Dictionary<string, decimal> DoanhThuTheoCong { get; set; } = new();
    }
}
