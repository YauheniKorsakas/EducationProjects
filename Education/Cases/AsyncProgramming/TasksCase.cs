using Education.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;

namespace Education.Cases.AsyncProgramming
{
    public class TasksCase : ICase
    {
        public async Task RunAsync() {
            var cts = new CancellationTokenSource();
            Task.Run(async () => {
                await Task.Delay(2000);
                cts.Cancel();
            });
            var task = PerformOperationsAsync(cts.Token);
            try {
                await task;
            } catch (Exception ex) {

            }
        }



        private async Task GetNumbersAsync(CancellationToken token) {
            int counter = 0;

            await Task.Delay(5000, token);
            Console.WriteLine(++counter);
            Console.WriteLine("end of method");
        }

        private Task PerformOperationsAsync(CancellationToken token) {
            int counter = 0;

            return Task.Run(async () => {
                while(true) {
                    await Task.Delay(1000);
                    Console.WriteLine(++counter);
                    token.ThrowIfCancellationRequested();
                }
            }, token);
        }

        private Task RunContinuation() {
            return Task.Run(async () => {
                await Task.Delay(1000);
                throw new Exception("from anc");
            }).ContinueWith(t => {
                if (t.Exception is not null) {
                    Console.WriteLine("Exception was thrown");
                }
                Console.WriteLine(t.Status);
            });
        }

        private Task RunMultipleTasksAsync() {
            var tasksBatch = new List<Task> {
                Task.Run(async () => {
                    await Task.Delay(1000);
                    Console.WriteLine("From first task");
                }),
                Task.Run(async () => {
                    await Task.Delay(2000);
                    Console.WriteLine("From second task");
                }),
                Task.Run(() => { throw new Exception("Third task was failed."); }),
                Task.Run(async () => {
                    await Task.Delay(2000);
                    Console.WriteLine("From fourth task");
                }),
                Task.Run(() => { throw new Exception("Sixth task was failed."); }),
            };

            return Task.WhenAll(tasksBatch);
        }

        private void GetCountOfThreads() {
            var threads = Enumerable
                .Range(1, 1000)
                .Select(item => new Thread(() => { }))
                .ToList();

            Console.WriteLine(threads.Select(item => item.ManagedThreadId).Distinct().Count());
        }

        private async Task InvokeAsyncYealding() {
            IAsyncEnumerable<int> enumerable = AsyncYielding();

            await foreach (var element in enumerable.ConfigureAwait(false)) {
                Console.WriteLine($"element: {element}");
            }
        }

        private async IAsyncEnumerable<int> AsyncYielding() {
            foreach (var item in Enumerable.Range(1, 3)) {
                Task task = Task.Delay(TimeSpan.FromSeconds(item));
                Console.WriteLine($"Task run: {item}");
                await task;
                yield return item;
            }
        }

    }
}
