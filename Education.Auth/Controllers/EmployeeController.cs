using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Education.Auth.Controllers
{
    [Authorize(Policy = "EmployeeOnly")]
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : Controller
    {
        [HttpGet("index")]
        public IActionResult Index() {
            return Ok("Employee check out.");
        }

        [AllowAnonymous]
        [HttpGet("get-default-data")]
        public string GetDefaultData() {
            return "default data for empl";
        }

    }
}
