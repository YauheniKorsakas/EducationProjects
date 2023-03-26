using Education.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Education.Cases.AsyncProgramming
{
    internal class DataParallelizmCase : ICase
    {
        public async Task RunAsync() {
            var list = Enumerable.Range(1, 10).ToList(); ;
            IterateParallel(list);
            Console.WriteLine("finish");
        }

        private void IterateParallel(List<int> list) {
            //Parallel.ForEach(list, i => { Console.WriteLine(i); });
            Console.WriteLine();
            Console.WriteLine();
            Parallel.For(0, list.Count, i => { Console.WriteLine($"{i} - {list[i]}"); });
        }
    }
}
