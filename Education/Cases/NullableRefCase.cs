using Education.Core;
using System;
using System.Threading.Tasks;

namespace Education.Cases {
    public class NullableRefCase : ICase {
        public async Task RunAsync() {
            string notNull = null;

            Console.WriteLine($"Result is {notNull}");
        }
    }
}
