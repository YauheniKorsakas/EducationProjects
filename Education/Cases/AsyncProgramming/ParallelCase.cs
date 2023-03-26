using Education.Core;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Education.Cases.AsyncProgramming
{
    internal class ParallelCase : ICase
    {
        public async Task RunAsync() {
            Console.WriteLine(System.Environment.ProcessorCount);
        }
    
        private async Task Invoke() {
            try {
                await InvokeIterationsWithExceptionsAsync();
            } catch (AggregateException ex) {
                foreach (var exception in ex.InnerExceptions) {
                    
                }
            }
        }


        private async Task InvokeIterationsWithExceptionsAsync() {
            //  an unhandled exception causes the loop to terminate as soon as all currently running iterations finish.
            // so that it is may be needed to store exceptions in collection and propagate them as AggregateException
            var arr = Enumerable.Range(0, 10).ToArray();
            var exceptions = new ConcurrentQueue<Exception>(); // to store exceptions from parallel iterations
            Parallel.ForEach<int, int>(arr, () => 0, (j, loopState, subtotal) => {
                try {
                    if (j < 5) {
                        throw new ArgumentException("Invalid argument");
                    }
                    if (j == 6) {
                        throw new ApplicationException("Bad application");
                    }
                    subtotal += j;
                }
                catch (Exception ex) {
                    exceptions.Enqueue(ex);
                }
                return subtotal;
            },
            finalResult => Console.WriteLine(finalResult));

            if (exceptions.Count > 0) {
                throw new AggregateException(exceptions); // Use this exception to compose single one that contains all exceptions
                // from the cycle.
            }
        }

        private async Task InvokeForEachAsync() {
            var arr = Enumerable.Range(0, 10).ToArray();
            var total = 0l;
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            Parallel.ForEach<int, long>(
                arr,
                new ParallelOptions {
                    MaxDegreeOfParallelism = 2,
                    CancellationToken = cts.Token // Automatically throws exception if cancellation requested.
                },
                () => 0, // initialize variable (each partition has its own local variable)
                (j, loopState, subtotal) => {
                    Thread.Sleep(1000);
                    subtotal += j;
                    return subtotal;
                },
                finalResult => {
                    Console.WriteLine("Per partition");
                    Interlocked.Add(ref total, finalResult); // function that is invoked on each result of each partition
                });
            await Console.Out.WriteLineAsync(total.ToString());
        }

        private async Task InvokeLocalStateAsync() {
            int[] nums = Enumerable.Range(0, 10).ToArray();
            long total = 0;

            // Use type parameter to make subtotal a long, not an int
            Parallel.For<long>(0, nums.Length, () => 0, (j, loop, subtotal) =>
            {
                subtotal += nums[j];
                return subtotal;
            },
                (x) => Interlocked.Add(ref total, x)
            );
            await Console.Out.WriteLineAsync(total.ToString());
        }

        private async Task InvokeParallelIterationAsync() {
            var data = Enumerable.Range(0, 10);
            using var cts = new CancellationTokenSource();
            var result = Parallel.For(0, data.Count(), new ParallelOptions {
                CancellationToken = cts.Token,
                MaxDegreeOfParallelism = 1
            }, async (item, loop)=> {
                await Task.Delay(2000);
                await Console.Out.WriteLineAsync(item.ToString());
            });

            await Task.Delay(10000);
        }
    }
}
