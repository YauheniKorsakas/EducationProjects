using Education.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Education.Cases
{
    public class MiscCase : ICase
    {
        public async Task RunAsync() {
            var numbs = new int[] { 123, 33, 25};

            var currentMaxValue = 0;
            var left = 0;
            var right = numbs.Length - 1;

            while (left < right) {
                int width = right - left;
                currentMaxValue = Math.Max(currentMaxValue, width * Math.Min(numbs[left], numbs[right]));

                if (numbs[left] <= numbs[right]) {
                    left++;
                } else {
                    right--;
                }
            }
        }
    }
}

// sort by value
// 

// x = i last - i first
// y = min(pair[first], pair[last])
// x * y
// int maxAmount = x * y;