using Libs.Models;
using Libs.Service;
using Microsoft.AspNetCore.Mvc;

[Route("api/doanh-thu")]
[ApiController]
public class DoanhThuController : ControllerBase
{
    private readonly DoanhThuService _service;

    public DoanhThuController(DoanhThuService service)
    {
        _service = service;
    }

    // 1) API thống kê tổng quan
    [HttpGet("thong-ke")]
    public async Task<IActionResult> GetThongKe(
        [FromQuery] DateTime? tuNgay,
        [FromQuery] DateTime? denNgay,
        CancellationToken ct)
    {
        var data = await _service.LayDashboardAsync(tuNgay, denNgay, ct);
        return Ok(new { status = true, data });
    }

    // 2) API danh sách giao dịch (DataTables)
    [HttpGet("giao-dich")]
    public async Task<IActionResult> GetGiaoDich(
        [FromQuery] int page,
        [FromQuery] int pageSize,
        [FromQuery] string? congThanhToan,
        [FromQuery] DateTime? tuNgay,
        [FromQuery] DateTime? denNgay,
        CancellationToken ct)
    {
        var filter = new DoanhThuFilterDto
        {
            Page = page,
            PageSize = pageSize,
            CongThanhToan = congThanhToan,
            TuNgay = tuNgay,
            DenNgay = denNgay
        };

        var data = await _service.LayDanhSachGiaoDichAsync(filter, ct);

        return Ok(new
        {
            status = true,
            items = data.Items,
            total = data.TotalItems
        });

    }

    // 3) API chi tiết giao dịch
    [HttpGet("chi-tiet/{id:long}")]
    public async Task<IActionResult> GetChiTiet(long id, CancellationToken ct)
    {
        var data = await _service.LayChiTietGiaoDichAsync(id, ct);
        if (data == null)
            return NotFound(new { status = false, message = "Không tìm thấy giao dịch" });

        return Ok(new { status = true, data });
    }
}
