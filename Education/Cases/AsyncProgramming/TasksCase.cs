using Education.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using System.Net;

namespace Education.Cases.AsyncProgramming.TasksCase
{
    public class TasksCase : ICase
    {
        public async Task RunAsync() {
            var task = InvokeContinuationExceptionAsync();
            await task;
        }

        private async Task InvokeContinuationExceptionAsync() {
            Task<int> task = Task.Run(
            () => {
                Console.WriteLine($"Executing task {Task.CurrentId}");
                throw new Exception("From main task");
                return 54;
            });

            var continuation = task.ContinueWith(
                antecedent => {
                    Console.WriteLine($"Executing continuation task {Task.CurrentId}");
                    //Console.WriteLine($"Value from antecedent: {antecedent.Result}");

                    throw new InvalidOperationException();
                });

            try {
                var resultTask = Task.WhenAll(task, continuation);
                await resultTask;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task SeparateContinuationTasksAsync() {
            var task = Task.Run(() => { Console.WriteLine("Main task completed"); });
            var continuation1 = task.ContinueWith(t => { Console.WriteLine("First continutation"); });
            var continuation2 = task.ContinueWith(t => { Console.WriteLine("Second continutation"); });
            await task;
            await Task.WhenAll(continuation1, continuation2);
        }

        private async Task ThrowIfRequestedInTaskAsync() {
            using var cts = new CancellationTokenSource();
            cts.CancelAfter(1000);
            Task task;
            Task task1;
            try {
                task = Task.Run(async () => {
                    await Task.Delay(1000);
                    cts.Token.ThrowIfCancellationRequested();
                }, cts.Token);
                task1 = Task.Run(async () => {
                    await Task.Delay(5000, cts.Token);
                    cts.Token.ThrowIfCancellationRequested();
                });
                await Task.WhenAll(task, task1);
            } catch (Exception ex) { }
        }

        private async Task InvokeTaskContinuationOptionsAsync() {
            var task = Task.Run<int>(() => {
                throw new Exception();
                return 1;
            });
            var continuation = task.ContinueWith(t => { // if continuation task does not rise exception so source exception is not propogated
                // outside continuation task.
                // here t.Exception is not null as continuation is OnlyOnFaulted
                Console.WriteLine("Faulted case");
            }, TaskContinuationOptions.OnlyOnFaulted)
            .ContinueWith(t => {
                Console.WriteLine("Continuation success");
            }, TaskContinuationOptions.OnlyOnRanToCompletion);
            //  If the condition isn't true when the antecedent is ready to invoke the continuation,
            //  the continuation transitions directly to the TaskStatus.Canceled state and can't be started later.
            // TaskContinuationOptions.OnlyOnCanceled
            var onSuccessContinuation = task.ContinueWith(t => {
                Console.WriteLine("Success");
            }, TaskContinuationOptions.OnlyOnRanToCompletion);
            await task;
            await continuation; // it would be better to create chain and configure all possible variations through 
            // task chain
            await onSuccessContinuation;
        }

        private async Task RemoveTaskAsItCompletesAsync() { // handle date asap
            var tasks = new List<Task<int>> {
                Task.Run(async () => {
                    await Task.Delay(1000);
                    Console.WriteLine("First");
                    return 1;
                }),
                Task.Run(async () => {
                    await Task.Delay(1500);
                    Console.WriteLine("Second");
                    return 2;
                })
            };

            while (tasks.Count > 0) {
                var task = await Task.WhenAny(tasks);
                var result = await task;
                await Console.Out.WriteLineAsync(result.ToString());
                tasks.Remove(task);
            }
        }

        private async Task WhenAnyBatchWithCancellation() { // can cancel the rest of tasks if found needed result
            using var cts = new CancellationTokenSource();
            Task.Run(async () => {
                await Task.Delay(500);
            });
                cts.Cancel();
            var tasks = new Task<int>[] {
                Task.Run(async () => {
                    await Task.Delay(1000, cts.Token);
                    Console.WriteLine("First");
                    return 1;
                }, cts.Token),
                Task.Run(async () => {
                    await Task.Delay(1500, cts.Token);
                    Console.WriteLine("Second");
                    return 2;
                }, cts.Token)
            };

            var firstResultTask = await Task.WhenAny(tasks);
            var result = await firstResultTask;
            cts.Cancel();
            await Console.Out.WriteLineAsync(result.ToString());
        }

        private async Task InvokeExceptionBatchAsync() {
            var tasks = new Task<int>[] {
                Task.Run<int>(() => {
                    throw new ArgumentException("First exception");
                    return 1;
                }),
                Task.Run<int>(() => {
                    throw new NullReferenceException("Second exception");
                    return 2;
                }),
                Task.FromResult(100)
            };
            try {
                var resultTask = Task.WhenAll(tasks);
                var result = await resultTask;
            } catch (Exception ex) { // have only one exception here (First exception)
                foreach (var task in tasks.Where(s => s.Status == TaskStatus.Faulted)) {
                    Console.WriteLine(task.Exception.Message);
                }

                foreach (var task in tasks.Where(s => s.IsCompletedSuccessfully)) {
                    Console.WriteLine(task.Result);
                }
            }
        }

        private async Task InvokeExceptionsAsync() {
            try {
                await Task.Run(() => {
                    throw new Exception("From task.....");
                });
            } catch (Exception ex) {

            }
        }

        private async Task TaskDelayAsync() {
            using var cts = new CancellationTokenSource();
            var task = Task.Run(async () => {
                await Task.Delay(5000);
                cts.Cancel();
            });
            Console.WriteLine("Task started");
            await Task.Delay(10000, cts.Token);
            Console.WriteLine("Task ended");
            await task;
        }

        private async Task TaskCompletionSourceCaseAsync() {
            var tsc = new TaskCompletionSource<int>();
            var task = tsc.Task;
            tsc.SetResult(1);
            Console.WriteLine(await task);
        }

        private async Task WhenAllAsync() {
            var task = Task.WhenAll(
                Task.FromResult(1),
                Task.FromResult(2)
            );
            var result = await task;
            result.ToList().ForEach(s => Console.WriteLine(s));
        }

        private Task HandleListByChildTasksAsync() {
            var list = new List<int> { 1, 2, 3, 4 };
            var mainProcessingTask = Task.Run(() => {
                for (var i = 0; i < list.Count; i++) {
                    Task.Factory.StartNew(async () => {
                        await Task.Delay(1000);
                        await Console.Out.WriteLineAsync($"{list[i]} has been handled.");
                    });
                }
                Console.WriteLine("asdasd");
            });

            return mainProcessingTask;
        }

        private async Task GetPersonByContinuationAsync() {
            var getPersonTask = Task.Run(() => new Person { })
                .ContinueWith(task => {
                    task.Result.Id = 1;
                    throw new Exception("From first continue task");
                    return task.Result;
                })
                .ContinueWith(task => {
                    if (!task.IsCompletedSuccessfully) {
                        throw new Exception("From second continue task");
                    }
                    task.Result.Name = "Zheka";
                    return task.Result;
                });

            var result = await getPersonTask;
        }

        private async Task ExecuteTasksAsync() {
            var task = new Task(async () => {
                await Task.Delay(1000);
                Console.WriteLine("End");
            });
            task.Start();
            await task;
            Console.WriteLine("Method ends");
        }

        private async Task ExecuteWithValue() {
            var task = Task.Run(async () => {
                await Task.Delay(2000);
                return 2;
            });

            Console.Out.WriteLineAsync(task.Result.ToString());
            await Console.Out.WriteLineAsync("End");
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
                while (true) {
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

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
