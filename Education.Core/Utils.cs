using System;
using System.Collections.Generic;
using System.Linq;

namespace Education.Core
{
    public static class Utils
    {
        public static void Log(object obj) => Console.WriteLine(obj);

        public static IEnumerable<int> GenerateRandomSequence(int count, int rangeStart = 1, int rangeEnd = 10)
        {
            var random = new Random();
            return Enumerable.Range(1, count).Select(item => random.Next(rangeStart, rangeEnd));
        }
    }
}
