using Education.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Education.Cases {
    public class MiscCase : ICase {
        public Task RunAsync() {
            Console.WriteLine("some data");

            return Task.CompletedTask;
        }

        private void ShowItems<T>(params T[] items) {
            foreach (var item in items) {
                Console.WriteLine(item);
            }
        }

        private (int age, string name)? GetData() {
            return (age: 12, name: "Name");
        }

        private void AggregateTest() {
            var list = new List<int> {
                1, 2, 3
            };

            var startIndex = 1;
            var endIndex = 2;

            var str = "asd";
            //var result = string.Join(string.Empty, Enumerable.Repeat('*', endIndex)) + str.Substring(endIndex);
            var result = Enumerable.Repeat('*', endIndex).Aggregate("", (prev, curr) => prev + curr) + str.Substring(endIndex);
            result = str.PadLeft(5, '$');
        }

        private void ChangeString() {
            var str = "123";
        }
    }
}
