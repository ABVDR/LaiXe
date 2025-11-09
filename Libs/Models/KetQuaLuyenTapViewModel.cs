using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Models
{
    public class KetQuaLuyenTapViewModel
    {
        public Guid CauHoiId { get; set; }
        public string NoiDung { get; set; }
        public string LuaChonA { get; set; }
        public string LuaChonB { get; set; }
        public string LuaChonC { get; set; }
        public string LuaChonD { get; set; }
        public string DapAnDung { get; set; }
        public string DapAnNguoiDung { get; set; }
        public bool IsCorrect { get; set; }
    }
}
