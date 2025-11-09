using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.ThanhToan.Options
{
    public sealed class MoMoTuyChon
    {
        public string Endpoint { get; set; } = default!;
        public string PartnerCode { get; set; } = default!;
        public string AccessKey { get; set; } = default!;
        public string SecretKey { get; set; } = default!;
        public string RequestType { get; set; } = "captureWallet";
        public string NotifyUrl { get; set; } = default!;
    }
}
