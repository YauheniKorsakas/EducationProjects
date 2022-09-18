using Education.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Education.Cases
{
    public class DownloadAndSaveWebPagesCase : ICase
    {
        const string ContentUrlTemplate = "https://lingust.ru/english/grammar/lesson{0}";
        //const int TotalPagesCount = 145;
        const int TotalPagesCount = 10;
        const string Path = @"C:\Books\lingustContents";

        public async Task RunAsync() {
            try {
                var responses = await GetAllPagesResponsesAsync();
                var contents = await GetPagesContentsAsync(responses);
                await SaveToFilesAsync(contents);
            }
            catch (Exception ex) {

            }
        }

        private async Task SaveToFilesAsync(IEnumerable<string> contents) {
            foreach (var item in contents) {
                Console.WriteLine(item);
            }
        }

        private async Task<IEnumerable<string>> GetPagesContentsAsync(IEnumerable<HttpResponseMessage> responseMessages) {
            var taskBatch = responseMessages.Select(response => response.Content.ReadAsStringAsync());
            await Task.WhenAll(taskBatch);

            return taskBatch.Select(s => s.Result);
        }

        private async Task<IEnumerable<HttpResponseMessage>> GetAllPagesResponsesAsync() {
            var taskBatch = Enumerable.Range(1, TotalPagesCount).Select(item => {
                var task = Task.Run<HttpResponseMessage>(async () => {
                    using var httpClient = new HttpClient();
                    var formattedUrl = string.Format(ContentUrlTemplate, item);
                    return await httpClient.GetAsync(formattedUrl);
                });

                return task;
            });

            await Task.WhenAll(taskBatch);
            return taskBatch.Select(s => s.Result);
        }
    }
}
