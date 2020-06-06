using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Education.Web.Services.HttpServices {
    public class MiscHttpService {
        public HttpClient Client { get; }

        public MiscHttpService(HttpClient client) {
            Client = client;
        }

        public async Task<string[]> GetParsedStringAsync(string toParse) {
            var request = new HttpRequestMessage(HttpMethod.Get, $"data/parsestring/{toParse}");
            var response = await Client.SendAsync(request);

            if (response.IsSuccessStatusCode) {
                // var result = await response.Content.ReadAsStringAsync();
                var resultString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<string[]>(resultString);

                return result;
            } else {
                return Array.Empty<string>();
            }
        }
    }
}
