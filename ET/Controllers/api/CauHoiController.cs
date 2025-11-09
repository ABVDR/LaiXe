using Libs.Entity;
using Libs.Repositories;
using Libs.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Libs.CacheService;

namespace ET.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CauHoiController : ControllerBase
    {
        private readonly CauHoiService _cauHoiService;
        private readonly CauHoiCache _cauHoiCache;

        public CauHoiController(CauHoiService cauHoiService, CauHoiCache cauHoiCache)
        {
            this._cauHoiService = cauHoiService;
            _cauHoiCache = cauHoiCache;
        }

        // GET: api/CauHoi/get-all-cau-hoi
        [HttpGet("get-all-cau-hoi")]
        public async Task<IActionResult> GetAllCauHoi()
        {
            var result = await _cauHoiService.GetAllCauHoiAsync();
            if (result == null || !result.Any())
            {
                return Ok(new { status = false, message = "Không có câu hỏi nào" });
            }
            return Ok(new { status = true, message = "Lấy tất cả câu hỏi thành công", data = result });
        }

        // GET: api/CauHoi/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCauHoiById(Guid id)
        {
            var result = await _cauHoiService.GetCauHoiByIdAsync(id);
            if (result == null)
            {
                return Ok(new { status = false, message = "Câu hỏi không tìm thấy" });
            }
            return Ok(new { status = true, message = "Lấy câu hỏi thành công", data = result });
        }

        [HttpGet("paged-cau-hois")]
        public async Task<IActionResult> GetPagedCauHoi(int page, int pageSize, string? search, string? sortCol, string? sortDir)
        {
            var result = await _cauHoiService.GetPagedCauHoi(page, pageSize, search, sortCol, sortDir);
            return Ok(new
            {
                recordsTotal = result.TotalCount,
                recordsFiltered = result.TotalCount,
                data = result.Items
            });
        }
        // POST: api/CauHoi/create
        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateCauHoi([FromBody] CauHoi cauHoi)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { status = false, message = "Dữ liệu không hợp lệ" });
            }

            var result = await _cauHoiService.CreateCauHoiAsync(cauHoi);
            return Ok(new { status = true, message = "Thêm câu hỏi thành công", data = result });
        }

        // PUT: api/CauHoi/update
        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCauHoi([FromBody] CauHoi cauHoi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { status = false, message = "Dữ liệu không hợp lệ" });
            }

            var result = await _cauHoiService.UpdateCauHoiAsync(cauHoi);
            if (result == null)
            {
                return NotFound(new { status = false, message = "Câu hỏi không tìm thấy" });
            }

            return Ok(new { status = true, message = "Cập nhật câu hỏi thành công", data = result });
        }

        // DELETE: api/CauHoi/delete/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCauHoi(Guid id)
        {
            var result = await _cauHoiService.DeleteCauHoiAsync(id);
            if (!result)
            {
                return NotFound(new { status = false, message = "Câu hỏi không tìm thấy" });
            }

            return Ok(new { status = true, message = "Xóa câu hỏi thành công" });
        }
        [HttpGet("test-cache/{id}")]
        public async Task<IActionResult> TestCache(Guid id)
        {
            var cauhoi = await _cauHoiCache.GetCauHoiByIdAsync(id);
            if (cauhoi == null)
            {
                return NotFound(new { status = false, message = "Loại cau hoi không tìm thấy" });
            }
            return Ok(new { status = true, message = "Lấy cau hoi từ cache thành công", data = cauhoi });
        }
    }
}
