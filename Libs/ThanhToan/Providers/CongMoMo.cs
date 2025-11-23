using Libs.ThanhToan.Abstractions;
using Libs.ThanhToan.Options;
using Libs.ThanhToan.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Libs.Repositories;
using IOrderRepository = Libs.Repositories.IDonHangRepository;
using IPaymentTransactionRepository = Libs.Repositories.IGiaoDichThanhToanRepository;
using IPremiumFeatureRepository = Libs.Repositories.ITinhNangMoKhoaRepository;

namespace Libs.ThanhToan.Providers
{
    public sealed class CongMoMo : ICongThanhToan
    {
        private readonly MoMoTuyChon _opt;
        private readonly IOrderRepository _orders;
        private readonly IPaymentTransactionRepository _txRepo;
        private readonly IPremiumFeatureRepository _premiumRepo;

        public string TenCong => "MoMo";

        public CongMoMo(
            IOptions<MoMoTuyChon> opt,
            IOrderRepository orders,
            IPaymentTransactionRepository txRepo,
            IPremiumFeatureRepository premiumRepo)
        {
            _opt = opt.Value;
            _orders = orders;
            _txRepo = txRepo;
            _premiumRepo = premiumRepo;
        }

        // =======================================================
        // TẠO PHIÊN THANH TOÁN MOMO
        // =======================================================
        public async Task<TaoPhienThanhToanResult> TaoPhienAsync(long donHangId, string returnUrl, CancellationToken ct)
        {
            var order = await _orders.GetAsync(donHangId, ct);
            var amount = Convert.ToInt64(Math.Round(order.TongTien, 0))
                          .ToString(CultureInfo.InvariantCulture);

            // 🔥 orderId MoMo phải UNIQUE → tránh lỗi trùng orderId
            var momoOrderId = $"{donHangId}_{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";
            var requestId = Guid.NewGuid().ToString("N");
            var orderInfo = $"Mo khoa tinh nang Luyen Cau Sai - Don #{donHangId}";

            // Tạo chuỗi raw để tạo chữ ký
            string raw =
                $"accessKey={_opt.AccessKey}&amount={amount}&extraData=&ipnUrl={_opt.NotifyUrl}" +
                $"&orderId={momoOrderId}&orderInfo={orderInfo}&partnerCode={_opt.PartnerCode}" +
                $"&redirectUrl={returnUrl}&requestId={requestId}&requestType={_opt.RequestType}";

            string signature = MoMoChuKy.Ky(raw, _opt.SecretKey);

            var payload = new
            {
                partnerCode = _opt.PartnerCode,
                accessKey = _opt.AccessKey,
                requestId,
                amount,
                orderId = momoOrderId,     // 🔥 luôn unique
                orderInfo,
                redirectUrl = returnUrl,
                ipnUrl = _opt.NotifyUrl,
                requestType = _opt.RequestType,
                extraData = "",
                signature,
                lang = "vi"
            };

            using var http = new HttpClient();
            var res = await http.PostAsJsonAsync(_opt.Endpoint, payload, ct);

            if (!res.IsSuccessStatusCode)
                return new(false, null, null, $"MoMo lỗi HTTP {(int)res.StatusCode}");

            var json = await res.Content.ReadFromJsonAsync<Dictionary<string, object>>(cancellationToken: ct);

            // Lấy payUrl hoặc deeplink
            json.TryGetValue("payUrl", out var payUrlObj);
            json.TryGetValue("deeplink", out var deepLinkObj);
            json.TryGetValue("qrCodeUrl", out var qrObj);

            var payUrl = payUrlObj?.ToString()
                       ?? deepLinkObj?.ToString()
                       ?? qrObj?.ToString();

            var gwOrderId = momoOrderId;

            // Lưu giao dịch
            await _txRepo.CreatePendingAsync(donHangId, TenCong, gwOrderId, ct);
            await _txRepo.AttachGatewayAsync(donHangId, TenCong, requestId, gwOrderId, ct);

            if (payUrl == null)
            {
                string reason = json.ContainsKey("message") ? json["message"]?.ToString()! : "Unknown error";
                return new(false, null, null, $"MoMo create failed: {reason}");
            }

            return new(true, payUrl, gwOrderId, null);
        }

        // =======================================================
        // XỬ LÝ WEBHOOK MOMO
        // =======================================================
        public async Task<bool> XuLyWebhookAsync(HttpRequest req, CancellationToken ct)
        {
            string body;
            using (var reader = new StreamReader(req.Body))
                body = await reader.ReadToEndAsync(ct);

            if (string.IsNullOrWhiteSpace(body))
                return false;

            MoMoIpnDto? ipn;

            try
            {
                ipn = JsonSerializer.Deserialize<MoMoIpnDto>(body);
            }
            catch
            {
                return false;
            }

            if (ipn is null)
                return false;

            // Kiểm tra chữ ký
            string raw = $"accessKey={_opt.AccessKey}&amount={ipn.amount}&extraData={ipn.extraData}" +
                         $"&message={ipn.message}&orderId={ipn.orderId}&orderInfo={ipn.orderInfo}" +
                         $"&orderType={ipn.orderType}&partnerCode={ipn.partnerCode}&payType={ipn.payType}" +
                         $"&requestId={ipn.requestId}&responseTime={ipn.responseTime}&resultCode={ipn.resultCode}" +
                         $"&transId={ipn.transId}";

            var ok = string.Equals(MoMoChuKy.Ky(raw, _opt.SecretKey), ipn.signature, StringComparison.OrdinalIgnoreCase);

            if (!ok)
                return false;

            var donHangId = long.Parse(ipn.orderId.Split('_')[0]); // lấy DonHangId ban đầu

            if (ipn.resultCode == 0)
            {
                await _txRepo.MarkPaidAsync(donHangId, TenCong, ipn.transId.ToString(), ct);
                await _premiumRepo.ActivateAsync(donHangId, ct);
            }
            else
            {
                await _txRepo.MarkFailedAsync(donHangId, TenCong, ipn.message, ct);
            }

            return true;
        }

        // DTO IPN
        private sealed class MoMoIpnDto
        {
            public string partnerCode { get; set; } = default!;
            public string orderId { get; set; } = default!;
            public string requestId { get; set; } = default!;
            public long amount { get; set; }       // ← phải là long
            public string orderInfo { get; set; } = default!;
            public string orderType { get; set; } = default!;
            public long transId { get; set; }
            public int resultCode { get; set; }
            public string message { get; set; } = default!;
            public long responseTime { get; set; }
            public string payType { get; set; } = default!;
            public string extraData { get; set; } = default!;
            public string signature { get; set; } = default!;
        }

    }
}
