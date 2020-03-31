using Education.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Education.Cases {
    public class ConfigureAwaitCase : ICase {
        public async Task RunAsync() {
            await GetMessageAsync("some message").ContinueWith(async (t) => {
                await Task.Delay(5000);
                Console.WriteLine("Some result from ContinueWith");
                var context = SynchronizationContext.Current;
            });
        }

        private async Task ShowMessage() {
            var message = GetMessageAsync("some message");
            Console.WriteLine(message);
        }

        private Task<string> GetMessageAsync(string message) {
            return Task.Run(async () => {
                await Task.Delay(2000);
                return message;
            });
        }
    }
}
