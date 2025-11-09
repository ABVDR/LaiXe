using Libs.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ET.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly AdminService adminService;
        public ManagerController(AdminService adminService)
        {
            this.adminService = adminService;
        }
        [HttpGet("role-list")]
        public async Task<IActionResult> GetRoleList()
        {
            var roles = await adminService.GetRoleListAsync();
            return Ok(new
            {
                status = true,
                message = "Get Role List Success",
                data = roles
            });
        }
        [HttpPost("set-role-user/{id}")]
        public async Task<IActionResult> SetRoleUser([FromRoute] Guid id, [FromBody] string Role)
        {
            var result = await adminService.SetRoleUser(id, Role);
            if (result == 1)
            {
                return Ok(new
                {
                    status = true,
                    message = "Set Role User Success",
                });
            }
            else
            {
                return BadRequest(new
                {
                    status = false,
                    message = "Set Role User Failed",
                });
            }
        }
        [HttpGet("get-user-by-id/{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var user = await adminService.GetUserByIdIncludeRolesAsync(id);
            if (user != null)
            {
                return Ok(new
                {
                    status = true,
                    message = "Get User By Id Success",
                    data = user
                });
            }
            else
            {
                return NotFound(new
                {
                    status = false,
                    message = "User Not Found",
                });
            }
        }
        [HttpGet("paged-users")]
        public async Task<IActionResult> GetPagedUser(int page, int pageSize, string? search, string? sortCol, string? sortDir)
        {
            var result = await adminService.GetPagedUser(page, pageSize, search, sortCol, sortDir);
            return Ok(new
            {
                recordsTotal = result.TotalCount,
                recordsFiltered = result.TotalCount,
                data = result.Items
            });
        }

    }
}
