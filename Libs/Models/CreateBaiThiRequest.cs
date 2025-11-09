using System;
using System.Collections.Generic;

namespace Libs.Models
{
    public class CreateBaiThiRequest
    {
        public string TenBaiThi { get; set; }
        public List<ChiTietBaiThiRequest> ChiTietBaiThis { get; set; }
    }

    public class ChiTietBaiThiRequest
    {
        public Guid CauHoiId { get; set; }
    }
}