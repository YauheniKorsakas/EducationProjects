using Education.Web.Services.HttpServices;
using Education.Web.Utils.Constants;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Education.Web.Controllers {
    [ApiController]
    [Route("{controller}")]
    public class RequestsController : Controller {
        private IHttpClientFactory _httpClientFactory;
        private MiscHttpService _miscHttpService;

        public RequestsController(IHttpClientFactory httpClientFactory, MiscHttpService miscHttpService) {
            _httpClientFactory = httpClientFactory;
            _miscHttpService = miscHttpService;
        }

        [HttpGet("MakeRequest")]
        public async Task<ActionResult> MakeRequest() {
            var toParseString = "some content is here";
            var result = await MakeRequestUsingService(toParseString);

            return Ok();
        }
        
        private async Task<string[]> MakeRequestUsingService(string toParse) => await _miscHttpService.GetParsedStringAsync(toParse);

        private async Task<string[]> MakeRequestUsingClient(string toParse) {
            var request = new HttpRequestMessage(HttpMethod.Get, $"data/parsestring/{toParse}");

            var client = _httpClientFactory.CreateClient(Microservices.Misc);
            var response = await client.SendAsync(request);
            string[] result = null;

            if (response.IsSuccessStatusCode) {
                // var result = await response.Content.ReadAsStringAsync();
                var resultString = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<string[]>(resultString);
            }

            return result;
        }
    }
}
