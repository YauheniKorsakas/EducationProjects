using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Education.Auth.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        [HttpGet("GetCurrentUser")]
        public string GetCurrentUser() {
            return User.Identity.Name;
        }

        [HttpGet("session")]
        public IActionResult GetSession() {
            var session = HttpContext.Session;

            return Ok();
        }

    }
}
