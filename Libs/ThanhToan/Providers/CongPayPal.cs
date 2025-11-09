using Libs.ThanhToan.Abstractions;
using Libs.ThanhToan.Options;
using Libs.ThanhToan.Security; // nếu cần chữ ký ở gateway khác
using Libs.ThanhToan.Providers;
using Libs.Repositories;                   // <— QUAN TRỌNG: namespace repo

// ===== ALIAS map sang tên repo tiếng Việt =====
using IOrderRepository = Libs.Repositories.IDonHangRepository;
using IPaymentTransactionRepository = Libs.Repositories.IGiaoDichThanhToanRepository;
using IPremiumFeatureRepository = Libs.Repositories.ITinhNangMoKhoaRepository;
// ==============================================

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Libs.ThanhToan.Providers
{
    public sealed class CongPayPal : ICongThanhToan
    {
        private readonly PayPalTuyChon _opt;
        private readonly IOrderRepository _orders;
        private readonly IPaymentTransactionRepository _txRepo;
        private readonly IPremiumFeatureRepository _premiumRepo;

        public string TenCong => "PayPal";

        public CongPayPal(
            IOptions<PayPalTuyChon> opt,
            IOrderRepository orders,
            IPaymentTransactionRepository txRepo,
            IPremiumFeatureRepository premiumRepo)
        {
            _opt = opt.Value;
            _orders = orders;
            _txRepo = txRepo;
            _premiumRepo = premiumRepo;
        }

        public async Task<TaoPhienThanhToanResult> TaoPhienAsync(long donHangId, string returnUrl, CancellationToken ct)
        {
            // Đảm bảo đơn tồn tại (nếu không sẽ ném lỗi trong repo/service)
            var _ = await _orders.GetAsync(donHangId, ct);

            // Demo: giả lập gatewayOrderId và link phê duyệt
            var gatewayOrderId = Guid.NewGuid().ToString("N");
            var approveLink = $"{_opt.ReturnUrl}?token={gatewayOrderId}";

            await _txRepo.CreatePendingAsync(donHangId, TenCong, gatewayOrderId, ct);
            await _txRepo.AttachGatewayAsync(donHangId, TenCong, gatewayOrderId, gatewayOrderId, ct);

            return new(true, approveLink, gatewayOrderId, null);
        }

        public async Task<bool> XuLyWebhookAsync(HttpRequest req, CancellationToken ct)
        {
            // Demo: lấy tham số từ query (tuỳ thực tế PayPal/IPN)
            var donHangId = long.Parse(req.Query["orderId"]);
            var captureId = req.Query["capId"].ToString();

            await _txRepo.MarkPaidAsync(donHangId, TenCong, captureId, ct);
            await _premiumRepo.ActivateAsync(donHangId, ct);
            return true;
        }
    }
}
