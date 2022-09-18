using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

namespace Education.Web.CoreWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("GetData")]
        public ActionResult<object> GetData([FromQuery] QueryViewModel model) {
            return Ok(model);
        }

        [HttpGet]
        public async Task<ActionResult<object>> Person() {
            var result = await GetPersonAsync();

            var serializerResult = JsonSerializer.Serialize(result);

            return Ok(result);
        }

        private Task<object> GetPersonAsync() {
            var result = new {
                name = "Yauheni",
                surname = "Korsakas",
                age = "20"
            };

            return Task.FromResult((object)result);
        }
    }

    public class QueryViewModel
    {
        public int Take { get; set; } = 10;
        public int Top { get; set; } = 50;
    }
}
