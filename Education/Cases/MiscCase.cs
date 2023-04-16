using Education.Cases.Patterns.Behavioral.TemplateMethod;
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
    public class MiscCase : ICase {
        public async Task RunAsync() {
            using var disposable = new Disposable();
            throw new Exception();
        }
    }

    public class Disposable : IDisposable {
        public void Dispose() {
            Console.WriteLine("Disposed");
        }
    }
}
