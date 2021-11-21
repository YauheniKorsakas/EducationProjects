using Education.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Education.Cases {
    public class MiscCase : ICase
    {
        public async Task RunAsync() {
            Console.WriteLine(Guid.NewGuid());
            Console.WriteLine((long)Guid.NewGuid().GetHashCode());
        }
    }
}
