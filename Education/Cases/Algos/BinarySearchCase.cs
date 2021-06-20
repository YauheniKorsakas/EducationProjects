using Education.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Education.Cases.Algos
{
    public class BinarySearchCase : ICase
    {
        public Task RunAsync()
        {
            Logs();
            return Task.CompletedTask;
        }

        private void ExecuteAlgo()
        {
            var list = Enumerable.Range(0, 100).ToArray();

            var result = Array.BinarySearch(list, int.MaxValue - 1);
            Console.WriteLine(result);
        }

        private void ExecuteCustomBinarySearchAlgo()
        {
            var list = new List<int>();
            var el = list[1];
            var arr = Enumerable.Range(0, 100).ToArray();
            var result = CustomBinarySearchAlgo(arr, 88);

            Console.WriteLine(result);
        }

        private int CustomBinarySearchAlgo(IEnumerable<int> list, int item)
        {
            var low = 0;
            var high = list.Count() - 1;
            int operationsCount = 0;
            while (low <= high)
            {
                var mid = (int)Math.Floor((decimal)(low + high) / 2);
                var guess = list.ElementAt(mid);

                if (guess == item)
                {
                    return mid;
                }

                if (guess > item)
                {
                    high = mid - 1;
                } else
                {
                    low = mid + 1;
                }

                Console.WriteLine($"Operations count: {++operationsCount}");
            }

            return -1;
        }

        private void Logs()
        {
            var arr = Array.Empty<int>();
            var log2 = Math.Log2(1000000000);
            Console.WriteLine(log2);

            var numb = 100;
            int result = 1;

            for (var i = 1; i <= numb; i++)
            {
                result *= i;
            }
        }

        // От количества элементов по разному растет скорость выполнения алгоритма.
        // O(n) показывает возрастание сложности алго (его сокрости) где n - количество операций.
        // O(n) показывает худший случай.
        // Константы игнорируются.
    }
}
