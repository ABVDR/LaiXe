using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ET.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAdmin()
        {
            return Ok(new { Message = "This is a protected API endpoint for Admin" });
        }

        [HttpGet("user")]
        [Authorize(Roles = "User")]
        public IActionResult GetUser()
        {
            return Ok(new { Message = "This is a protected API endpoint for User" });
        }
    }
}
