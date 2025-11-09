using Libs.Service;
using Microsoft.AspNetCore.Mvc;

namespace ET.Controllers
{
    public class LoaiBangLaiController : Controller
    {
        private readonly LoaiBangLaiService _loaiBangLaiService;

        public LoaiBangLaiController(LoaiBangLaiService loaiBangLaiService)
        {
            _loaiBangLaiService = loaiBangLaiService;
        }

        // View này có thể dùng model nếu bạn muốn truyền danh sách từ server
        public async Task<IActionResult> DanhSach()
        {
            var list = await _loaiBangLaiService.GetDanhSachLoaiBangLaiAsync();
            return View(list); // Tương ứng với Views/LoaiBangLai/DanhSach.cshtml
        }

        // View này fetch dữ liệu từ API => không cần truyền model
        public IActionResult XeMay()
        {
            return View(); // Views/LoaiBangLai/XeMay.cshtml
        }

        // View này fetch dữ liệu từ API => không cần truyền model
        public IActionResult OTo()
        {
            return View(); // Views/LoaiBangLai/OTo.cshtml (nếu bạn tạo riêng cho ô tô)
        }
    }
}
