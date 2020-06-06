using Microsoft.AspNetCore.Mvc;

namespace Education.Web.MiscMicroservice.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase {
        [HttpGet("ParseString/{toParse}")]
        public string[] ParseString(string toParse) {
            return toParse.Split(' ');
        }

        [HttpGet]
        public string Get() {
            return "String from microservice";
        }
    }
}
