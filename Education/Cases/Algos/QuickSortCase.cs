using Education.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Education.Cases.Algos
{
    public class QuickSortCase : ICase
    {
        public Task RunAsync()
        {
            var list = Utils.GenerateRandomSequence(10).ToList();
            var result = ExecuteQuickSort(list);

            return Task.CompletedTask;
        }

        private IEnumerable<int> ExecuteQuickSort(List<int> list)
        {
            if (list.Count < 2)
            {
                return list;
            }

            var marker = list.First();
            var lessThanMarker = list.Where(item => item <= marker).ToList();
            var greaterThanMarker = list.Where(item => item > marker).ToList();

            var lessSort = ExecuteQuickSort(lessThanMarker);
            var greaterSort = ExecuteQuickSort(greaterThanMarker);
            var result = lessSort.Concat(new[] { marker }).Concat(greaterSort);

            return result;
        }
    }
}

// quick sort. выбирается 1 элемент, меньше и больше его. а затем сонкат по рекурсии.