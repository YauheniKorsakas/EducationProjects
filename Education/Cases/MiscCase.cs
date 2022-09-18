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
            var res = RNGCryptoServiceProvider.Create();
            var prohibition = new { };
            var a = 1;
        }
    }
}
