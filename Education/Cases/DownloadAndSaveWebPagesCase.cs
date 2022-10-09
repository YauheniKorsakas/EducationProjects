using Education.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Education.Cases
{
    public class DownloadAndSaveWebPagesCase : ICase
    {
        private string ContentUrlTemplate => "https://lingust.ru/english/grammar/lesson{0}";
        private const int TotalPagesCount = 145;
        private const string ContentPath = @"C:\Books\lingustContents";

        public async Task RunAsync() {
            try {
                var responses = await GetAllPagesResponsesAsync();
                var contents = await GetPagesContentsAsync(responses);
                await SaveToFilesAsync(contents);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task SaveToFilesAsync(IEnumerable<PageContent> contents) {
            if (Directory.Exists(ContentPath)) {
                var fileNames = Directory.GetFiles(ContentPath);
                fileNames.ToList().ForEach(item => File.Delete(item));
                Directory.Delete(ContentPath);
            }
            Directory.CreateDirectory(ContentPath);

            foreach (var item in contents) {
                var fileName = $"lesson{item.Id}.html";
                var filePath = Path.Combine(ContentPath, fileName);
                using var fileStream = File.Create(filePath);
                await WriteIntoFileAsync(fileStream, item.Content);
            }
        }

        private async Task WriteIntoFileAsync(FileStream fileStream, string content) {
            var bytes = Encoding.Default.GetBytes(content);
            await fileStream.WriteAsync(bytes, 0, bytes.Length);
        }

        private async Task<IEnumerable<PageContent>> GetPagesContentsAsync(IEnumerable<HttpResponseMessage> responseMessages) {
            var taskBatch = responseMessages.Select(response => response.Content.ReadAsStringAsync());
            await Task.WhenAll(taskBatch);

            var index = 0;
            return taskBatch.Select(s => new PageContent { Id = ++index, Content = s.Result });
        }

        private async Task<IEnumerable<HttpResponseMessage>> GetAllPagesResponsesAsync() {
            var range = Enumerable.Range(1, TotalPagesCount);
            var taskBatch = range.Select(item => {
                var task = Task.Run(async () => {
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

    public class PageContent
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }
}
