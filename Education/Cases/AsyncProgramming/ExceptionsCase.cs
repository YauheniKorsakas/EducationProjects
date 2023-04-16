using Education.Core;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Education.Cases.AsyncProgramming
{
    /// <summary>
    /// Can catch all excepitons from all tasks using var task = Task.WhenAll();
    /// task.Exception (ex is gonna be AggregateException)
    /// object of task ALWAYS contains aggregate exception. And you can get all exceptions from inner tasks from
    /// parent task. Just dont catch any particular exception, use catch { task.Exception ... }
    /// // or you can use WaitAll
    /// If you dont use WaitAll or Wait or Result then exception will not propogate 
    /// </summary> 
    public class ExceptionsCase : ICase
    {
        public async Task RunAsync() {
            //await InvokeStreamAsync();
            await AttachTaskExceptionAsync();
        }

        public async Task AttachTaskExceptionAsync() {
            var task = Task.Factory.StartNew(async () => {
                var child = Task.Factory.StartNew(() => {
                    throw new Exception("child");
                }, TaskCreationOptions.None);
                child.Wait();
            });

            try {
                await task;
            } catch {
                // get here as child task is attached and parent task throws AggregateException.
            }
        }

        public async Task DetachedTwo() {
            var task = Task.Run(async () =>
            {
                var nestedTask = Task.Run(
                    () => throw new BusinessException("Detached child task faulted."));

                // Here the exception will be escalated back to the calling thread.
                // We could use try/catch here to prevent that.
            });

            try {
                await task;
            }
            catch {
                //foreach (var e in ae.Flatten().InnerExceptions) {
                //    if (e is BusinessException) {
                //        Console.WriteLine(e.Message);
                //    }
                //}
            }
        }

        public static async Task FlattenTwo() {
            var task = Task.Factory.StartNew(() =>
            {
                var child = Task.Factory.StartNew(() =>
                {
                    var grandChild = Task.Factory.StartNew(() =>
                    {
                        // This exception is nested inside three AggregateExceptions.
                        throw new BusinessException("Attached child2 faulted.");
                    }, TaskCreationOptions.AttachedToParent);

                    // This exception is nested inside two AggregateExceptions.
                    throw new BusinessException("Attached child1 faulted.");
                }, TaskCreationOptions.AttachedToParent);
            });

            try {
                await task;
            }
            catch {
                
            }
        }

        public static void HandleFour() {
            var task = Task.Run(
                () => throw new BusinessException("This exception is expected!"));

            while (!task.IsCompleted) { }

            if (task.Status == TaskStatus.Faulted) {
                foreach (var ex in task.Exception?.InnerExceptions ?? new(Array.Empty<Exception>())) {
                    // Handle the custom exception.
                    if (ex is BusinessException) {
                        Console.WriteLine(ex.Message);
                    }
                    // Rethrow any other exception.
                    else {
                        throw ex;
                    }
                }
            }
        }

        public static async Task ThrowMultipleExceptionsAsync() {
            Task allTasks = null;
            var firstTask = Task.Run(() => throw new IndexOutOfRangeException
            ("An IndexOutOfRangeException is thrown explicitly."));
            var secondTask = Task.Run(() => throw new InvalidOperationException
            ("An InvalidOperationException is thrown explicitly."));
            allTasks = Task.WhenAll(firstTask, secondTask);
            try {
                await allTasks;
            }
            catch {
                Console.WriteLine("The following exceptions have occurred:-\n");
                AggregateException allExceptions = allTasks.Exception;

                foreach (var ex in allExceptions.InnerExceptions) {
                    Console.WriteLine(ex.GetType().ToString());
                }
            }
        }

        private async Task InvokeWhenAllWithExceptionsAsync() {
            List<Task> tasks = new();
            tasks.Add(Task.Run(() =>
            {
                // This should throw an UnauthorizedAccessException.
                throw new ArgumentException("Incorrect argument");
            }));

            tasks.Add(Task.Run(() =>
            {
                throw new ArgumentException(
                    "The system root is not a valid path.");
            }));

            tasks.Add(Task.Run(() =>
            {
                throw new NotImplementedException(
                    "This operation has not been implemented.");
            }));
            Task whenAllTask = Task.WhenAll(tasks);
            try {
                await whenAllTask;
            }
            catch { // Here will be only first exception. In order to get all of them, us iterator 
                // for AggregateException from Task.WhenAll();
                var allExceptions = whenAllTask.Exception;

                foreach (var item in allExceptions.InnerExceptions) {
                    await Console.Out.WriteLineAsync(item.Message);
                }
            }
        }

        private async Task InvokeMultipleChildsWithExceptionsAsync() {
            var exceptions = new ConcurrentQueue<Exception>();
            var task = Task.Factory.StartNew(() => {
                var child1 = Task.Factory.StartNew(() => {
                    var exception = new ArgumentNullException("From child1");
                    exceptions.Enqueue(exception);
                    throw exception;
                }, TaskCreationOptions.AttachedToParent);
                var child2 = Task.Factory.StartNew(() => {
                    var exception = new ArgumentNullException("From child2");
                    exceptions.Enqueue(exception);
                    throw exception;
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
