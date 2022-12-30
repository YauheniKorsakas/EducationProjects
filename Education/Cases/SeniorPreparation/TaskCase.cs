using Education.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Education.Cases.SeniorPreparation
{
    public class TaskCase : ICase
    {
        public async Task RunAsync() {
            //await AttachedToParrent();
            await CancelTask();
            Console.WriteLine("End");
        }

        private async Task AttachedToParrent() {
            var task = Task.Factory.StartNew(() => {
                Console.WriteLine("Task start");
                Task.Factory.StartNew((x) => { Thread.Sleep(1000); }, 1, TaskCreationOptions.None);
            });

            await task;
        }

        private async Task CancelTask() {
            Console.WriteLine("Cancel task");
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();
            var task = Task.Factory.StartNew(
                () => {
                    Console.WriteLine("Task has been started");
                }, cancellationTokenSource.Token);
            await task;
            Console.WriteLine(task.Status);
        }
    }
}
