using Libs.Entity;
using Libs.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ET.Controllers
{
    public class ChuDeController : Controller
    {
        private readonly ChuDeService _chuDeService;
        private readonly LoaiBangLaiService _loaiBangLaiService;

        public ChuDeController(ChuDeService chuDeService, LoaiBangLaiService loaiBangLaiService)
        {
            _chuDeService = chuDeService;
            _loaiBangLaiService = loaiBangLaiService;
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]

        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Details()
        { 
            return View();
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit()
        { 

            return View();
        }

        // phuc vu cho UI nguoi dung
        public async Task<IActionResult> ChonChuDe()
        {         
            return View();
        }

        public async Task<IActionResult> OnTapChuDe(Guid loaiBangLaiId, Guid chuDeId)
        {            
            return View();
        }
    }
}
