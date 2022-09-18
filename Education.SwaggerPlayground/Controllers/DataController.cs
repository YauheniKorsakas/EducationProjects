using Education.SwaggerPlayground.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Education.SwaggerPlayground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        [HttpGet]
        [Route("GetPerson")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PersonViewModel> GetPerson() {
            var result = new PersonViewModel {
                Name = "Zheka",
                Surname = "Kors"
            };

            return Ok(result);
        }

        [HttpGet("GetRequestData")]
        public ActionResult<PersonRequest> GetRequestData([FromQuery] PersonRequest request) {
            return Ok(request);
        }
    }

    public class PersonRequest
    {
        public int Skip { get; set; }
        public int Top { get; set; }
        public int MaxCount { get; set; } = 123;
        public int Age { get; } = 1;
    }
}
