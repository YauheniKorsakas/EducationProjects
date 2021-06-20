using Education.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Education.Cases.Algos
{
    public class SelectionSortCase : ICase
    {
        public Task RunAsync()
        {
            ExecuteSort();
            return Task.CompletedTask;
        }

        private void ExecuteSort()
        {
            var random = new Random();
            var list = Enumerable.Range(1, 10).Select(item => random.Next(1, 10)).ToList();
            var listCount = list.Count;
            var resultList = new List<int>();
            var flag = true;

            while (flag)
            {
                var smallestElementIndex = GetSmallestElementIndex(list);
                resultList.Add(list.ElementAt(smallestElementIndex));
                list.RemoveAt(smallestElementIndex);

                if (!list.Any())
                {
                    flag = false;
                }
            }

            resultList.ForEach(item => Console.WriteLine(item));

        }

        private int GetSmallestElementIndex(IEnumerable<int> data)
        {
            var minE = data.Min();

            return Array.FindIndex(data.ToArray(), item => item == minE);
        }
    }
}
