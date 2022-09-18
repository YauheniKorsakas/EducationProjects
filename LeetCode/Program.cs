using Education.Core;
using LeetCode.Problems;
using System;
using System.Threading.Tasks;

namespace LeetCode
{
    public class Program
    {
        static async Task Main(string[] args) {
            var instance = GetCaseInstance<RomanToInteger>();
            await instance.RunAsync();
        }

        private static ICase GetCaseInstance<T>() where T : ICase, new() => Activator.CreateInstance<T>();
    }
}
