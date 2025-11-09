using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.ThanhToan.Abstractions
{
    public interface ICongThanhToan
    {
        string TenCong { get; } // "MoMo" | "PayPal"

        Task<TaoPhienThanhToanResult> TaoPhienAsync(long donHangId, string returnUrl, CancellationToken ct);

        /// <summary>
        /// Xử lý webhook/IPN từ cổng. Trả true nếu đã xử lý hợp lệ.
        /// </summary>
        Task<bool> XuLyWebhookAsync(HttpRequest httpRequest, CancellationToken ct);
    }
}
