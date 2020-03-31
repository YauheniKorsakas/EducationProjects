using Education.Core;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Education.Cases {
    public class StreamCase : ICase {
        private const string _assetsPath = @"D:\Repositories\Education\Education\Education\Assets\Notes.txt";

        public async Task RunAsync() {
            Write();
            Console.WriteLine($"End of the {nameof(RunAsync)} method.");
        }

        private async Task CopyFilesAsync() {
            var startDirectory = @"D:\Repositories\Education\Education\Education\Cases";
            var endDirectory = @"D:\Repositories\Education\Education\Education\CasesCopies";
            var fileNames = Directory.EnumerateFiles(startDirectory).ToList();

            if (!Directory.Exists(endDirectory)) {
                Directory.CreateDirectory(endDirectory);
            }

            var allFiles = new DirectoryInfo(startDirectory).GetFiles();

            foreach(var fileName in fileNames) {
                using (var sourceStream = File.Open(fileName, FileMode.Open)) {
                    var newFilePath = endDirectory + fileName.Substring(fileName.LastIndexOf('\\'));
                    using (var destStream = File.Create(newFilePath)) {
                        await sourceStream.CopyToAsync(destStream);
                    }
                }
            }
        }

        private async Task ReadFromFileAsync(string fileName) {
            var resultText = "";

            using (var streamReader = new StreamReader(fileName)) {
                var line = "";

                while ((line = await streamReader.ReadLineAsync()) != null) {
                    Console.WriteLine(line);
                }
            }
        }

        private void Write() {
            var dirInfos = new DirectoryInfo(@"D:\").GetDirectories().ToList();

            using (var streamWriter = new StreamWriter(@"D:\allDirectories.txt")) {
                dirInfos.ForEach(async item => {
                    await streamWriter.WriteLineAsync(item.Name);
                });
            }
        }

        private void PerformAction(Action action) {
            action?.Invoke();
            Console.WriteLine($"End of the {nameof(PerformAction)} invoking.");
        }
    }
}
