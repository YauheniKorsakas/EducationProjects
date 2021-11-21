using Education.Auth.Domain;
using Education.Auth.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Education.Auth.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private static int RequestsCount = 0;
        private UserContext dbContext;
        //public AuthController(IHttpContextAccessor httpContextAccessor) { // Allows to get access to current HttpContext in any part of the app.
        //}

        public AuthController(UserContext dbContext) {
            this.dbContext = dbContext;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel) {
            var existingUser = dbContext.Users.FirstOrDefault(user => user.Username == registerModel.Username);

            if (existingUser == null) {
                var newUser = new User { Username = registerModel.Username, Password = registerModel.Password };
                dbContext.Users.Add(newUser);
                await dbContext.SaveChangesAsync();
                await Authenticate(registerModel.Username);

                return Ok("Successfully logged in.");
            }

            return BadRequest("User already exists.");
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login) {
            var existingUser = dbContext.Users.FirstOrDefault(user => user.Username == login.Username && user.Password == login.Password);

            if (existingUser != null) {
                await Authenticate(login.Username);

                return Ok("Successfully logged in");
            }

            return BadRequest("Invalid login data.");
        }

        [HttpGet]
        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok("Signed out");
        }

        private async Task Authenticate(string username) {
            var claims = new List<Claim> {
                new Claim(ClaimsIdentity.DefaultNameClaimType, username)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultRoleClaimType, ClaimsIdentity.DefaultNameClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity)); // ClaimsPrincipal is the CurrentUser.
        }
        
    }
}
