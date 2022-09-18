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

        private static void GenerateObjects() {
            for (int i = 0; i < 4; i++) {
                var a = 1;
                var b = 2;

                for (int j = 0; j < 5; j++) {

                }
            }
        }

        [HttpGet("session")]
        public IActionResult GetSession() {
            var session = HttpContext.Session;

            return Ok();
        }

    }
}
