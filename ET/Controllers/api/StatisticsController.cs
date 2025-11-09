using Libs.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ET.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private AdminService adminService;
        public StatisticsController(AdminService adminService)
        {
            this.adminService = adminService;
        }
        [HttpGet("users-per-month")]
        public async Task<IActionResult> GetUserCountPerMonth()
        {
            var data = await adminService.GetUserCountByMonthInCurrentYearAsync();
            return Ok(new
            {
                status = true,
                message = "Thống kê người dùng theo tháng trong năm hiện tại",
                data
            });
        }
        [HttpGet("count-all-user")]
        public async Task<IActionResult> GetAllUserCount()
        {
            var count = await adminService.GetAllUsersCountAsync();
            return Ok(new
            {
                status = true,
                message = "Tổng số người dùng",
                data = count
            });
        }
    }
}
