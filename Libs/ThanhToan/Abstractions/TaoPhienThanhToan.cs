using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PaymentMethod = Libs.Entity.PhuongThucThanhToan;

namespace Libs.ThanhToan.Abstractions
{
    public record TaoPhienThanhToanRequest(long DonHangId, PaymentMethod PhuongThuc, string ReturnUrl);

    public record TaoPhienThanhToanResult(
        bool Ok,
        string? RedirectUrl,
        string? MaDonCuaCong,  // GatewayOrderId
        string? Error);
}