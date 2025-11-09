using Microsoft.AspNetCore.Mvc;

namespace ET.Controllers
{
    public class ShareController : Controller
    {
        public IActionResult Share()
        {
            return View();
        }
        public IActionResult Sharereport()
        {
            return View();
        }
    }
}
