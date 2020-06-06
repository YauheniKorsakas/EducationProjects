using Education.Web.Services.Interfaces;
using Education.Web.Utils.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Education.Web.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Education.Web.Filters;

namespace Education.Web.Controllers {
    [Route("/api/[controller]")]
    // [ApiController]
    public class DataController : ControllerBase {
        private IDataService _dataService;
        private IConfiguration _configuration;
        private ILogger<LogModel> _logger;

        public DataController(
            IDataService dataService,
            IConfiguration configuration,
            IOptions<UserDataOptions> userDataOptions,
            ILogger<LogModel> logger
        ) {
            _configuration = configuration;
            _logger = logger;

            var name = configuration.GetSection("UserData").GetSection("Name");
            var surname = configuration["UserData:Surname"];
        }

        [HttpGet("params")]
        [ModelValidationFilter]
        public IActionResult Params([Required][FromQuery]int id) {
            return Ok();
        }

        [HttpPost("products")]
        public IActionResult Products([FromBody]ProductModel product) {
            return Ok();
        }

        [HttpPost("products")]
        public IActionResult Products(int id, string name) {
            return Ok();
        }

        [HttpGet("{param}")]
        public string Get(string param) {
            var paramState = param;
            _logger.LogInformation($"{param} has been received.");

            if (param == "admin") {
                _logger.LogWarning("Admin role has been requsted.");
            }

            HttpContext.Response.OnCompleted((obj) => {
                var service = _dataService;
                return Task.CompletedTask;
            }, paramState);
            return $"{param} hanled";
        }

        [HttpGet("error")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult Error() {
            return NotFound();
            //return BadRequest(new SerializableError());
        }

        [HttpPost("getResponseByType")]
        //[Consumes("application/xml")]
        public ActionResult GetResponse([FromBody]BodyDataModel bodyData) {
            return Ok();
        }

        [HttpPost("create-person")]
        public ActionResult CreatePerson([FromBody]PersonModel person) {

            return Ok();
        }
        
        [HttpGet("getString")]
        [HeadersExistFilter("custom-data")]
        public string GetString() {
            var beforeReturn = "before return";
            return "string";
        }

        [HttpGet("get-number")]
        public int GetNumber() {
            return 1;
        }

        [HttpPost("test/{param}")]
        public ActionResult<string> Test(string param, [FromBody] BodyDataModel bodyData) {
            var route = HttpContext.GetRouteData();
            var paramValue = route.Values["param"];
            var query = HttpContext.Request.Query;
            return Ok(param);
        }

        [HttpGet("getByParam/{param}")]
        public IActionResult GetByParam(string param) {
            if (param == "correct") {
                return Ok(param);
            } else {
                return NotFound();
            }
        }
    }
}
