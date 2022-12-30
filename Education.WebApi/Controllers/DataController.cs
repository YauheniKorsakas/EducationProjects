using Education.WebApi.Models;
using Education.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly IPersonService personService;

        public DataController(IPersonService personService) {
            this.personService = personService ?? throw new ArgumentNullException(nameof(personService));
        }

        //[HttpGet("Description")]
        [HttpGet("Description")]
        public Task<DescriptionViewModel> GetDescription() {
            var session = HttpContext.Session.Id;
            HttpContext.Session.Set("initial", Encoding.Unicode.GetBytes("value for initial key"));
            return Task.FromResult(GetDescription(nameof(GetDescription)));
        }

        [HttpGet("LogPersonInfo")]
        public ActionResult LogPersonInfo() {
            personService.ShowPersonData();
            return Ok();
        }

        private DescriptionViewModel GetDescription(string actionName) {
            return new DescriptionViewModel {
                ActionName = actionName,
                ControllerName = nameof(DataController),
                Version = "1"
            };
        }
    }
}
