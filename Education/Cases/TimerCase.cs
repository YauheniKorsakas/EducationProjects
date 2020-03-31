using Education.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Education.Cases {
    public class TimerCase : ICase {
        public async Task RunAsync() {
            Console.WriteLine("Start runasync method.");
            StartTimer();
        }

        private void StartTimer() {
            var timer = new Timer(
                async (state) => await DisplayDataAsync(), // callback invokes independetly of each other.
                null,
                0,
                2000
            );
        }

        private async Task DisplayDataAsync() {
            await Task.Delay(10000);
            Console.WriteLine($"Some data from {nameof(DisplayDataAsync)}");
        }
    }
}
