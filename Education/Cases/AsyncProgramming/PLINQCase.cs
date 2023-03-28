using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Education.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Education.Cases.AsyncProgramming
{
    /// <summary>
    /// Its good to parallel expensive workload
    /// Additional operations like .AsOrdered() or .Join() incur more complexity
    /// By default chunks results require to be merged as one before consuming. If its not the case - use ForAll.
    /// ForAll does not require chunks results joining before consuming their results
    /// In PLINQ, the goal is to maximize performance while maintaining correctness.
    /// A query should run as fast as possible but still produce the correct results. 
    /// </summary>
    [MemoryDiagnoser]
    public class PLINQCase : ICase
    {
        private List<int> list = new List<int>();

        public async Task RunAsync() {
            BenchmarkRunner.Run<PLINQCase>();
        }

        [GlobalSetup]
        public void Setup() {
            list.AddRange(Enumerable.Range(1, 100));
        }

        [Benchmark]
        public int MyFirstBenchmarkMethod() {
            var count = 0;
            foreach (var item in list) {
                var checkItem = item;
            }
            return count;
            //Write your code here   
        }

        [Benchmark]
        public int MySecondBenchmarkMethod() {
            var count = 0;
            list
                .AsParallel()
                .ForAll(s => {
                    var checkItem = s;
                });
            
            return 1;
            //Write your code here
        }

        private async Task<int> HandleNumberAsync(int number) {
            await Task.Delay(200);
            return number;
        }
    }
}
