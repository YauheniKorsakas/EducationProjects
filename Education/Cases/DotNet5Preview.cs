using Education.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Collections;
using System.Diagnostics;

namespace Education.Cases
{
    public class DotNet5Preview : ICase
    {
        public Task RunAsync()
        {
            var enumerable = Enumerable.Range(1, 100000000);
            var list = Enumerable.Range(1, 100000000).ToList();
            var stopWatch = new Stopwatch();

            stopWatch.Start();
            Console.WriteLine(enumerable.Count());
            stopWatch.Stop();
            Console.WriteLine(stopWatch.Elapsed + "\n\n");

            stopWatch.Reset();

            stopWatch.Start();
            Console.WriteLine(list.Count());
            stopWatch.Stop();
            Console.WriteLine(stopWatch.Elapsed);


            return Task.CompletedTask;
        }
    }
}
