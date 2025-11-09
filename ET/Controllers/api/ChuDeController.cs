using Libs.CacheService;
using Libs.Entity;
using Libs.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ET.Controllers.api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChuDeController : ControllerBase
    {
        private readonly ChuDeService _chuDeService;
        private readonly LoaiBangLaiService _loaiBangLaiService;
        private readonly ChuDeCache _chuDeCache;


        public ChuDeController(ChuDeService chuDeService, LoaiBangLaiService loaiBangLaiService, ChuDeCache chuDeCache)
        {
            _chuDeService = chuDeService;
            _loaiBangLaiService = loaiBangLaiService;
            _chuDeCache = chuDeCache;
        }

        [HttpGet("danh-sach")]
        public async Task<IActionResult> GetDanhSach()
        {
            var list = await _chuDeService.GetDanhSachChuDe();
            return Ok(list);
        }


        [HttpGet("ten/{chuDeId}")]
        public async Task<IActionResult> GetTenChuDe(Guid chuDeId)
        {
            var ten = await _chuDeService.GetTenChuDeById(chuDeId);
            return Ok(new { tenChuDe = ten });
        }

        [HttpGet("{id}/chu-de")]
        public async Task<IActionResult> GetChuDeByLoaiBangLai(Guid id)
        {
            var (loai, chuDeList) = await _loaiBangLaiService.GetChuDeByLoaiBangLaiAsync(id);

            var result = new
            {
                loai = new
                {
                    loai.Id,
                    loai.TenLoai
                },
                chuDeList = chuDeList.Select(cd => new
                {
                    cd.Id,
                    cd.TenChuDe,
                    cd.ImageUrl,
                    SoCau = cd.CauHois?.Count ?? 0
                })
            };

            return Ok(result);
        }

        // Add this new endpoint that matches the URL called in ThemDeThi.cshtml
        // In ChuDeController.cs if it doesn't already exist
        [HttpGet("by-loai-bang-lai/{loaiBangLaiId}")]
        public async Task<IActionResult> GetChuDeByLoaiBangLaiId(Guid loaiBangLaiId)
        {
            try
            {
                var (loai, chuDeList) = await _loaiBangLaiService.GetChuDeByLoaiBangLaiAsync(loaiBangLaiId);

                if (loai == null)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy loại bằng lái!" });
                }

                var result = chuDeList
                    .Where(cd => !cd.isDeleted)
                    .Select(cd => new
                    {
                        id = cd.Id,
                        tenChuDe = cd.TenChuDe,
                        moTa = cd.MoTa,
                        imageUrl = cd.ImageUrl
                    })
                    .ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Lỗi khi tải danh sách chủ đề.", error = ex.Message });
            }
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _chuDeService.GetAllAsync();
            return Ok(new { success = true, data = list });
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _chuDeService.GetByIdAsync(id);
            if (item == null)
                return NotFound(new { success = false, message = "Không tìm thấy chủ đề" });

            return Ok(new { success = true, data = item });
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ChuDe model)
        {
            await _chuDeService.AddAsync(model);
            return Ok(new { success = true, message = "Tạo mới thành công" });
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] ChuDe model)
        {
            await _chuDeService.UpdateAsync(model);
            return Ok(new { success = true, message = "Cập nhật thành công" });
        }
        [Authorize(Roles = "Admin")]

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _chuDeService.DeleteAsync(id);
            return Ok(new { success = true, message = "Xoá thành công" });
        }
        [Authorize(Roles = "Admin")]

        [HttpPost("toggle-active/{id}")]
        public async Task<IActionResult> ToggleActive(Guid id)
        {
            var item = await _chuDeService.GetByIdAsync(id);
            if (item == null)
                return NotFound(new { success = false });

            item.isDeleted = !item.isDeleted;
            await _chuDeService.UpdateAsync(item);

            return Ok(new { success = true, message = "Cập nhật trạng thái thành công", data = item });
        }

        [HttpPost("create-with-image")]
        public async Task<IActionResult> CreateWithImage([FromForm] ChuDe model, IFormFile? imageUrl)
        {
            if (imageUrl != null && imageUrl.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(imageUrl.FileName);
                var savePath = Path.Combine("wwwroot/images", fileName);
                using var stream = new FileStream(savePath, FileMode.Create);
                await imageUrl.CopyToAsync(stream);
                model.ImageUrl = "/images/" + fileName;
            }

            await _chuDeService.AddAsync(model);
            return Ok(new { success = true, message = "Tạo mới thành công" });
        }


        [HttpPost("edit")]
        public async Task<IActionResult> EditChuDe([FromForm] ChuDe chuDe, [FromForm] IFormFile? imageUrl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ", errors = ModelState });
            }

            var existingChuDe = await _chuDeService.GetByIdAsync(chuDe.Id);
            if (existingChuDe == null)
            {
                return NotFound(new { success = false, message = "Không tìm thấy chủ đề cần chỉnh sửa" });
            }

            // Cập nhật thông tin
            existingChuDe.TenChuDe = chuDe.TenChuDe;
            existingChuDe.MoTa = chuDe.MoTa;

            if (imageUrl != null && imageUrl.Length > 0)
            {
                // Xoá ảnh cũ
                if (!string.IsNullOrEmpty(existingChuDe.ImageUrl))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingChuDe.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Lưu ảnh mới
                existingChuDe.ImageUrl = await SaveImage(imageUrl);
            }

            await _chuDeService.UpdateAsync(existingChuDe);

            return Ok(new { success = true, message = "Cập nhật chủ đề thành công", data = existingChuDe });
        }
        private async Task<string> SaveImage(IFormFile imageFile)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await imageFile.CopyToAsync(stream);

            return "/images/" + uniqueFileName;
        }
        [HttpGet("test-cache/{id}")]
        public async Task<IActionResult> TestCache(Guid id, [FromServices] ChuDeCache chuDeCache)
        {
            var chuDe = await _chuDeCache.GetChuDeByIdAsync(id);
            if (chuDe == null)
                return NotFound(new { success = false, message = "Không tìm thấy chủ đề" });

            return Ok(new
            {
                success = true,
                data = chuDe
            });
        }

    }
}