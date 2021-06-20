using Education.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Education.Cases.Algos
{
    public class RecursionCase : ICase
    {
        public Task RunAsync()
        {
            ExecuteRecursion();
            return Task.CompletedTask;
        }

        private void ExecuteRecursion()
        {
            var list = Utils.GenerateRandomSequence(10).ToList();
            var result = Sort(list);
        }

        private List<int> Sort(List<int> data, int iterationsCount = 0)
        {
            if (iterationsCount == data.Count)
            {
                return data;
            }

            var maxElement = data.TakeLast(data.Count - iterationsCount).Max();
            var indexOfMaxElement = data.LastIndexOf(maxElement);
            data.RemoveAt(indexOfMaxElement);
            data.Insert(0, maxElement);

            if (iterationsCount != data.Count)
            {
                iterationsCount++;
            }

            return Sort(data, iterationsCount);
        }
    }
}
