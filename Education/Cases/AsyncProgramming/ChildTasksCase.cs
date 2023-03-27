using Education.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Education.Cases.AsyncProgramming
{
    /// <summary>
    ///  An attached child task is a nested task that is created with the TaskCreationOptions.AttachedToParent option
    ///  whose parent does not explicitly or by default prohibit it from being attached.
    ///  A task may create any number of attached and detached child tasks, limited only by system resources.
    ///  That is why tasks created inside parent tasks are detached by default, and you must explicitly specify
    ///  the TaskCreationOptions.AttachedToParent option to create an attached child task.
    ///  
    /// Important!
    /// If you use nested tasks without await or result (only launch) then it matters whether child task is attached
    /// to parrent or not. Otherwise if you use await child or child.Result, it does not matter whether it is attached.
    /// </summary>
    public class ChildTasksCase : ICase
    {
        public async Task RunAsync() {
            await InvokeRunAsync();
        }

        private async Task RunManyChildsAsync() {
            Task firstChild = null;
            Task secondChild = null;
            var i = 0;
            var mainTask = Task.Run(() => {
                Console.WriteLine("main");
                firstChild = Task.Run(async () => {
                    while (i < 10) {
                        await Task.Delay(500);
                        Console.WriteLine(1);
                        Interlocked.Increment(ref i);
                    }
                });

                secondChild = Task.Run(async () => {
                    while (i < 10) {
                        await Task.Delay(500);
                        Console.WriteLine(0);
                        Interlocked.Increment(ref i);
                    }
                });

                Console.WriteLine("Main end");
            });
            await Console.Out.WriteLineAsync(mainTask.Status.ToString());
            await mainTask;
            await Console.Out.WriteLineAsync(mainTask.Status.ToString()); // shows RanToCompletion as two child
            // tasks are detached and main task only launches them.
            await Task.WhenAll(firstChild, secondChild);
        }

        private async Task InvokeTaskFactoryAsync() {
            var mainTask = Task.Factory.StartNew(async () => {
                await Task.Delay(500);
                Console.WriteLine("Main");

                var child = Task.Factory.StartNew(async () => {
                    await Task.Delay(1000);
                    await Console.Out.WriteLineAsync("Child task");
                    return 1;
                }, TaskCreationOptions.AttachedToParent);
            }, TaskCreationOptions.DenyChildAttach);

            var res = await mainTask;
            await Console.Out.WriteLineAsync(res.Status.ToString());
            await res;
            await Console.Out.WriteLineAsync(res.Status.ToString());
        }

        private async Task InvokeRunAsync() {
            var mainTask = Task.Run(async () => { // Task.Run by default denies attaching childs. Use TaskFactory start new instead.
                await Task.Delay(500);
                await Console.Out.WriteLineAsync("Main task");

                var child = Task.Factory.StartNew<Task<int>>(async () => {
                    await Task.Delay(1000);
                    await Console.Out.WriteLineAsync("Child task");
                    return 1;
                }, TaskCreationOptions.AttachedToParent); // It would not work as Task.Run has DenyChildAttach by default.
                // main task status will be changed to ran to completion before child task has been ended.
                var resTask = await child;
                //var res = (await child).Result; // but if use this, thread of main task will be blocked and it will be waiting
                // for child task
                var res = await resTask;
                await Console.Out.WriteLineAsync(res.ToString());
            });
            await mainTask;
            await Console.Out.WriteLineAsync(mainTask.Status.ToString());
        }

        private async Task InvokeBlockedByResultAsync() {
            var mainTask = Task.Run<int>(() => { // However, a child task can attach to its parent only if its parent does not prohibit attached child tasks.
                var result = Task.Run<int>(async () => {
                    await Task.Delay(1000);
                    return 1;
                });

                return result.Result; // bloks thread and waits until child has been completed
            });

            await Console.Out.WriteLineAsync(mainTask.Status.ToString());
            var res = await mainTask;
            Console.WriteLine(res);
        }

        private async Task InvokeChildWithAwaitAsync() {
            Task nestedTask = null;
            var mainTask = Task.Run(async () => {
                Console.WriteLine("Outer executing.");

                nestedTask = Task.Run(async () => {
                    await Task.Delay(1000);
                    await Console.Out.WriteLineAsync("Nested task has been completed");
                });
                await nestedTask;
                await Console.Out.WriteLineAsync("Main task has been completed");
            });
            await mainTask;
            await Console.Out.WriteLineAsync(mainTask.Status.ToString());
            await Console.Out.WriteLineAsync(nestedTask.Status.ToString());
            await nestedTask;
            await Console.Out.WriteLineAsync(nestedTask.Status.ToString());
        }

        private async Task InvokeSimpleChildAsync() {
            Task nestedTask = null;
            var mainTask = Task.Run(() => {
                Console.WriteLine("Main task");
                nestedTask = Task.Run(async () => {
                    await Task.Delay(500);
                    await Console.Out.WriteLineAsync("Child task");
                });
            });

            await mainTask;
            await Console.Out.WriteLineAsync(mainTask.Status.ToString());
            await Console.Out.WriteLineAsync(nestedTask.Status.ToString());
            await nestedTask;
            await Console.Out.WriteLineAsync(nestedTask.Status.ToString());
        }
    }
}
