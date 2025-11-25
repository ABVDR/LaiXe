using Libs.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ET.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AdminController : Controller
    {
        private readonly BaiThiService _baiThiService;
        private readonly LoaiBangLaiService _loaiBangLaiService;
        private readonly ChuDeService _chuDeService;

        public AdminController(
            BaiThiService baiThiService,
            LoaiBangLaiService loaiBangLaiService,
            ChuDeService chuDeService)
        {
            _baiThiService = baiThiService;
            _loaiBangLaiService = loaiBangLaiService;
            _chuDeService = chuDeService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult QuanLyDeThi()
        {
            return View();
        }
        public IActionResult QuanLyUser()
        {
            return View();
        }
        public IActionResult DetailsUser()
        {
            return View();
        }
        public async Task<IActionResult> ChiTietDeThi(Guid id)
        {
            if (id == Guid.Empty)
                return RedirectToAction("QuanLyDeThi");

            var baiThi = await _baiThiService.GetChiTietBaiThi(id);
            if (baiThi == null)
                return NotFound();

            return View(baiThi);
        }

        public IActionResult ThemDeThi()
        {
            // Removed ViewBag usage - data will be fetched via API
            return View();
        }

        public async Task<IActionResult> SuaDeThi(Guid id)
        {
            if (id == Guid.Empty)
                return RedirectToAction("QuanLyDeThi");

            var baiThi = await _baiThiService.GetChiTietBaiThi(id);
            if (baiThi == null)
                return NotFound();

            // Removed ViewBag usage - data will be fetched via API
            return View(baiThi);
        }

        [HttpGet]
        [Route("api/admin/loai-bang-lai")]
        public async Task<IActionResult> GetLoaiBangLai()
        {
            var loaiBangLai = await _loaiBangLaiService.GetDanhSachLoaiBangLaiAsync();
            return Json(loaiBangLai);
        }

        [HttpGet]
        [Route("api/admin/bai-thi/{id}")]
        public async Task<IActionResult> GetBaiThi(Guid id)
        {
            var baiThi = await _baiThiService.GetChiTietBaiThi(id);
            if (baiThi == null)
                return NotFound();

            return Json(baiThi);
        }
        public IActionResult QLMoPhong()
        {
            return View();
        }
        public IActionResult CreateMoPhong()
        {
            return View();
        }

        public IActionResult EditMoPhong(Guid id)
        {
            return View(id);
        }

        public IActionResult DetailsMoPhong(Guid id)
        {
            return View(id);
        }
        public IActionResult QLCauHoi()
        {
            return View();
        }
        public IActionResult CreateCauHoi()
        {
            return View();
        }
        public IActionResult EditCauHoi(Guid id)
        {
            return View(id);
        }
        public IActionResult DetailsCauHoi(Guid id)
        {
            return View(id);
        }
        public IActionResult QLbaocaochiase()
        {
            return View();
        }
        public IActionResult QLLoaiBangLai()
        {
            return View();
        }

        public IActionResult CreateLoaiBangLai()
        {
            return View();
        }

        public IActionResult EditLoaiBangLai(Guid id)
        {
            return View(id);
        }

        public IActionResult DetailsLoaiBangLai(Guid id)
        {
            return View(id);
        }
        public IActionResult QLSaHinh()
        {
            return View();
        }
        public IActionResult CreateSaHinh()
        {
            return View();
        }
        public IActionResult EditSaHinh(Guid id)
        {
            return View(id);
        }
        public IActionResult DetailsSaHinh(Guid id)
        {
            return View(id);
        }
        //moi them 
        public IActionResult DoanhThu()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult DanhSachGiaoDich()
        {
            return View();
        }

        public IActionResult ChiTiet(long id)
        {
            return View(id);
        }


    }
}
