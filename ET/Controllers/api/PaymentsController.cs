using Libs.Entity;
using Libs.ThanhToan.Abstractions;
using Microsoft.AspNetCore.Mvc;
using PaymentMethod = Libs.Entity.PhuongThucThanhToan;

namespace ET.Controllers.Api;

[ApiController]
[Route("api/payments")]
public class PaymentsController : ControllerBase
{
    private readonly IThanhToanService _svc;
    public PaymentsController(IThanhToanService svc) => _svc = svc;

    public sealed class CreatePaymentDto
    {
        public long OrderId { get; set; }
        public PaymentMethod Method { get; set; }
        public string ReturnUrl { get; set; } = default!;
    }

    [HttpPost("session")]
    public async Task<IActionResult> CreateSession([FromBody] CreatePaymentDto dto, CancellationToken ct)
        => Ok(await _svc.TaoThanhToanAsync(new(dto.OrderId, dto.Method, dto.ReturnUrl), ct));
}
