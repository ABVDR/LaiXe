using Microsoft.AspNetCore.Mvc;
using Libs.Service;
using Libs.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Libs.CacheService;

namespace ET.Controllers.api
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoaiBangLaiController : ControllerBase
    {
        private readonly LoaiBangLaiService _loaiBangLaiService;
        private readonly LoaiBangLaiCache _loaiBangLaiCache;

        public LoaiBangLaiController(LoaiBangLaiService loaiBangLaiService, LoaiBangLaiCache loaiBangLaiCache)
        {
            _loaiBangLaiService = loaiBangLaiService;
            _loaiBangLaiCache = loaiBangLaiCache;
        }

        [HttpGet("danh-sach")]
        public async Task<IActionResult> GetDanhSach()
        {
            var list = await _loaiBangLaiService.GetDanhSachLoaiBangLaiAsync();
            return Ok(list);
        }

        [HttpGet("xe-may")]
        public async Task<IActionResult> GetXeMay()
        {
            var list = await _loaiBangLaiService.GetXeMayAsync();
            return Ok(list);
        }

        [HttpGet("oto")]
        public async Task<IActionResult> GetOTo()
        {
            var list = await _loaiBangLaiService.GetOToAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var loai = await _loaiBangLaiService.GetByIdAsync(id);
            return loai == null ? NotFound() : Ok(loai);
        }

       
        [HttpGet("get-loai-bang-lai-list")]
        public IActionResult GetLoaiBangLaiList()
        {
            List<LoaiBangLai> loaiBangLaiList = _loaiBangLaiService.GetAllLoaiBangLais();
            return Ok(new { status = true, message = "Get Loai Bang Lai Successfully", data = loaiBangLaiList });
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateLoaiBangLai([FromBody] LoaiBangLai loaiBangLai)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { status = false, message = "Dữ liệu không hợp lệ" });
            }

            var result = await _loaiBangLaiService.CreateLoaiBangLaiAsync(loaiBangLai);
            return Ok(new { status = true, message = "Thêm loại bằng lái thành công", data = result });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateLoaiBangLai([FromBody] LoaiBangLai loaiBangLai)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { status = false, message = "Dữ liệu không hợp lệ" });
            }

            var result = await _loaiBangLaiService.UpdateLoaiBangLaiAsync(loaiBangLai);
            if (result == null)
            {
                return NotFound(new { status = false, message = "Loại bằng lái không tìm thấy" });
            }

            return Ok(new { status = true, message = "Cập nhật loại bằng lái thành công", data = result });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteLoaiBangLai(Guid id)
        {
            var result = await _loaiBangLaiService.DeleteLoaiBangLaiAsync(id);
            if (!result)
            {
                return NotFound(new { status = false, message = "Loại bằng lái không tìm thấy" });
            }

            return Ok(new { status = true, message = "Xóa loại bằng lái thành công" });
        }

        [HttpGet("paged-loai-bang-lai")]
        public async Task<IActionResult> GetPagedLoaiBangLai(int page, int pageSize, string? search, string? sortCol, string? sortDir)
        {
            var result = await _loaiBangLaiService.GetPagedLoaiBangLai(page, pageSize, search, sortCol, sortDir);
            return Ok(new
            {
                recordsTotal = result.TotalCount,
                recordsFiltered = result.TotalCount,
                data = result.Items
            });
        }
        [HttpGet("get-loai-bang-lai-has-mo-phong")]
        public async Task<IActionResult> GetLoaiBangLaiHasMoPhong()
        {
            var loaiBangLai = await _loaiBangLaiService.GetLoaiBangLaiHasMoPhong();
            if (loaiBangLai == null)
            {
                return NotFound(new { status = false, message = "Loại bằng lái không tìm thấy" });
            }
            return Ok(new { status = true, message = "Lấy loại bằng lái thành công", data = loaiBangLai });
        }
        [HttpGet("test-cache/{id}")]
        public async Task<IActionResult> TestCache(Guid id)
        {
            var loaiBangLai = await _loaiBangLaiCache.GetLoaiBangLaiByIdAsync(id);
            if (loaiBangLai == null)
            {
                return NotFound(new { status = false, message = "Loại bằng lái không tìm thấy" });
            }
            return Ok(new { status = true, message = "Lấy loại bằng lái từ cache thành công", data = loaiBangLai });
        }
    }
}