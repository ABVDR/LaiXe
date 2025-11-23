using Libs.Repositories;
using Libs.ThanhToan.Abstractions;
using Libs.ThanhToan.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using IOrderRepository = Libs.Repositories.IDonHangRepository;
using IPaymentTransactionRepository = Libs.Repositories.IGiaoDichThanhToanRepository;
using IPremiumFeatureRepository = Libs.Repositories.ITinhNangMoKhoaRepository;

namespace Libs.ThanhToan.Providers
{
    public sealed class CongPayPal : ICongThanhToan
    {
        private readonly PayPalTuyChon _opt;
        private readonly IOrderRepository _orders;
        private readonly IPaymentTransactionRepository _txRepo;
        private readonly IPremiumFeatureRepository _premiumRepo;
        private readonly IHttpClientFactory _http;

        public string TenCong => "PayPal";

        public CongPayPal(
            IOptions<PayPalTuyChon> opt,
            IOrderRepository orders,
            IPaymentTransactionRepository txRepo,
            IPremiumFeatureRepository premiumRepo,
            IHttpClientFactory http)
        {
            _opt = opt.Value;
            _orders = orders;
            _txRepo = txRepo;
            _premiumRepo = premiumRepo;
            _http = http;
        }

        // ======================================================
        // SMART-BUTTON: Tạo PayPal Order
        // ======================================================
        public async Task<(bool Ok, string? PayPalOrderId, string? Error)> TaoOrderAsync(long orderId)
        {
            var order = await _orders.GetAsync(orderId, CancellationToken.None);
            if (order == null)
                return (false, null, "Order not found");

            decimal usd = order.TongTien / 25000m;
            string usdValue = usd.ToString("0.00");

            var access = await GetAccessTokenAsync();
            var client = _http.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", access);

            var body = new
            {
                intent = "CAPTURE",
                purchase_units = new[]
                {
                    new {
                        amount = new {
                            currency_code = "USD",
                            value = usdValue
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(body);
            var req = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = await client.PostAsync($"{_opt.ApiBaseUrl}/v2/checkout/orders", req);
            var text = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
                return (false, null, text);

            var data = JsonDocument.Parse(text);
            string payPalOrderId = data.RootElement.GetProperty("id").GetString()!;

            // Lưu vào DB
            await _txRepo.CreatePendingAsync(orderId, TenCong, payPalOrderId, CancellationToken.None);

            return (true, payPalOrderId, null);
        }

        // ======================================================
        // SMART-BUTTON: Capture PayPal Order
        // ======================================================
        public async Task<bool> CaptureOrderAsync(long orderId, string payPalOrderId)
        {
            var access = await GetAccessTokenAsync();
            var client = _http.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", access);

            var body = new StringContent("{}", Encoding.UTF8, "application/json");

            var resp = await client.PostAsync(
                $"{_opt.ApiBaseUrl}/v2/checkout/orders/{payPalOrderId}/capture",
                body
            );

            var text = await resp.Content.ReadAsStringAsync();
            if (!resp.IsSuccessStatusCode)
                return false;

            await _txRepo.MarkPaidAsync(orderId, TenCong, payPalOrderId, CancellationToken.None);
            await _premiumRepo.ActivateAsync(orderId, CancellationToken.None);

            return true;
        }

        public Task<bool> XuLyWebhookAsync(HttpRequest req, CancellationToken ct)
            => Task.FromResult(false);

        // ======================================================
        private async Task<string> GetAccessTokenAsync()
        {
            var client = _http.CreateClient();
            var auth = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{_opt.ClientId}:{_opt.ClientSecret}")
            );

            var req = new HttpRequestMessage(HttpMethod.Post, $"{_opt.ApiBaseUrl}/v1/oauth2/token");
            req.Headers.Authorization = new AuthenticationHeaderValue("Basic", auth);
            req.Content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("grant_type","client_credentials")
            });

            var resp = await client.SendAsync(req);
            var text = await resp.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(text);

            return json.RootElement.GetProperty("access_token").GetString()!;
        }

        public Task<TaoPhienThanhToanResult> TaoPhienAsync(long donHangId, string returnUrl, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
