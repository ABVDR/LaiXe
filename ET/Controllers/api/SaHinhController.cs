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
    public class SaHinhController : ControllerBase
    {
        private readonly SaHinhService _saHinhService;
        private readonly SaHinhCache _saHinhCache;
        public SaHinhController(SaHinhService saHinhService, SaHinhCache saHinhCache)
        {
            this._saHinhService = saHinhService;
            _saHinhCache = saHinhCache;
        }
        [HttpGet("get-all-bai-sa-hinh")]
        public async Task<IActionResult> GetAllBaiSaHinh()
        {
            var result = await _saHinhService.GetAllBaiSaHinhAsync();
            if (result == null || !result.Any())
            {
                return Ok(new { status = false, message = "Không có bài sa hình nào" });
            }
            return Ok(new { status = true, message = "Lấy tất cả bài sa hình thành công", data = result });
        }
        // GET: api/SaHinh/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBaiSaHinhById(Guid id)
        {
            var result = await _saHinhService.GetBaiSaHinhByIdAsync(id);
            if (result == null)
            {
                return Ok(new { status = false, message = "Bài sa hình không tìm thấy" });
            }
            return Ok(new { status = true, message = "Lấy bài sa hình thành công", data = result });
        }
        [HttpGet("paged-bai-sa-hinh")]
        public async Task<IActionResult> GetPagedBaiSaHinh(int page, int pageSize, string? search, string? sortCol, string? sortDir)
        {
            var result = await _saHinhService.GetPagedBaiSaHinh(page, pageSize, search, sortCol, sortDir);
            return Ok(new
            {
                recordsTotal = result.TotalCount,
                recordsFiltered = result.TotalCount,
                data = result.Items
            });
        }
        // POST: api/SaHinh/create
        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateBaiSaHinh([FromBody] BaiSaHinh baiSaHinh)
        {
            if (baiSaHinh == null)
            {
                return BadRequest(new { status = false, message = "Dữ liệu bài sa hình không hợp lệ" });
            }
            var createdBaiSaHinh = await _saHinhService.CreateBaiSaHinhAsync(baiSaHinh);
            return Ok(new { status = true, message = "Tạo bài sa hình thành công", data = createdBaiSaHinh });
        }
        // PUT: api/SaHinh/update
        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateBaiSaHinh([FromBody] BaiSaHinh baiSaHinh)
        {
            if (baiSaHinh == null || baiSaHinh.Id == Guid.Empty)
            {
                return BadRequest(new { status = false, message = "Dữ liệu bài sa hình không hợp lệ" });
            }
            var updatedBaiSaHinh = await _saHinhService.UpdateBaiSaHinhAsync(baiSaHinh);
            if (updatedBaiSaHinh == null)
            {
                return NotFound(new { status = false, message = "Bài sa hình không tìm thấy" });
            }
            return Ok(new { status = true, message = "Cập nhật bài sa hình thành công", data = updatedBaiSaHinh });
        }
        // DELETE: api/SaHinh/delete/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBaiSaHinh(Guid id)
        {
            var baiSaHinh = await _saHinhService.DeleteBaiSaHinhAsync(id);
            if (!baiSaHinh)
            {
                return NotFound(new { status = false, message = "Bài sa hình không tìm thấy" });
            }
            return Ok(new { status = true, message = "Xóa bài sa hình thành công" });

        }
        [HttpGet("test-cache/{id}")]
        public async Task<IActionResult> TestCache(Guid id)
        {
            var sahinh = await _saHinhCache.GetSaHinhByIdAsync(id);
            if (sahinh == null)
            {
                return NotFound(new { status = false, message = "Loại mo phong không tìm thấy" });
            }
            return Ok(new { status = true, message = "Lấy mo phong từ cache thành công", data = sahinh });
        }

    }
}
