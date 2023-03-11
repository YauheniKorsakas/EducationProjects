using Education.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Education.Cases.AsyncProgramming
{
    public class ExceptionsCase : ICase
    {
        public async Task RunAsync() {
            var list = new List<Task<int>> {
                Task.FromResult(1),
                Task.Run(() => {
                    throw new Exception("Invalid number");
                    return 23;
                }),
                Task.FromResult(30)
            };

            while (list.Any()) {
                var current = await Task.WhenAny(list);
                list.Remove(current);
                try {
                    Console.WriteLine(await current);
                } catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                    throw;
                } 
            }

        }

        private async Task HandleNumberAsync(int number, bool shouldThrowException = false) {
            Console.WriteLine($"{number} started handling...");
            if (shouldThrowException) {
                throw new InvalidOperationException($"Cannot process {number}");
            }
            await Task.Delay(1000 * number);
            Console.WriteLine($"{number} has been processed!");
        }

        static async IAsyncEnumerable<string> ReadWordsFromStreamAsync() {
            string data =
                @"This is a line of text.
              Here is the second line of text.
              And there is one more for good measure.
              Wait, that was the penultimate line.";

            using var readStream = new StringReader(data);

            string? line = await readStream.ReadLineAsync();
            while (line != null) {
                foreach (string word in line.Split(' ', StringSplitOptions.RemoveEmptyEntries)) {
                    yield return word;
                }

                line = await readStream.ReadLineAsync();
            }
        }

    }
}
