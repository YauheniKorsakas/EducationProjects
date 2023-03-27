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
            //await InvokeStreamAsync();
            await InvokeMultipleChildsWithExceptionsAsync();
        }

        private async Task InvokeWhenAllWithExceptionsAsync() {
            string path = @"";
            List<Task> tasks = new();

            tasks.Add(Task.Run(() =>
            {
                // This should throw an UnauthorizedAccessException.
                return Directory.GetFiles(
                    path, "*.txt",
                    SearchOption.AllDirectories);
            }));

            tasks.Add(Task.Run(() =>
            {
                if (path == @"C:\") {
                    throw new ArgumentException(
                        "The system root is not a valid path.");
                }
                return new string[] { ".txt", ".dll", ".exe", ".bin", ".dat" };
            }));

            tasks.Add(Task.Run(() =>
            {
                throw new NotImplementedException(
                    "This operation has not been implemented.");
            }));

            try {
                await Task.WhenAll(tasks.ToArray());
            }
            catch (AggregateException ae) {
                throw ae.Flatten();
            }
        }

        private async Task InvokeMultipleChildsWithExceptionsAsync() {
            var task = Task.Factory.StartNew(() => {
                var child = Task.Factory.StartNew(() => {
                    var grandChild = Task.Factory.StartNew(() => {
                        // This exception is nested inside three AggregateExceptions.
                        throw new Exception("Attached child2 faulted.");
                    }, TaskCreationOptions.AttachedToParent);

                    // This exception is nested inside two AggregateExceptions.
                    throw new ArgumentNullException("Attached child1 faulted.");
                }, TaskCreationOptions.AttachedToParent);

                var child1 = Task.Factory.StartNew(() => {
                    throw new ArgumentNullException("From child1");
                }, TaskCreationOptions.AttachedToParent);
            });

            try {
                await task;
                
            }
            catch (Exception ex) {

            }
        }

        private async Task InvokeCancelledTaskAsync() {
            using var cts = new CancellationTokenSource();
            cts.Cancel();
            await Task.Run(() => { }, cts.Token);
        }

        private async Task HandleNumberAsync(int number, bool shouldThrowException = false) {
            Console.WriteLine($"{number} started handling...");
            if (shouldThrowException) {
                throw new InvalidOperationException($"Cannot process {number}");
            }
            await Task.Delay(1000 * number);
            Console.WriteLine($"{number} has been processed!");
        }

        private async Task InvokeStreamAsync() {
            await foreach (var word in ReadWordsFromStreamAsync()) {
                Console.Write(word + " ");
            }
        }

        private async IAsyncEnumerable<string> ReadWordsFromStreamAsync() {
            string data =
                @"This is a line of text.
              Here is the second line of text.
              And there is one more for good measure.
              Wait, that was the penultimate line.";

            using var readStream = new StringReader(data);

            string? line = await readStream.ReadLineAsync();
            while (line != null) {
                foreach (string word in line.Split(' ', StringSplitOptions.RemoveEmptyEntries)) {
                    await Task.Delay(200);
                    yield return word;
                }

                line = await readStream.ReadLineAsync();
            }
        }

    }
}
