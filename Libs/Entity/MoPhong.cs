using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Entity
{
    public class MoPhong
    {
        public Guid Id { get; set; }
        public string NoiDung { get; set; }
        public string? VideoUrl { get; set; }
        public string DapAn { get; set; }
        public Guid LoaiBangLaiId { get; set; }
        public LoaiBangLai? LoaiBangLai { get; set; }
    }
}
