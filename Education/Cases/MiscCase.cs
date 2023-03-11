using Education.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Education.Cases.Misc
{
    public class MiscCase : ICase
    {
        public async Task RunAsync() {
            var list = new List<int> { 1, 2, 3 }.ToArray();
            var copy = list[..];
        }
    }
}
