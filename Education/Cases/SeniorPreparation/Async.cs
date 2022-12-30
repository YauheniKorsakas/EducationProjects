using Education.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Education.Cases.SeniorPreparation
{
    public class Async : ICase
    {
        static readonly CancellationTokenSource s_cts = new CancellationTokenSource();

        public async Task RunAsync() {
            Console.WriteLine("Application started.");
            Console.WriteLine("Press the ENTER key to cancel...\n");

            Task cancelTask = Task.Run(() =>
            {
                while (Console.ReadKey().Key != ConsoleKey.Enter) {
                    Console.WriteLine("Press the ENTER key to cancel...");
                }

                Console.WriteLine("\nENTER key pressed: cancelling downloads.\n");
                
               // s_cts.Cancel();
            });
            await cancelTask;

            Console.WriteLine("Application ending.");
        }

        private async Task GetValueAsync() {
            Console.WriteLine("In method");
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            var task = new Task(() => Console.WriteLine($"From inner task: {Thread.CurrentThread.ManagedThreadId}"));
            task.RunSynchronously();
        }
    }
}
