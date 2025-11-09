using Libs.CacheService;
using Libs.Entity;
using Libs.Models;
using Libs.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ET.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaiThiController : ControllerBase
    {
        private readonly BaiThiService _baiThiService;
        private readonly ChuDeService _chuDeService;
        private readonly LoaiBangLaiService _loaiBangLaiService;
        private readonly LichSuThiService _lichSuThiService;
        private readonly BaiThiCache _baiThiCache;
        public BaiThiController(BaiThiService baiThiService, ChuDeService chuDeService, LoaiBangLaiService loaiBangLaiService, LichSuThiService lichSuThiService, BaiThiCache baiThiCache)
        {
            _baiThiService = baiThiService;
            _chuDeService = chuDeService;
            _loaiBangLaiService = loaiBangLaiService;
            _lichSuThiService = lichSuThiService;
            _baiThiCache = baiThiCache;
        }

        [HttpPost("nop-bai-thi")]
        public async Task<IActionResult> NopBaiThi([FromBody] SubmitBaiThiRequest request)
        {
            if (request == null || request.BaiThiId == Guid.Empty || request.Answers == null)
            {
                return BadRequest(new { success = false, message = "Dữ liệu gửi lên không hợp lệ!" });
            }

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _baiThiService.XuLyNopBaiThi(request, userId);

                if (!result.Success)
                {
                    return BadRequest(new { success = false, message = result.Message });
                }

                return Ok(new
                {
                    success = true,
                    baiThiId = result.BaiThiId,
                    ketQuaList = result.KetQuaList.Select(kq => new
                    {
                        kq.CauHoiId,
                        kq.CauTraLoi,
                        kq.DapAnDung,
                        kq.DungSai
                    }),
                    soCauDung = result.SoCauDung,
                    tongSoCau = result.TongSoCau,
                    tongDiem = result.TongDiem,
                    ketQua = result.KetQua,
                    macLoiNghiemTrong = result.MacLoiNghiemTrong,
                    soCauLoiNghiemTrong = result.SoCauLoiNghiemTrong
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Lỗi khi xử lý bài thi.", error = ex.Message });
            }
        }

        [HttpGet("bai-thi/{id}")]
        public async Task<IActionResult> GetBaiThi(Guid id)
        {
            //var baiThi = await _baiThiService.GetBaiThiWithDetails(id); //cu
            var baiThi = await _baiThiCache.GetBaiThiByIdAsync(id);
            if (baiThi == null)
                return NotFound(new { success = false, message = "Không tìm thấy bài thi!" });

            var result = new
            {
                baiThi.Id,
                baiThi.TenBaiThi,
                ChiTietBaiThis = baiThi.ChiTietBaiThis.Select(ct => new
                {
                    ct.Id,
                    CauHoi = new
                    {
                        ct.CauHoi.Id,
                        ct.CauHoi.NoiDung,
                        ct.CauHoi.LuaChonA,
                        ct.CauHoi.LuaChonB,
                        ct.CauHoi.LuaChonC,
                        ct.CauHoi.LuaChonD,
                        ct.CauHoi.DapAnDung,
                        ct.CauHoi.MediaUrl,
                        LoaiBangLai = new
                        {
                            ct.CauHoi.LoaiBangLai.Id,
                            ct.CauHoi.LoaiBangLai.TenLoai,
                            ct.CauHoi.LoaiBangLai.ThoiGianThi,
                            ct.CauHoi.LoaiBangLai.DiemToiThieu
                        }
                    }
                }).ToList()
            };

            return Ok(result);
        }


        [HttpGet("danh-sach-bai-thi")]
        public async Task<IActionResult> GetDanhSachBaiThi()
        {
            var list = await _baiThiService.GetDanhSachBaiThi();
            return Ok(list);
        }

        [HttpGet("de-thi-ngau-nhien/{loaiBangLaiId}")]
        public async Task<IActionResult> GetDeThiNgauNhien(Guid loaiBangLaiId)
        {
            var deThi = await _baiThiService.GetDeThiNgauNhien(loaiBangLaiId);
            if (deThi == null)
                return NotFound(new { success = false, message = "Không tìm thấy đề thi phù hợp!" });

            return Ok(deThi);
        }

        [HttpGet("danh-sach-de-thi")]
        public async Task<IActionResult> GetDanhSachDeThi([FromQuery] string loaiXe = null)
        {
            try
            {
                var list = await _baiThiService.GetDanhSachDeThi(loaiXe);

                // Chuyển đổi dữ liệu để tránh tham chiếu vòng tròn
                var result = list.Select(bt => new {
                    bt.Id,
                    bt.TenBaiThi,
                    SoCau = bt.ChiTietBaiThis?.Count ?? 0,
                    LoaiBangLai = bt.ChiTietBaiThis?.FirstOrDefault()?.CauHoi?.LoaiBangLai?.TenLoai ?? "Không xác định"
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi tải danh sách đề thi", error = ex.Message });
            }
        }
        [HttpGet("on-tap/{loaiBangLaiId}")]
        public async Task<IActionResult> GetCauHoiOnTap(Guid loaiBangLaiId)
        {
            try
            {
                var list = await _baiThiService.GetCauHoiOnTap(loaiBangLaiId);
                if (list == null)
                    return Ok(new List<CauHoi>());

                // Loại bỏ tham chiếu vòng
                var result = list.Select(c => new {
                    c.Id,
                    c.NoiDung,
                    c.LuaChonA,
                    c.LuaChonB,
                    c.LuaChonC,
                    c.LuaChonD,
                    c.DapAnDung,
                    c.DiemLiet,
                    c.MediaUrl,
                    c.GiaiThich,
                    c.MeoGhiNho,
                    ChuDe = c.ChuDe != null ? new { c.ChuDe.Id, c.ChuDe.TenChuDe } : null
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine($"Error in GetCauHoiOnTap: {ex.Message}");
                return StatusCode(500, new { success = false, message = "Lỗi khi lấy câu hỏi", error = ex.Message });
            }
        }

        public LichSuThiService Get_lichSuThiService()
        {
            return _lichSuThiService;
        }

        [HttpGet("lich-su-thi")]
        [Authorize]
        public async Task<IActionResult> GetLichSuThi()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { success = false, message = "Không tìm thấy thông tin người dùng!" });
            }

            // Provide default values for pageNumber and pageSize when calling GetLichSuThiByUser
            var lichSuThis = await _lichSuThiService.GetLichSuThiByUser(userId, 1, 10);
            return Ok(lichSuThis);
        }

        [HttpGet("lich-su-thi/{id}")]
        [Authorize]
        public async Task<IActionResult> GetChiTietLichSuThi(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { success = false, message = "Không tìm thấy thông tin người dùng!" });
            }

            var chiTiet = await _lichSuThiService.GetLichSuThiDetail(id);
            if (chiTiet == null)
                return NotFound(new { success = false, message = "Không tìm thấy lịch sử thi!" });
            return Ok(new { success = true, data = chiTiet });
        }

        [HttpGet("cau-hoi")]
        public async Task<IActionResult> GetCauHoiTheoChuDe(Guid loaiBangLaiId, Guid chuDeId)
        {
            try
            {
                if (chuDeId == Guid.Empty)
                    return BadRequest(new { success = false, message = "chuDeId không hợp lệ!" });

                var list = await _chuDeService.GetCauHoiTheoChuDe(loaiBangLaiId, chuDeId);
                if (list == null)
                    return Ok(new List<CauHoi>());

                // Loại bỏ tham chiếu vòng để tránh lỗi serialization
                var result = list.Select(c => new {
                    c.Id,
                    c.NoiDung,
                    c.LuaChonA,
                    c.LuaChonB,
                    c.LuaChonC,
                    c.LuaChonD,
                    c.DapAnDung,
                    c.DiemLiet,
                    c.MediaUrl,
                    c.GiaiThich,
                    c.MeoGhiNho,
                    ChuDe = c.ChuDe != null ? new { c.ChuDe.Id, c.ChuDe.TenChuDe } : null
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine($"Error in GetCauHoiTheoChuDe: {ex.Message}");
                return StatusCode(500, new { success = false, message = "Lỗi khi lấy câu hỏi", error = ex.Message });
            }
        }

        [HttpGet("cau-hoi-tao-de-thi")]
        public async Task<IActionResult> GetCauHoiTaoDeThiAsync([FromQuery] Guid loaiBangLaiId, [FromQuery] Guid? chuDeId = null)
        {
            if (loaiBangLaiId == Guid.Empty)
                return BadRequest("ID loại bằng lái không hợp lệ!");

            List<CauHoi> cauHois;
            if (chuDeId.HasValue && chuDeId != Guid.Empty)
                cauHois = await _chuDeService.GetCauHoiTheoChuDe(loaiBangLaiId, chuDeId.Value);
            else
                cauHois = await _baiThiService.GetCauHoiTaoDeThiAsync(loaiBangLaiId);

            return Ok(cauHois ?? new List<CauHoi>()); // Không bao giờ trả về null
        }

        [HttpGet("de-thi/{loaiBangLaiId}")]
        public async Task<IActionResult> GetDeThiByLoaiBangLai(Guid loaiBangLaiId)
        {
            var loaiBangLai = await _loaiBangLaiService.GetByIdAsync(loaiBangLaiId);
            if (loaiBangLai == null)
                return NotFound("Không tìm thấy loại bằng lái.");

            var danhSachDeThi = await _baiThiService.GetDeThiByLoaiBangLai(loaiBangLaiId);

            var result = new
            {
                loai = new
                {
                    loaiBangLai.Id,
                    loaiBangLai.TenLoai
                },
                danhSachDeThi = danhSachDeThi.Select(bt => new
                {
                    bt.Id,
                    bt.TenBaiThi,
                    SoCau = bt.ChiTietBaiThis?.Count ?? 0,                  
                })
            };

            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetChiTietBaiThi(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new { success = false, message = "ID không hợp lệ!" });
            }

            var baiThi = await _baiThiService.GetChiTietBaiThi(id);

            if (baiThi == null)
            {
                return NotFound(new { success = false, message = "Không tìm thấy bài thi." });
            }

            return Ok(new { success = true, data = baiThi });
        }
        [HttpGet("de-thi-ngau-nhien-theo-chu-de/{loaiBangLaiId}/{chuDeId}")]
        public async Task<IActionResult> GetDeThiNgauNhienTheoChuDe(Guid loaiBangLaiId, Guid chuDeId)
        {
            try
            {
                // Verify that both the license type and subject exist
                var loaiBangLai = await _loaiBangLaiService.GetByIdAsync(loaiBangLaiId);
                if (loaiBangLai == null)
                    return NotFound(new { success = false, message = "Không tìm thấy loại bằng lái!" });

                var tenChuDe = await _chuDeService.GetTenChuDeById(chuDeId);
                if (string.IsNullOrEmpty(tenChuDe) || tenChuDe == "Không rõ chủ đề")
                    return NotFound(new { success = false, message = "Không tìm thấy chủ đề!" });

                // Get questions for this license type and subject
                var cauHois = await _chuDeService.GetCauHoiTheoChuDe(loaiBangLaiId, chuDeId);
                if (cauHois == null || !cauHois.Any())
                    return NotFound(new { success = false, message = "Không có câu hỏi phù hợp cho loại bằng lái và chủ đề này!" });

                // First I need to check if we have enough questions
                if (cauHois.Count < 5)
                    return BadRequest(new { success = false, message = "Không đủ câu hỏi để tạo đề thi (cần ít nhất 5 câu hỏi)!" });

                // Create a new random exam using the BaiThiService
                var deThi = await _baiThiService.CreateDeThiNgauNhienTheoChuDe(loaiBangLaiId, chuDeId, cauHois);
                if (deThi == null)
                    return StatusCode(500, new { success = false, message = "Không thể tạo đề thi ngẫu nhiên!" });

                return Ok(deThi);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Lỗi khi xoá đề thi.", error = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBaiThi(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest(new { success = false, message = "ID không hợp lệ!" });
                }

                var baiThi = await _baiThiService.GetById(id);
                if (baiThi == null)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy bài thi." });
                }

                await _baiThiService.Delete(baiThi);
                return Ok(new { success = true, message = "Xoá đề thi thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Lỗi khi xoá đề thi.", error = ex.Message });
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateBaiThi([FromBody] CreateBaiThiRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.TenBaiThi) ||
                request.ChiTietBaiThis == null || !request.ChiTietBaiThis.Any())
            {
                return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ!" });
            }

            try
            {
                // Generate new ID for BaiThi
                var baiThiId = Guid.NewGuid();

                // Create BaiThi object
                var baiThi = new BaiThi
                {
                    Id = baiThiId,
                    TenBaiThi = request.TenBaiThi,
                    ChiTietBaiThis = request.ChiTietBaiThis.Select(ct => new ChiTietBaiThi
                    {
                        Id = Guid.NewGuid(),
                        BaiThiId = baiThiId, // Very important - set BaiThiId explicitly
                        CauHoiId = ct.CauHoiId
                    }).ToList()
                };

                // Log the data we're trying to save
                Console.WriteLine($"Saving BaiThi: {baiThiId}, Title: {request.TenBaiThi}, Questions: {request.ChiTietBaiThis.Count}");

                // Save to database
                await _baiThiService.AddAsync(baiThi);

                // Return success
                return Ok(new { success = true, id = baiThi.Id });
            }
            catch (ApplicationException appEx)
            {
                Console.WriteLine($"Application error in CreateBaiThi: {appEx.Message}");
                return StatusCode(500, new { success = false, message = appEx.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in CreateBaiThi: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return StatusCode(500, new { success = false, message = "Lỗi khi lưu đề thi: " + ex.Message });
            }
        }
        [HttpGet("load-bang-lai-va-chu-de")]
        public async Task<IActionResult> LoadBangLaiVaChuDe()
        {
            try
            {
                // Get all license types
                var loaiBangLais = await _loaiBangLaiService.GetDanhSachLoaiBangLaiAsync();

                // Prepare the result with license types and their topics
                var result = new List<object>();

                foreach (var loaiBangLai in loaiBangLais)
                {
                    var (bangLai, chuDes) = await _loaiBangLaiService.GetChuDeByLoaiBangLaiAsync(loaiBangLai.Id);

                    result.Add(new
                    {
                        bangLai = new
                        {
                            bangLai.Id,
                            bangLai.TenLoai,
                            bangLai.ThoiGianThi,
                            bangLai.DiemToiThieu
                        },
                        chuDes = chuDes.Where(cd => !cd.isDeleted).Select(cd => new
                        {
                            cd.Id,
                            cd.TenChuDe,
                            cd.MoTa,
                            cd.ImageUrl
                        }).ToList()
                    });
                }

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Lỗi khi tải dữ liệu bằng lái và chủ đề.", error = ex.Message });
            }
        }
    }
}