using Education.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Education.Cases {
    public class SyncPerformAsyncOperationCase : ICase {
        public async Task RunAsync() {
            var context = new SynchronizationContext();
            var result = PerformOperationAsync(123).Result;
            Console.WriteLine(result);
        }

        private Task<int> PerformOperationAsync(int toReturn) {
            return Task.Run(async () => {
                await Task.Delay(1000);
                return toReturn;
            });
        }
    }
}
