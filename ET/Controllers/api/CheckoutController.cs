using Libs.ThanhToan.Abstractions;
using Microsoft.AspNetCore.Mvc;
using PaymentMethod = Libs.Entity.PhuongThucThanhToan;

namespace ET.Controllers.api
{
    [Route("checkout")]
    public class CheckoutController : Controller
    {
        private readonly IThanhToanService _svc;
        public CheckoutController(IThanhToanService svc) => _svc = svc;

        [HttpGet("{orderId:long}")]
        public IActionResult Index(long orderId) => View(model: orderId);

        [HttpPost("pay")]
        public async Task<IActionResult> Pay(long orderId, PaymentMethod method, CancellationToken ct)
        {
            var returnUrl = Url.Action("Result", "Checkout", new { orderId }, Request.Scheme)!;
            var res = await _svc.TaoThanhToanAsync(new(orderId, method, returnUrl), ct);
            if (!res.Ok) return RedirectToAction("Result", new { orderId, error = res.Error });
            return Redirect(res.RedirectUrl!);
        }

        [HttpGet("result")]
        public IActionResult Result(long orderId, string? error) => View(model: (orderId, error));
    }
}
