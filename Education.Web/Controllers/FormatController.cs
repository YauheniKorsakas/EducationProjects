using Microsoft.AspNetCore.Mvc;
using Education.Web.Models;
using System.IO;
using System;

namespace Education.Web.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class FormatController : ControllerBase {
        [HttpGet(nameof(GetPerson))]
        //[Produces("application/xml")]
        //[Produces("application/xml")]
        public ActionResult<PersonModel> GetPerson() {
            return Ok(new PersonModel { Name = "default name"});
            // return Ok(new { result = "some string" });
        }

        [HttpGet(nameof(GetHtmlPage))]
        public ActionResult GetHtmlPage() {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "index.html");
            return PhysicalFile(filePath, "application/html");
        }

        [HttpGet(nameof(ThrowError))]
        public ActionResult ThrowError() {
            throw new Exception("Some excepetion");
        }
    }
}