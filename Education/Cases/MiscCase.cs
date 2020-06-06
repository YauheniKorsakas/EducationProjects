using Education.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Education.Cases {
    public class MiscCase : ICase {
        public Task RunAsync() {
            ShowItems<int>();

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
    }
}
