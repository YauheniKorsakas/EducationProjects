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
            var arr = new[] { 1, (object)null };
            Console.WriteLine(arr.Length);
        }
    }
}
public class Solution
{
    public int[] TwoSum(int[] nums, int target) {
        int[] result = null;
        var dict = new Dictionary<int, int>();
        var str = "s";
        var queue = new Queue<int>();
        var hashset = new HashSet<int>();
        hashset.Count();

        for (var i = 0; i < nums.Length; i++) {
            for (var j = 0; j < nums.Length; j++) {
                if (i != j && (nums[i] + nums[j]) == target) {
                    result = new[] { i, j };
                }
            }
        }

        return result;
    }
}