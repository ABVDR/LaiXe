using Libs.Repositories;
using Libs.ThanhToan.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

[ApiController]
[Route("api/paypal")]
public class PayPalController : ControllerBase
{
    private readonly IHttpClientFactory _http;
    private readonly PayPalTuyChon _opt;
    private readonly IDonHangRepository _orderRepo;
    private readonly IGiaoDichThanhToanRepository _txRepo;
    private readonly ITinhNangMoKhoaRepository _premiumRepo;

    public PayPalController(
        IHttpClientFactory http,
        IOptions<PayPalTuyChon> opt,
        IDonHangRepository orderRepo,
        IGiaoDichThanhToanRepository txRepo,
        ITinhNangMoKhoaRepository premiumRepo)
    {
        _http = http;
        _opt = opt.Value;
        _orderRepo = orderRepo;
        _txRepo = txRepo;
        _premiumRepo = premiumRepo;
    }

    [HttpPost("create-order")]
    [Authorize]
    public async Task<IActionResult> CreateOrder([FromBody] PayRequest req)
    {
        var order = await _orderRepo.GetAsync(req.OrderId, CancellationToken.None);
        if (order == null)
            return BadRequest(new { status = false, message = "Order not found" });

        decimal usd = order.TongTien / 25000m;

        var token = await GetAccessToken();
        var client = _http.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var body = new
        {
            intent = "CAPTURE",
            purchase_units = new[]
            {
                new {
                    amount = new {
                        currency_code = "USD",
                        value = usd.ToString("0.00")
                    }
                }
            }
        };

        var response = await client.PostAsync(
            $"{_opt.ApiBaseUrl}/v2/checkout/orders",
            new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
        );

        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        if (!response.IsSuccessStatusCode)
            return BadRequest(new { status = false, message = json });

        var payPalOrderId = json.RootElement.GetProperty("id").GetString();

        await _txRepo.CreatePendingAsync(req.OrderId, "PayPal", payPalOrderId, CancellationToken.None);

        return Ok(new { status = true, payPalOrderId });
    }

    [HttpPost("capture-order")]
    [Authorize]
    public async Task<IActionResult> Capture([FromBody] CaptureRequest req)
    {
        var token = await GetAccessToken();
        var client = _http.CreateClient();

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var resp = await client.PostAsync(
            $"{_opt.ApiBaseUrl}/v2/checkout/orders/{req.PayPalOrderId}/capture",
            new StringContent("{}", Encoding.UTF8, "application/json")
        );

        if (!resp.IsSuccessStatusCode)
            return BadRequest(new { status = false });

        await _txRepo.MarkPaidAsync(req.OrderId, "PayPal", req.PayPalOrderId, CancellationToken.None);
        await _premiumRepo.ActivateAsync(req.OrderId, CancellationToken.None);

        return Ok(new { status = true });
    }

    private async Task<string> GetAccessToken()
    {
        var client = _http.CreateClient();

        var auth = Convert.ToBase64String(
            Encoding.UTF8.GetBytes($"{_opt.ClientId}:{_opt.ClientSecret}")
        );

        var req = new HttpRequestMessage(HttpMethod.Post, $"{_opt.ApiBaseUrl}/v1/oauth2/token");
        req.Headers.Authorization = new AuthenticationHeaderValue("Basic", auth);
        req.Content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "grant_type", "client_credentials" }
        });

        var resp = await client.SendAsync(req);
        var json = JsonDocument.Parse(await resp.Content.ReadAsStringAsync());
        return json.RootElement.GetProperty("access_token").GetString();
    }
    public class PayRequest
    {
        public long OrderId { get; set; }
    }

    public class CaptureRequest
    {
        public long OrderId { get; set; }
        public string PayPalOrderId { get; set; }
    }

}
