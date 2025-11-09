using ET.Models;
using Libs.Entity;
using Libs.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Libs.CacheService;

namespace ET.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoPhongController : ControllerBase
    {
        private MoPhongService moPhongService;
        private readonly MoPhongcache _moPhongCache;
        public MoPhongController(MoPhongService moPhongService, MoPhongcache moPhongcache)
        {
            this.moPhongService = moPhongService;
            _moPhongCache = moPhongcache;
        }

        [HttpGet("get-mo-phong-by-loai-bang-lai")]
        public async Task<IActionResult> GetMoPhong(Guid id)
        {
            var result = await moPhongService.GetMoPhongByLoaiBangLaiIdAsync(id);

            if (result == null || !result.Any())
            {
                return Ok(new { status = false, message = "Mô phỏng không tìm thấy" });
            }
            return Ok(new { status = true, message = "Lấy mô phỏng thành công", data = result });
        }

        [HttpGet("get-all-mo-phong")]
        public async Task<IActionResult> GetAllMoPhong()
        {
            var result = await moPhongService.GetAllMoPhongAsync();
            if (result == null || !result.Any())
            {
                return Ok(new { status = false, message = "Không có mô phỏng nào" });
            }
            return Ok(new { status = true, message = "Lấy tất cả mô phỏng thành công", data = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMoPhongById(Guid id)
        {
            var result = await moPhongService.GetMoPhongByIdAsync(id);
            if (result == null)
            {
                return Ok(new { status = false, message = "Mô phỏng không tìm thấy" });
            }
            return Ok(new { status = true, message = "Lấy mô phỏng thành công", data = result });
        }
        [HttpGet("paged-mo-phongs")]
        public async Task<IActionResult> GetPagedMoPhong(int page, int pageSize, string? search, string? sortCol, string? sortDir)
        {
            var result = await moPhongService.GetPagedMoPhong(page, pageSize, search, sortCol, sortDir);
            return Ok(new
            {
                recordsTotal = result.TotalCount,
                recordsFiltered = result.TotalCount,
                data = result.Items
            });
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateMoPhong(MoPhong moPhong)
        {
            var mo = moPhong;
            if (!ModelState.IsValid)
            {
                return Ok(new { status = false, message = "Dữ liệu không hợp lệ" });
            }

            var result = await moPhongService.CreateMoPhongAsync(moPhong);
            return Ok(new { status = true, message = "Thêm mô phỏng thành công", data = result });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateMoPhong(MoPhong moPhong)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { status = false, message = "Dữ liệu không hợp lệ" });
            }

            var result = await moPhongService.UpdateMoPhongAsync(moPhong);
            if (result == null)
            {
                return NotFound(new { status = false, message = "Mô phỏng không tìm thấy" });
            }

            return Ok(new { status = true, message = "Cập nhật mô phỏng thành công", data = result });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteMoPhong(Guid id)
        {
            var result = await moPhongService.DeleteMoPhongAsync(id);
            if (!result)
            {
                return NotFound(new { status = false, message = "Mô phỏng không tìm thấy" });
            }

            return Ok(new { status = true, message = "Xóa mô phỏng thành công" });
        }
        [HttpGet("test-cache/{id}")]
        public async Task<IActionResult> TestCache(Guid id)
        {
            var mophong = await _moPhongCache.GetMoPhongByIdAsync(id);
            if (mophong == null)
            {
                return NotFound(new { status = false, message = "Loại mo phong không tìm thấy" });
            }
            return Ok(new { status = true, message = "Lấy mo phong từ cache thành công", data = mophong });
        }
    }
}
