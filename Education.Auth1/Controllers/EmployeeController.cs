using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Education.Auth1.Controllers
{
    [Authorize(Policy = "EmployeeOnly")]
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        public IActionResult Index() {
            return View();
        }
    }
}
